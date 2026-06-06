using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public int itemCount;
    public int itemID;
}

public static class ScriptableObjectExtension
{
    public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
    {
        if(scriptableObject == null)
        {
            Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
            return (T)ScriptableObject.CreateInstance(typeof(T));
        }

        T instance = Object.Instantiate(scriptableObject);
        instance.name = scriptableObject.name;
        return instance;
    }
}

