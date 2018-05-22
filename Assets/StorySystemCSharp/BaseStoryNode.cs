using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStoryNode
{
    public string tag { get; set; }
    public bool abort { get; set; }
    public List<BaseStoryNode> nextNodes = new List<BaseStoryNode>();
    public System.Action<BaseStoryNode> onFinishHandler;
    public System.Action<BaseStoryNode> onAbortHandler;

    public BaseStoryNode() { }
    public BaseStoryNode(BaseStoryNode parent) { parent.AddNext(this); }

    public virtual void AddNext(BaseStoryNode nextNode)
    {
        nextNodes.Add(nextNode);
        onFinishHandler += (n) => { nextNode.Start(); };
    }

    public virtual void Start()
    {
    }

    public virtual void Finish()
    {
        if (!abort && onFinishHandler != null)
        {
            onFinishHandler.Invoke(this);
        }
        onFinishHandler = null;
    }

    public virtual void Abort()
    {
        abort = true;
        if (onAbortHandler != null)
        {
            onAbortHandler.Invoke(this);
        }
        for (int i = 0; i < nextNodes.Count; i++)
        {
            nextNodes[i].Abort();
        }
    }
}
