using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaBranchNode : BranchStoryNode
{
    public override void Start()
    {
        base.Start();
        getBranchTagHandler = () => {
            return Random.Range(1, 1000) < 500 ? "1" : "2";
        };
    }
}
