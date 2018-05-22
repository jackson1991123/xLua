using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogNode : CoroutineStoryNode
{
    public string whom;
    public string speakWhat;
    public float speakTime;

    public CharacterDialogNode() { }
    public CharacterDialogNode(BaseStoryNode parent) : base(parent) { }

    public override void Start()
    {
        base.Start();
       
    }

    protected override IEnumerator ProcessFunction()
    {
        Debug.Log(string.Format("{0} 说：{1}", whom, speakWhat));
        yield return new WaitForSeconds(speakTime);
        Finish();
    }
}
