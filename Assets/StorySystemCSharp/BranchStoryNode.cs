using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchStoryNode : BaseStoryNode
{
    public System.Func<string> getBranchTagHandler = null;

    public override void AddNext(BaseStoryNode nextNode)
    {
        nextNodes.Add(nextNode);
    }

    public override void Finish()
    {
        if (getBranchTagHandler != null)
        {
            string wannaTag = getBranchTagHandler.Invoke();
            for (int i = 0; i < nextNodes.Count; i++)
            {
                var nextNode = nextNodes[i];
                if (string.Equals(wannaTag, nextNode.tag))
                {
                    onFinishHandler += (n) => { nextNode.Start(); }; ;
                }
            }
        }
        base.Finish();
    }
}
