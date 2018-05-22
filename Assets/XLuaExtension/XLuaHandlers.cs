using System.Collections;
using System.Collections.Generic;

namespace XLua.Handlers
{
    /// <summary>
    /// 无参数"."方法
    /// </summary>
    [CSharpCallLua]
    public delegate void LuaFunc();

    /// <summary>
    /// 无参数 “：”方法
    /// </summary>
    /// <param name="self"></param>
    [CSharpCallLua]
    public delegate void LuaTargetFunc(LuaTable self);

    [CSharpCallLua]
    public delegate LuaTable XLuaBehavior_APIInit(LuaTable self, UnityEngine.Transform thisTransform);

    [CSharpCallLua]
    public delegate LuaTable XLuaBehavior_APIRelease(LuaTable self);

    [CSharpCallLua]
    public delegate void XLuaBehavior_Awake(LuaTable self, LuaTable apiTable);

    [CSharpCallLua]
    public delegate void XLuaBehavior_Start(LuaTable self);

    [CSharpCallLua]
    public delegate void XLuaBehavior_OnEnable(LuaTable self);

    [CSharpCallLua]
    public delegate void XLuaBehavior_OnDisable(LuaTable self);

    [CSharpCallLua]
    public delegate void XLuaBehavior_OnDestory(LuaTable self);

    [CSharpCallLua]
    public delegate void XLuaBehavior_Update(LuaTable self, float deltaTime);
}
