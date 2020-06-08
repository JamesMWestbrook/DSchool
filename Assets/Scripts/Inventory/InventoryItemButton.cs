using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QFSW.BA.QGUI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemButton : MonoBehaviour , ISelectHandler, IDeselectHandler
{
    [SerializeField] Inventory inventory;
    public ItemType itemType;
    public enum ItemType
    {
        Usable,
        Key,
        Combat
    }
    public Item item;
    public CombatItem combatItem;
    public Image image;
    public int index;
    public int VisibleIndex;

    public void Awake()
    {
        inventory = GetComponentInParent(typeof(Inventory)) as Inventory;
    }
    public void SetGraphic()
    {
        if(item != null)
        {
            image.sprite = item.Icon;

        }
        else
        {
            image.sprite = null;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
        Debug.Log(this.gameObject.name + " Was selected");
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was deselected");
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
    }
}

[CustomEditor(typeof(InventoryItemButton))]
public class InvButtonEditor : Editor
{
    public InventoryItemButton.ItemType ItemType;
    public override void OnInspectorGUI()
    {
        SerializedProperty itemType = serializedObject.FindProperty("itemType");
        EditorGUILayout.PropertyField(itemType);
        switch ((InventoryItemButton.ItemType)itemType.intValue)
//        switch (script.itemType)
        {
            case InventoryItemButton.ItemType.Usable:
                SerializedProperty item = serializedObject.FindProperty("item");
                EditorGUILayout.PropertyField(item);
                break;
            case InventoryItemButton.ItemType.Key:
               // SerializedProperty key = serializedObject.FindProperty("key");
                //EditorGUILayout.PropertyField(key);
                break;
            case InventoryItemButton.ItemType.Combat:
                SerializedProperty combatItem = serializedObject.FindProperty("combatItem");
                EditorGUILayout.PropertyField(combatItem);
                break;
        }

        SerializedProperty image = serializedObject.FindProperty("image");
        EditorGUILayout.PropertyField(image);

        SerializedProperty index = serializedObject.FindProperty("index");
        EditorGUILayout.PropertyField(index);

        serializedObject.ApplyModifiedProperties();
    }



    
}