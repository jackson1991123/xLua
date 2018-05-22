using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using XLua.Handlers;

public class Tester : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var script = LuaManager.Get().CreateLuaObject("GameBoot");
        LuaFunc startFunction = script.GetInPath<LuaFunc>("GameBoot.StartGame");
        startFunction.Invoke();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
