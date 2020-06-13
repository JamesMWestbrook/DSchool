using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
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
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI MenuName;
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
    void PrintBitch()
    {
        Debug.Log("Bitch");
    }
    private GameObject CurrentlySelected;
    void Update()
    {
        if (InMenu)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                CurrentlySelected = EventSystem.current.currentSelectedGameObject;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(CurrentlySelected);
            }
        }
        if (LockedInput || !InMenu) return;

        else if (actions.Vertical.WasPressed)
        {
            switch (currentMenu)
            {
                case ListType.Items:
                    if (actions.Up.WasPressed)
                    {
                        CreateButtons(ListType.Combat);
                    }
                    else if (
                       actions.Down.WasPressed)
                    {
                        CreateButtons(ListType.KeyItems);
                    }

                    break;
                case ListType.Combat:
                    if (actions.Up.WasPressed)
                    {
                        CreateButtons(ListType.KeyItems);

                    }
                    else if (
                       actions.Down.WasPressed)
                    {
                        CreateButtons(ListType.Items);
                    }
                    break;
                case ListType.KeyItems:
                    if (actions.Up.WasPressed)
                    {
                        CreateButtons(ListType.Items);
                    }
                    else if (
                       actions.Down.WasPressed)
                    {
                        CreateButtons(ListType.Combat);
                    }
                    break;
            }
        }
        else if (actions.KeySwitch.WasPressed)
        {
            if (currentMenu == ListType.Items)
            {
                CreateButtons(ListType.KeyItems);
                StartCoroutine(LockInputTimer(0.3f));
            }
            else if (currentMenu == ListType.KeyItems)
            {
                CreateButtons(ListType.Items);
                StartCoroutine(LockInputTimer(0.3f));
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
        StartCoroutine(LockInputTimer(0.5f));

        CreateButtons(ListType.Items);

        HorizontalPanel.gameObject.SetActive(true);
        HorizontalPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(HorizontalPanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);
    }

    public void CreateButtons(ListType listType, bool Horizontal = true)
    {
        List<GameObject> Buttons = new List<GameObject>();
        List<Item> _Items = new List<Item>();

        switch (listType)
        {
            case ListType.Items:
                Buttons = ButtonsHor;
                currentMenu = ListType.Items;
                MenuName.text = "Items";
                _Items = Items;
                break;
            case ListType.Combat:
                MenuName.text = "Weapons";

                currentMenu = ListType.Combat;
                break;
            case ListType.KeyItems:
                MenuName.text = "Key Items";

                Buttons = ButtonsHor;
                _Items = KeyItems;
                currentMenu = ListType.KeyItems;
                break;
        }
        //LeanTween.value(FG.gameObject, (Color x) => FG.color = x, FG.color, FGtarget, duration);
        LeanTween.value(MenuName.gameObject, (float x) => MenuName.maxVisibleCharacters = (int)x, 0, MenuName.text.Length, 0.3f);

        int q = -150;
        int t = 0;
        int index = 0;

        bool StopContinueOnce = true;

        for (int i = 0; i <= 8; i++)
        {
            Buttons[i].SetActive(true);
            InventoryItemButton Button = Buttons[i].GetComponent<InventoryItemButton>();
            Button.buttonIndex = i;

            if (i == 0 || i == 8 || i >= _Items.Count)
            {
                Button.item = null;
                Button.SetGraphic();


                if (_Items.Count < 8)
                {
                    Buttons[i].SetActive(false);
                }

                if (i >= _Items.Count && _Items.Count < 8 && StopContinueOnce)
                {
                    StopContinueOnce = false;
                    Buttons[i].SetActive(true);
                }
                else
                {
                    //ButtonOpacity(Buttons[i], 0f);

                    Color BGtarget = Button.GetComponent<Image>().color;
                    BGtarget.a = 0;
                    Image BG = Button.GetComponent<Image>();
                    BG.color = BGtarget;

                    Image FG = Button.transform.GetChild(0).GetComponent<Image>();
                    Color FGtarget = FG.color;
                    FGtarget.a = 0;
                    FG.color = FGtarget;

                    continue;

                }

            }

            Button.transform.position = CenterPoint.transform.position;
            //  if (Horizontal) LeanTween.moveLocalX(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            ButtonOpacity(Buttons[i], 1f);
            if (Horizontal) LeanTween.moveLocalX(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            else LeanTween.moveLocalY(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            //LeanTween.move(Button.gameObject, new Vector2(xMovement, yMovement), 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);


            if (Horizontal) q += 50;
            else q += 50;
            Button.item = _Items[t];
            Button.itemIndex = t;
            t++;
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
        // StartCoroutine(DelayedFunction(RedoButtonIndex, 0.5f));

    }
    public void ButtonOpacity(GameObject button, float EndOpacity, float duration = 0.5f)
    {
        Color BGtarget = button.GetComponent<Image>().color;
        BGtarget.a = EndOpacity;
        Image BG = button.GetComponent<Image>();
        LeanTween.value(BG.gameObject, (Color x) => BG.color = x, BG.color, BGtarget, duration);

        Image FG = button.transform.GetChild(0).GetComponent<Image>();
        Color FGtarget = FG.color;
        FGtarget.a = EndOpacity;
        LeanTween.value(FG.gameObject, (Color x) => FG.color = x, FG.color, FGtarget, duration);
    }

    public void ScrollButtons(Vector2 moveValue, Vector2 incrementValue, int VisibleButton, int MovedInvsButton, int ItemIndexModifier, Vector2 InvsDestination)
    {
        List<GameObject> _Buttons = new List<GameObject>();
        List<Item> _Items = new List<Item>();

        switch (currentMenu)
        {
            case ListType.Items:
                _Buttons = ButtonsHor;
                _Items = Items;
                break;
            case ListType.Combat:
                break;
            case ListType.KeyItems:
                _Buttons = ButtonsHor;
                _Items = KeyItems;
                break;
        }

        InventoryItemButton newItemButton = _Buttons[VisibleButton].GetComponent<InventoryItemButton>();

        if (VisibleButton == 8)
        { // right or up
            if (newItemButton.itemIndex < _Items.Count)

            {
                newItemButton.item = _Items[newItemButton.itemIndex];
            }
            else
            {
                //already hit minimum
                newItemButton.item = _Items[0];
                newItemButton.itemIndex = 0;
            }
        }
        else

        { //left or down
            if (newItemButton.itemIndex >= 0)
            {
                newItemButton.item = _Items[newItemButton.itemIndex];
            }
            else
            {
                //already passed minimum

                newItemButton.item = _Items[_Items.Count - 1];
                newItemButton.itemIndex = _Items.Count - 1;
            }
        }

        newItemButton.SetGraphic();


        StartCoroutine(LockInputTimer(0.3f));
        StartCoroutine(LockButtons(_Buttons));
        for (int i = 0; i < _Buttons.Count; i++)
        {
            if (i == MovedInvsButton) continue;
            if (i == VisibleButton) ButtonOpacity(_Buttons[i], 1f);

            LeanTween.moveLocal(_Buttons[i], new Vector2(moveValue.x, moveValue.y), 0.3f);
            moveValue.x += incrementValue.x;
            moveValue.y += incrementValue.y;

        }

        //    ButtonOpacity(_Buttons[MovedInvsButton], 0f);
        LeanTween.moveLocal(_Buttons[MovedInvsButton], InvsDestination, 0.0f);
        // LeanTween.moveLocal(_Buttons[MovedInvsButton], new Vector2(moveValue.x, moveValue.y), 0.0f);
        GameObject button = _Buttons[MovedInvsButton];
        //_Buttons.RemoveAt(0);
        _Buttons.RemoveAt(MovedInvsButton);
        if (VisibleButton == 8)
        { //Right
            _Buttons.Add(button);
        }
        else
        {//left
            _Buttons.Insert(0, button);
        }

        InventoryItemButton movedButton = _Buttons[VisibleButton].GetComponent<InventoryItemButton>();
        movedButton.item = null;
        movedButton.SetGraphic();
        movedButton.itemIndex = newItemButton.itemIndex + ItemIndexModifier;
        //RedoButtonIndex();
        StartCoroutine(DelayedFunction(RedoButtonIndex, 0.3f));

        ButtonOpacity(_Buttons[MovedInvsButton], 0f);
    }
    public IEnumerator DelayedFunction(Action bitch, float delay)
    {
        yield return new WaitForSeconds(delay);
        bitch();
    }

    public void OldScrollButtons(bool Right = true)
    {
        StartCoroutine(LockInputTimer(0.3f));
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
                    if (i == 8) ButtonOpacity(Buttons[i], 1f);
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
                if (newItemButton.itemIndex < items.Count)
                {
                    newItemButton.item = items[newItemButton.itemIndex];

                }
                else //if we already hit the max
                {
                    newItemButton.item = items[0];
                    newItemButton.itemIndex = 0;
                }
                newItemButton.SetGraphic();



                ButtonOpacity(Buttons[0], 0f);//both this and the following opacity change for Buttons[0] is necessary don't remove either
                LeanTween.moveLocalX(Buttons[0], 200, 0.0f);
                GameObject button = Buttons[0];
                Buttons.RemoveAt(0);
                Buttons.Add(button);
                InventoryItemButton movedButton = Buttons[8].GetComponent<InventoryItemButton>();
                movedButton.item = null;
                movedButton.SetGraphic();
                movedButton.itemIndex = newItemButton.itemIndex + 1;
                RedoButtonIndex();

                ButtonOpacity(Buttons[0], 0f);
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
                InventoryItemButton newItemButton = Buttons[0].GetComponent<InventoryItemButton>();
                if (newItemButton.itemIndex >= 0)
                {
                    newItemButton.item = items[newItemButton.itemIndex];

                }
                else //if we already hit the minimum
                {
                    int maxItem = items.Count - 1;
                    newItemButton.item = items[maxItem];
                    newItemButton.itemIndex = maxItem;
                }
                newItemButton.SetGraphic();

                LeanTween.moveLocalX(Buttons[8], -200, 0.0f);
                GameObject button = Buttons[8];
                Buttons.RemoveAt(8);
                Buttons.Insert(0, button);
                InventoryItemButton movedButton = Buttons[0].GetComponent<InventoryItemButton>();
                movedButton.item = null;
                movedButton.SetGraphic();
                movedButton.itemIndex = newItemButton.itemIndex - 1;
                RedoButtonIndex();



            }
            else
            {

            }
        }

        foreach (GameObject button in Buttons)
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
                for (int i = 0; i < ButtonsHor.Count; i++)
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

    public IEnumerator Bitch(TextMeshProUGUI text, string name, float duration = 0.3f)
    {
        yield return new WaitForSeconds(3) ;
    }
}
