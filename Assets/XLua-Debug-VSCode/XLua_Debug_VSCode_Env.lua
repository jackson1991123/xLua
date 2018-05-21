XLua_Debug_VSCode_Env = {};

XLua_Debug_VSCode_Env.LuaDebugerJIT = "LuaDebugjit";
XLua_Debug_VSCode_Env.LuaDebuger = "LuaDebug";

function XLua_Debug_VSCode_Env:StartUp(localHost,port)
    local breakSocketHandle,debugXpCall = require("LuaDebugjit")(localHost,port);
    self.breakSocketHandle = breakSocketHandle;
    self.debugXpCall = debugXpCall;
end

function XLua_Debug_VSCode_Env:LoopCall()
    if self.breakSocketHandle ~= nil then
        
        self.breakSocketHandle()
    end
end