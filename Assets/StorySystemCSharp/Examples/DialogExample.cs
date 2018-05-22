using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogExample : MonoBehaviour {
    EmptyNode root;
    // Use this for initialization
    void Start ()
    {
        root = new EmptyNode();

        CharacterDialogNode n1 = new CharacterDialogNode(root)
        {
            whom = "张三",
            speakWhat = "快开始了",
            speakTime = 1
        };

        SpawnStoryNode set1 = new SpawnStoryNode(n1);

        CharacterDialogNode n21 = new CharacterDialogNode()
        {
            whom = "李四",
            speakWhat = "王五你先说吧，你口吃",
            speakTime = 2
        };
        set1.AddSub(n21);

        CharacterDialogNode n22 = new CharacterDialogNode();
        set1.AddSub(n22);
        n22.whom = "王五";
        n22.speakWhat = "好好好好。。。。呵呵呵呵。。。。";
        n22.speakTime = 3;

        CharacterDialogNode n3 = new CharacterDialogNode();
        set1.AddNext(n3);
        n3.whom = "张三";
        n3.speakWhat = "都废话玩了吧";
        n3.speakTime = 1;

        n3.onFinishHandler = (n) => { Debug.Log("一切都结束了"); };
        n3.onAbortHandler = (n) => { Debug.Log("我被打断啦"); };
        root.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (GUILayout.Button("ABORT"))
        {
            root.Abort();
        }
    }
}
