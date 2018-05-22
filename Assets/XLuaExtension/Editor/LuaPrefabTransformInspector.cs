//----------------------------------------------
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CanvasRenderer))]
public class MarkQuickSetter : Editor
{
    GameObject targetObject;

    void OnEnable()
    {
        targetObject = (target as CanvasRenderer).gameObject;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("将物体标记为：");

        if (targetObject.GetComponent<UnityEngine.UI.Text>() != null)
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Lable", GUILayout.Width(100)))
                {
                    targetObject.name = targetObject.name.StartsWith("lb@") ? targetObject.name : "lb@" + targetObject.name;
                }
                GUILayout.Space(5f);
                GUILayout.Label("标签: lb@");
            }
            EditorGUILayout.EndHorizontal();
        }

        if (targetObject.GetComponent<UnityEngine.UI.Image>() != null)
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Image", GUILayout.Width(100)))
                {
                    targetObject.name = targetObject.name.StartsWith("img@") ? targetObject.name : "img@" + targetObject.name;
                }
                GUILayout.Space(5f);
                GUILayout.Label("图片: img@");
            }
            EditorGUILayout.EndHorizontal();
        }

        if (targetObject.GetComponent<UnityEngine.UI.Button>() != null)
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Button", GUILayout.Width(100)))
                {
                    targetObject.name = targetObject.name.StartsWith("btn@") ? targetObject.name : "btn@" + targetObject.name;
                }
                GUILayout.Space(5f);
                GUILayout.Label("按钮: btn@");
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("去除所有标记"))
        {
            var l = targetObject.name.LastIndexOf("@") + 1;
            targetObject.name = targetObject.name.Substring(l, targetObject.name.Length - l);
        }
    }
}
