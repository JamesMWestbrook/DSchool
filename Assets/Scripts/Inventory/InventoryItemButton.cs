using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QFSW.BA.QGUI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Inventory inventory;
    public Item item;
    public Image image;
    public int buttonIndex;
    public int itemIndex;
    public int VisibleIndex;
    public Axis axis;
    public Vector2 moveValue;
    public Vector2 incrementValue;
    public ItemType itemType;
    public enum ItemType
    {
        Usable,
        Key,
        Combat
    }

    public enum Axis
    {
        Vertical,
        Horizontal
    }
    public void Awake()
    {
        inventory = GetComponentInParent(typeof(Inventory)) as Inventory;
    }
    public void SetGraphic()
    {
        if (item != null)
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
        inventory.ItemName.text = item.Name;
        TextMeshProUGUI text = inventory.ItemName;
        LeanTween.value(text.gameObject, (float x) => text.maxVisibleCharacters = (int)x, 0, text.text.Length, 0.3f);


        LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
        inventory.SetButton(buttonIndex);
        if (buttonIndex == 0 && inventory.Items.Count >= 8)//moving left/down
        {
            
                moveValue.x = -150;
                incrementValue.x = 50;
                             //1     2       3           4           5  6  7
            inventory.ScrollButtons(moveValue, incrementValue, 0, 8, -1, new Vector2(-200,0));
            //            inventory.ScrollButtons(Movement);
        }

        else if (buttonIndex == 8 && inventory.Items.Count >= 8)//moving right/up
        {
            
                moveValue.x = -200;
                incrementValue.x = 50;
           
            inventory.ScrollButtons(moveValue, incrementValue, 8, 0, 1, new Vector2(200, 0));
        }


    }
    public void OnDeselect(BaseEventData eventData)
    {
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

        SerializedProperty buttonIndex = serializedObject.FindProperty("buttonIndex");
        EditorGUILayout.PropertyField(buttonIndex);

        SerializedProperty index = serializedObject.FindProperty("itemIndex");
        EditorGUILayout.PropertyField(index);

        serializedObject.ApplyModifiedProperties();
    }




}