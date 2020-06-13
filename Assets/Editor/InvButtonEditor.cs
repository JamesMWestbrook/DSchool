using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


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

        SerializedProperty buttonIndex = serializedObject.FindProperty("buttonIndex");
        EditorGUILayout.PropertyField(buttonIndex);

        SerializedProperty index = serializedObject.FindProperty("itemIndex");
        EditorGUILayout.PropertyField(index);

        serializedObject.ApplyModifiedProperties();
    }




}