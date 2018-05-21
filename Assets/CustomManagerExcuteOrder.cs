using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义了脚本执行激活的优先级
/// </summary>
public class CustomManagerExcuteOrder
{
    /// <summary>
    /// LuaManager需要在资源引擎启动之后启用
    /// </summary>
    public const int LUAMANAGER = -100;
}
