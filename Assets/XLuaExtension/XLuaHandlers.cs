using System.Collections;
using System.Collections.Generic;

namespace XLua.Handlers
{
    /// <summary>
    /// 无参数 “：”方法
    /// </summary>
    /// <param name="self"></param>
    [CSharpCallLua]
    public delegate void LuaTargetFunc(LuaTable self);
}
