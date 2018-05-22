using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UnityEngine;

using XLua;

namespace XLua.useless
{
    public class LuaManagerChecker
    {
        [RuntimeInitializeOnLoadMethod]
        private static void NotExistWarning()
        {
            LuaManager existManager = Object.FindObjectOfType<LuaManager>();
            if (existManager == null)
            {
                Debug.LogError("请确保当前scene中有一个prefab上贴上了 LuaManager脚本，否则整个Lua系统是没有办法工作的.");
            }
            else
            {
                Debug.Log("LuaManager 发现 可以使用Lua系统");
            }
        }
    }
}

[DefaultExecutionOrder(CustomManagerExcuteOrder.LUAMANAGER)]
/// <summary>
/// 作为xlua的接口，调用lua，都通过这个
/// </summary>
public class LuaManager : MonoBehaviour
{
    /// <summary>
    /// 加载方式，根据filePathFormat所给的后缀名自行判定的
    /// </summary>
    public enum LoadMode
    {
        Auto,
        IO,
        AssetBundle,
    }

    private static LuaManager g_instance = null;
    public static LuaManager Get() { return g_instance; }

    [Tooltip("使用的加载模式，设置为Auto会根据filePathFormat的后缀名判定")]
    public LoadMode usingLoadMode = LoadMode.Auto;

    [Tooltip("modulename->filepath的格式\n#dataPath#:Application.dataPath\n")]
    public string filePathFormat = "#dataPath#/luascripts/{0}.lua";

    [Tooltip("是否用VSCode断点Debug")]
    public bool vscodeDebug = true;

    public bool isRunning {
        get { return lua != null; }
    }

    public LuaEnv lua {
        get;private set;
    }

    /// <summary>
    /// 专门用来拼接string的构造器，避免gc
    /// </summary>
    private StringBuilder pathBuilder = new StringBuilder();

    private void Awake()
    {
        g_instance = this;
        gameObject.name = "*LuaManager";
        GameObject.DontDestroyOnLoad(gameObject);

        filePathFormat = filePathFormat.Replace("#dataPath#", Application.dataPath);
        filePathFormat = filePathFormat.Replace("#persistentDataPath#", Application.persistentDataPath);

        if (usingLoadMode == LoadMode.Auto)
        {
            usingLoadMode = filePathFormat.EndsWith(".asb") ? LoadMode.AssetBundle : LoadMode.IO;
        }
        StartUp();
    }

    private void StartUp()
    {
        if (!isRunning)
        {
            lua = new LuaEnv();

            if (usingLoadMode == LoadMode.IO)
            {
                lua.AddLoader(ModuleFileContent_SystemIO_Bytes);
            }
            if (usingLoadMode == LoadMode.AssetBundle)
            {
                lua.AddLoader(ModuleFileContent_AssetBundle_Bytes);
            }

            if (vscodeDebug)
            {
                XLua.DebugExtension.XLua_Debug_VSCode.StartUp(this, lua);
            }
        }
    }

    public void ReBoot()
    {
        lua.Dispose();
        lua = null;
        System.GC.Collect();
        StartUp();
    }

    public LuaTable CreateLuaObject(string moduleName)
    {
        LuaTable meta = lua.NewTable();
        LuaTable scriptTable = lua.NewTable();
        meta.Set("__index", lua.Global);
        scriptTable.SetMetaTable(meta);
        meta.Dispose();

        byte[] content = usingLoadMode == LoadMode.AssetBundle ? ModuleFileContent_AssetBundle_Bytes(ref moduleName) : ModuleFileContent_SystemIO_Bytes(ref moduleName);
        if (content != null)
        {
            lua.DoString(content, moduleName, scriptTable);
        }
        else
        {
            throw new System.Exception(string.Format("[{0}]:不存在", moduleName));
        }

        return scriptTable;
    }

    private byte[] ModuleFileContent_AssetBundle_Bytes(ref string moduleName)
    {
        pathBuilder.AppendFormat(filePathFormat, moduleName.Replace(".", "/"));
        pathBuilder.Append(".asb");
        string path = pathBuilder.ToString();
        pathBuilder.Length = 0;

        AssetBundle bundle = AssetBundle.LoadFromFile(path);
        if (bundle != null)
        {
            TextAsset text = bundle.LoadAsset<TextAsset>(moduleName);
            if (text != null)
            {
                return text.bytes;
            }
        }
        return null;
    }

    private byte[] ModuleFileContent_SystemIO_Bytes(ref string moduleName)
    {
        pathBuilder.AppendFormat(filePathFormat, moduleName.Replace(".", "/"));
        string path = pathBuilder.ToString();
        pathBuilder.Length = 0;
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
    }
}
