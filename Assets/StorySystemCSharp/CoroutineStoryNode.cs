using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoroutineStoryNode : BaseStoryNode
{
    public CoroutineStoryNode() { }
    public CoroutineStoryNode(BaseStoryNode parent) : base(parent) { }

    protected Coroutine coroutine;

    public override void Start()
    {
        base.Start();
        coroutine = CoroutineManager.Get().StartASyncFunction(ProcessFunction);
    }

    protected abstract IEnumerator ProcessFunction();

    public override void Finish()
    {
        base.Finish();
        coroutine = null;
    }

    public override void Abort()
    {
        base.Abort();
        if (coroutine != null)
        {
            CoroutineManager.Get().StopASyncFunction(coroutine);
            coroutine = null;
        }
    }
}
