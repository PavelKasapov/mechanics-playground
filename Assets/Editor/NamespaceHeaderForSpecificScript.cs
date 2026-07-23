using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
public class NamespaceHeaderForSpecificScript : Editor
{
    private static readonly string[] TargetClassNames = { "Scope" }; 

    public override void OnInspectorGUI()
    {
        var targetType = target.GetType();

        foreach (var name in TargetClassNames)
        {
            if (targetType.Name == name)
            {
                EditorGUILayout.LabelField(targetType.FullName, EditorStyles.boldLabel);
                EditorGUILayout.Space();
                break;
            }
        }

        DrawDefaultInspector();
    }
}