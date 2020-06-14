using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Button button;
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
        button = GetComponent<Button>();
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


        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.1f);

        ColorBlock col = button.colors;
        col.colorMultiplier = 5;
        button.colors = col;

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
        inventory.ItemName.text = item.Name;
        TextMeshProUGUI text = inventory.ItemName;
        LeanTween.value(text.gameObject, (float x) => text.maxVisibleCharacters = (int)x, 0, text.text.Length, 0.3f);

        inventory.SetButton(buttonIndex);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.1f);
        ColorBlock col = button.colors;
        col.colorMultiplier = 1;
        button.colors = col;
    }
}
