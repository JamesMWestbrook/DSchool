using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool InMenu;
   [HideInInspector] public static Inventory instance;
    [SerializeField] private GameObject InvButton;
    public Image HorizontalPanel;
    public Image CenterPoint;
    public List<GameObject> ButtonsHor;
    public List<GameObject> ButtonsVert;
    [SerializeField] private Image RightPoint;
    public List<Item> Items;
    public List<Item> CombatItems;
    public List<Item> KeyItems;
    public int CurrentButton;
    // Start is called before the first frame update
    public MenuActions actions;


    public bool LockedInput;
    bool OpenedItems;
    bool OpenedWeapons;
    public int SelectedItem;
    int SelectedWeapon;

    public ListType currentMenu;
    public enum ListType
    {
        Items,
        Combat,
        KeyItems
    }


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        HorizontalPanel.gameObject.SetActive(false);
        actions = MenuActions.CreateWithAllBindings();
    }
    private GameObject CurrentlySelected;
    void Update()
    {
        if (InMenu)
        {
            if(EventSystem.current.currentSelectedGameObject != null)
            {
                CurrentlySelected = EventSystem.current.currentSelectedGameObject;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(CurrentlySelected);
            }
        }
        if (LockedInput) return;

        if (actions.Horizontal.WasPressed)
        {
            if (currentMenu == ListType.Combat)
            {
                //switch to Items
            }
            else
            {
                //running this directly from the button now
                //ScrollButtons(true);
            }
        }
        else if (actions.Vertical.WasPressed)
        {

        }
        else if (actions.KeySwitch.WasPressed)
        {
            if (currentMenu == ListType.Items)
            {

            }
            else if (currentMenu == ListType.KeyItems)
            {

            }

        }
    }


    IEnumerator LockInputTimer(float time)
    {
        LockedInput = true;
        yield return new WaitForSeconds(time);
        LockedInput = false;
    }
    public void OpenInventory()
    {
        InMenu = true;
        StartCoroutine(LockInputTimer(4f));
        if (OpenedItems)
        {

        }
        else
        {
            CreateButtons(true, ListType.Items);
        }
        HorizontalPanel.gameObject.SetActive(true);
        HorizontalPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(HorizontalPanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);
        OpenedItems = true;
    }

    public void CreateButtons(bool Horizontal, ListType listType)
    {
        List<GameObject> Buttons = new List<GameObject>();
        List<Item> _Items = new List<Item>();

        switch (listType)
        {
            case ListType.Items:
                Buttons = ButtonsHor;
                _Items = Items;
                break;
            case ListType.Combat:
                break;
            case ListType.KeyItems:
                break;
        }

        int q = -150;
        int index = 0;


        for (int i = 0; i <= 8; i++)
        {
            Buttons[i].SetActive(true);
            InventoryItemButton Button = Buttons[i].GetComponent<InventoryItemButton>();
            Button.buttonIndex = i;

            if (i == 0 || i == 8 || i >= Items.Count)
            {
                Button.item = null;
                Button.SetGraphic();
                if (Items.Count < 8)
                {
                    Buttons[i].SetActive(false);
                }
                continue;
            }

            Button.transform.position = CenterPoint.transform.position;
            if (Horizontal) LeanTween.moveLocalX(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            else LeanTween.moveLocalY(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            //LeanTween.move(Button.gameObject, new Vector2(xMovement, yMovement), 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);

            if (Horizontal) q += 50;
            else q += 50;
            Button.item = Items[i];
            Button.itemIndex = i;
            Button.SetGraphic();
            if (i < 6)
            {
                index++;
            }
        }

        if (currentMenu == ListType.Combat)
        {
            SelectedItem = index;

        }
        else
        {
            SelectedWeapon = index;

        }

        Buttons[index].GetComponent<Button>().Select();
        Buttons[index].GetComponent<InventoryItemButton>().OnSelect(null);
    }

    public void ScrollButtons(bool Right = true)
    {
        StartCoroutine(LockInputTimer(0.7f));
        List<GameObject> Buttons = new List<GameObject>();
        List<Item> items = new List<Item>();
        int index = 0;
        bool Horizontal = false;
        float q = -250f;
        switch (currentMenu)
        {
            case ListType.Combat:
                Horizontal = false;
                items = CombatItems;
                index = SelectedWeapon;
                Buttons = ButtonsVert;

                if (Right)
                {

                }
                else
                {

                }
                break;
            default:
                Horizontal = true;
                index = SelectedItem;
                items = Items;
                Buttons = ButtonsHor;
                if (Right)
                {
                    q = -200f;
                }
                else
                {
                    q = 200f;
                }
                break;
        }
        StartCoroutine(LockButtons(Buttons));
        if (Right)//Right
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Horizontal)
                {
                    if (i == 0) continue;
                    LeanTween.moveLocalX(Buttons[i], q, 0.3f);
                    q += 50;
                }
                else
                {

                }
                //  else LeanTween.moveLocalY(Buttons[i], movement, 0.3f);
            }

            if (Horizontal)
            {
                
                InventoryItemButton newItemButton = Buttons[8].GetComponent<InventoryItemButton>();
                if(newItemButton.itemIndex < items.Count)
                {
                    newItemButton.item = items[newItemButton.itemIndex];

                }
                else //if we already hit the max
                {
                    newItemButton.item = items[0];
                    newItemButton.itemIndex = 0;
                }


                newItemButton.SetGraphic();
                LeanTween.moveLocalX(Buttons[0], 200, 0.0f);
                GameObject button = Buttons[0];
                Buttons.RemoveAt(0);
                Buttons.Add(button);
                InventoryItemButton movedButton = Buttons[8].GetComponent<InventoryItemButton>();
                movedButton.item = null;
                movedButton.SetGraphic();
                movedButton.itemIndex = newItemButton.itemIndex + 1;
                RedoButtonIndex();
            }
            else
            {

            }

        }
        else//Left
        {
            for (int i = 8; i > -1; i--)
            {
                if (Horizontal)
                {
                    if (i == 8) continue;
                    LeanTween.moveLocalX(Buttons[i], q, 0.3f);
                    q -= 50;
                }
                else
                {

                }
                //  else LeanTween.moveLocalY(Buttons[i], movement, 0.3f);
            }

            if (Horizontal)
            {

                LeanTween.moveLocalX(Buttons[8], -200, 0.0f);
                GameObject button = Buttons[8];
                Buttons.RemoveAt(8);
                Buttons.Insert(0, button);
                Buttons[0].GetComponent<InventoryItemButton>().item = null;
                Buttons[0].GetComponent<InventoryItemButton>().SetGraphic();
                RedoButtonIndex();


            }
            else
            {

            }
        }

        foreach(GameObject button in Buttons)
        {

        }
    }

    public IEnumerator LockButtons(List<GameObject> Buttons)
    {
        foreach (GameObject button in Buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(0.4f);
        foreach (GameObject button in Buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
    void RedoButtonIndex()
    {
        switch (currentMenu)
        {
            case ListType.Items:
                for(int i = 0; i < ButtonsHor.Count; i++)
                {
                    ButtonsHor[i].GetComponent<InventoryItemButton>().buttonIndex = i;
                }
                break;
            case ListType.Combat:

                break;
        }

    }
        public void SetButton(int newInt)
    {
        if (currentMenu == ListType.Combat)
        {
            SelectedWeapon = newInt;
        }
        else
        {
            SelectedItem = newInt;
        }
    }

    
}