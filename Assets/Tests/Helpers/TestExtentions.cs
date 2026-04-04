using MechanicsPlayground.Core;
using System.Reflection;
using TMPro;
using UnityEngine;

public static class TestExtensions
{
    public static void SetPrivateField<T>(this T obj, string fieldName, object value) where T : MonoBehaviour
    {
        var field = typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        field.SetValue(obj, value);
    }

    public static void CreateComponent<T,V>(this T obj, string fieldName, bool asChildGameObject = false) where T : MonoBehaviour where V : Component
    {
        GameObject targetGo;
        if (asChildGameObject) 
        {
            targetGo = new GameObject(typeof(V).Name);
            targetGo.transform.SetParent(obj.transform);
        }
        else
        {
            targetGo = obj.gameObject;
        }
        
        var titleTextMesh = targetGo.AddComponent<V>();
        obj.SetPrivateField(fieldName, titleTextMesh);
    }
}