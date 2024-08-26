using System.Text;
using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ItemType
{
    None,
    Rare,
    Unique,
    Epic,
    Legend,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public string itemDescription;
    public string itemDefine;
    public string itemID;
    public int goldGeneration;

    [Range(0, 100)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        if (itemType == ItemType.None)
        {
            sb.Clear();
            sb.Append(itemDescription);
            return sb.ToString();
        }
        else
        return "";
    }
}
