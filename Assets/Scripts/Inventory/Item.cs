using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Items/Item", order = 0)]
public class Item : ScriptableObject
{
    public Mesh Model;
    public Sprite Icon;
    public string Name;
    public string Description;

    public bool Usable;
    public bool Mixable;
    public bool Stackable = true;
    public ItemType itemType;
    public enum ItemType
    {
        Normal,
        Key
    }

    public int KeyIndex;
    public bool NameIsKnown = true;
    public string AssummedDescription;
}
