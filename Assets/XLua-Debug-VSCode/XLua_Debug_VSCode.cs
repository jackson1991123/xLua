using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;

namespace XLua.DebugExtension
{
    public class XLua_Debug_VSCode
    {
        [CSharpCallLua]
        private delegate void StartUpCaller(LuaTable self, string localHost, int port);

        [CSharpCallLua]
        private delegate void LoopCallCaller(LuaTable self);

        private static string debugLuaFilePath;
        private static string debugLuaJITFilePath;
        private static string debugLuaEnvFilePath;

        private static LuaTable scriptTable;
        private static LoopCallCaller loopCallCaller;
        private static WaitForSeconds waitFor1Seconds = new WaitForSeconds(1);

        /// <summary>
        /// 开启这个debug断点功能
        /// 在mono里边调用，这个mono最好是传说中的LuaManager
        /// </summary>
        public static void StartUp(MonoBehaviour luaManagerScript, LuaEnv usingEnv,string ip = "127.0.0.1",int port = 7003)
        {
            debugLuaFilePath = string.Concat(Application.dataPath, "/XLua-Debug-VSCode/LuaDebug.lua").Replace("/", "\\");
            debugLuaJITFilePath = string.Concat(Application.dataPath, "/XLua-Debug-VSCode/LuaDebugjit.lua").Replace("/", "\\");
            debugLuaEnvFilePath = string.Concat(Application.dataPath, "/XLua-Debug-VSCode/XLua_Debug_VSCode_Env.lua").Replace("/", "\\");
            usingEnv.AddLoader(LoaderExtension);

            LuaTable meta = usingEnv.NewTable();
            scriptTable = usingEnv.NewTable();
            meta.Set("__index", usingEnv.Global);
            scriptTable.SetMetaTable(meta);
            meta.Dispose();

            usingEnv.DoString(System.IO.File.ReadAllText(debugLuaEnvFilePath), "XLua_Debug_VSCode_Env", scriptTable);
            StartUpCaller startUpCaller = scriptTable.GetInPath<StartUpCaller>("XLua_Debug_VSCode_Env.StartUp");
            loopCallCaller = scriptTable.GetInPath<LoopCallCaller>("XLua_Debug_VSCode_Env.LoopCall");

            startUpCaller.Invoke(scriptTable, ip, port);

            if (luaManagerScript != null)
            {
                luaManagerScript.StartCoroutine(LoopCall());
            }
        }

        private static byte[] LoaderExtension(ref string fileName)
        {
            if (string.Equals(fileName.ToLower(), "luadebug"))
            {
                return System.IO.File.ReadAllBytes(debugLuaFilePath);
            }
            else if (string.Equals(fileName.ToLower(), "luadebugjit"))
            {
                return System.IO.File.ReadAllBytes(debugLuaJITFilePath);
            }
            return null;
        }

        private static  IEnumerator LoopCall()
        {
            while (Application.isPlaying)
            {
                if (loopCallCaller != null)
                {
                    loopCallCaller.Invoke(scriptTable);
                }
                yield return waitFor1Seconds;
            }
        }
    }
}

