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
    private static LuaManager g_instance = null;
    public static LuaManager Get() { return g_instance; }

    [Tooltip("modulename->filepath的格式\n#dataPath#:Application.dataPath\n")]
    public string filePathFormat = "#dataPath#/luascripts/{0}";
    private const string bytesTail = ".bytes";
    private const string luaTail = ".lua";

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

        StartUp();
    }

    private void StartUp()
    {
        if (!isRunning)
        {
            lua = new LuaEnv();
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
        

        return scriptTable;
    }

    private string ModuleFileContent(string moduleName)
    {
        /*
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            //file mode io on 
            pathBuilder.AppendFormat(filePathFormat, moduleName);
            pathBuilder.Append(bytesTail);

        }
        


        pathBuilder.Length = 0;
        */
        return string.Empty;
    }
}
