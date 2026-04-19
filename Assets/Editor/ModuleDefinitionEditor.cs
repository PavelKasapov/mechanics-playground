/*#if UNITY_EDITOR
using MechanicsPlayground.Core;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VContainer.Unity;

[CustomEditor(typeof(ModuleDefinition))]
public class ModuleDefinitionEditor : Editor
{
    private string[] _typeNames;
    private int _selectedIndex;

    private void OnEnable()
    {
        var types = TypeCache.GetTypesDerivedFrom<LifetimeScope>()
            .Where(t => !t.IsAbstract && t.IsClass && !t.Assembly.GetName().Name.Contains("Tests") && !t.Assembly.GetName().Name.Contains("FeatureManagement"))
            .ToArray();
        _typeNames = types.Select(t => $"{t.FullName}, {t.Assembly.GetName().Name}").ToArray();

        var def = (ModuleDefinition)target;
        var scopeTypeProp = serializedObject.FindProperty("_scopeTypeName");
        string current = scopeTypeProp.stringValue;
        _selectedIndex = System.Array.IndexOf(_typeNames, current);
        if (_selectedIndex < 0) _selectedIndex = 0;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var scopeTypeProp = serializedObject.FindProperty("_scopeTypeName");

        int newIndex = EditorGUILayout.Popup("Scope Type", _selectedIndex, _typeNames);
        if (newIndex != _selectedIndex)
        {
            _selectedIndex = newIndex;
            scopeTypeProp.stringValue = _typeNames[newIndex];
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif*/