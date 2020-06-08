using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QFSW.BA.QGUI;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
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
    public void SetGraphic()
    {
        image.sprite = item.Icon;
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