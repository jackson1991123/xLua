using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStoryNode : BaseStoryNode
{
    public List<BaseStoryNode> subNodes = new List<BaseStoryNode>();

    private List<string> unFinishedTags = new List<string>();

    public SpawnStoryNode() { }
    public SpawnStoryNode(BaseStoryNode parent) : base(parent) { }

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < subNodes.Count; i++)
        {
            subNodes[i].Start();
        }
    }

    public void AddSub(BaseStoryNode subNode)
    {
        subNodes.Add(subNode);
        unFinishedTags.Add(subNode.tag);
        subNode.onFinishHandler += OnSubNodeFinish;
    }

    private void OnSubNodeFinish(BaseStoryNode node)
    {
        for (int i = 0; i < unFinishedTags.Count; i++)
        {
            if (string.Equals(unFinishedTags[i], node.tag))
            {
                unFinishedTags.RemoveAt(i);
                break;
            }
        }

        if (unFinishedTags.Count == 0)
        {
            Finish();
        }
    }
}
