using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HacksScene))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HacksScene example = (HacksScene)target;
        if (GUILayout.Button("OnCall"))
            example.OnCall();
        if (GUILayout.Button("OnFoundObjs"))
            example.OnFoundObjects();
    }
}
