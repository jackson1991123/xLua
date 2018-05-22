using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XLua;
using XLua.Handlers;

/// <summary>
/// 机器自动根据抓取标志生成一张api lua
/// 需要手动撰写一张 ctrllua，ctrllua的名字即为模块名
/// </summary>
public class XLuaBehavior : MonoBehaviour
{
    /// <summary>
    /// 模块名称
    /// </summary>
    public string moduleName;

    private LuaTable apiScript;
    private LuaTable ctrlScript;

    private XLuaBehavior_APIRelease apiReleaseHandler = null;

    private XLuaBehavior_Awake awakeHandler = null;
    private XLuaBehavior_Start startHandler = null;
    private XLuaBehavior_OnEnable enableHandler = null;
    private XLuaBehavior_OnDisable disableHandler = null;
    private XLuaBehavior_OnDestory destroyHandler = null;

    protected virtual void Awake()
    {
        string apiScriptName = moduleName + "API";
        apiScript = LuaManager.Get().CreateLuaObject(apiScriptName);
        ctrlScript = LuaManager.Get().CreateLuaObject(moduleName);

        //api抓取
        XLuaBehavior_APIInit apiInitHandler = apiScript.GetInPath<XLuaBehavior_APIInit>(apiScriptName + ".Init");
        apiReleaseHandler = apiScript.GetInPath<XLuaBehavior_APIRelease>(apiScriptName + ".Release");

        //ctrl抓取
        awakeHandler = ctrlScript.GetInPath<XLuaBehavior_Awake>(moduleName + ".Awake");
        startHandler = ctrlScript.GetInPath<XLuaBehavior_Start>(moduleName + ".Start");
        enableHandler = ctrlScript.GetInPath<XLuaBehavior_OnEnable>(moduleName + ".OnEnable");
        disableHandler = ctrlScript.GetInPath<XLuaBehavior_OnDisable>(moduleName + ".OnDisable");
        destroyHandler = ctrlScript.GetInPath<XLuaBehavior_OnDestory>(moduleName + ".OnDestory");

        apiInitHandler.Invoke(apiScript, transform);
        awakeHandler.Invoke(ctrlScript, apiScript);
    }

    protected virtual void Start ()
    {
        if (startHandler != null)
        {
            startHandler.Invoke(ctrlScript);
        }
	}
	
	protected virtual void OnEnable()
    {
        if (enableHandler != null)
        {
            enableHandler.Invoke(ctrlScript);
        }
    }

    protected virtual void OnDisable()
    {
        if (disableHandler != null)
        {
            disableHandler.Invoke(ctrlScript);
        }
    }

    protected virtual void OnDestroy()
    {
        if (destroyHandler != null)
        {
            destroyHandler.Invoke(ctrlScript);
        }

        if (apiReleaseHandler != null)
        {
            apiReleaseHandler.Invoke(apiScript);
        }

        apiScript = null;
        ctrlScript = null;

        awakeHandler = null;
        startHandler = null;
        enableHandler = null;
        disableHandler = null;
        destroyHandler = null;
        apiReleaseHandler = null;
    }
}
