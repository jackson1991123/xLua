using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyNode : BaseStoryNode
{
    public EmptyNode() { }
    public EmptyNode(BaseStoryNode parent) : base(parent) { }

    public override void Start()
    {
        base.Start();
        Finish();
    }
}
