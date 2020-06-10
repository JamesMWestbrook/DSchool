using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] private GameObject InvButton;
    public Image HorizontalPanel;
    public Image CenterPoint;
    [SerializeField] private Image RightPoint;
    public List<Item> Items;
    public List<CombatItem> CombatItems;
    public int CurrentButton;
    // Start is called before the first frame update
    public MenuActions actions;

    bool EightItems;

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
    void Update()
    {
        if (actions.Horizontal.WasPressed)
        {
            Debug.Log("Was pressed");
            if (actions.Left.WasPressed)
            {

            }
            else if (actions.Right.WasPressed)
            {

            }
        }
    }

    bool LockedInput;
    bool OpenedItems;

    public List<GameObject> ButtonsHor;
    public List<GameObject> ButtonsVert;

    public ListType currentMenu;
    public enum ListType
    {
        Items,
        Combat,
        KeyItems
    }
    IEnumerator LockInputTimer(float time)
    {

        yield return new WaitForSeconds(time);
        LockedInput = false;
    }
    public void OpenInventory()
    {

        if (OpenedItems)
        {

        }
        else
        {
            CreateButtons(true, ListType.Items);
        }
        StartCoroutine(LockInputTimer(0.5f));
        HorizontalPanel.gameObject.SetActive(true);
        HorizontalPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(HorizontalPanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);
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
        for(int i = 0; i <= 8; i++)
        {
            Buttons[i].SetActive(true);
            InventoryItemButton Button = Buttons[i].GetComponent<InventoryItemButton>();
            Button.index = i;

            if(i == 0 || i == 8 || i >= Items.Count)
            {
                Button.item = null;
                Button.SetGraphic();
                if(Items.Count < 8)
                {
                    Buttons[i].SetActive(false);
                }
                continue;
            }

            Button.transform.position = CenterPoint.transform.position;
           if(Horizontal) LeanTween.moveLocalX(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
           else LeanTween.moveLocalY(Button.gameObject, q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            //LeanTween.move(Button.gameObject, new Vector2(xMovement, yMovement), 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);

            if (Horizontal) q += 50;
            else q += 50;
            Button.item = Items[i];
            Button.SetGraphic();
            if (i < 6) Button.GetComponent<Button>().Select();
        }

    
    }
    /*  public void ShowInventory()
  {


      int q = -200;

      foreach (GameObject button in Buttons)
      {
          button.GetComponent<InventoryItemButton>().item = null;
          button.SetActive(false);
      }
      for ( int i = 0;  i < 8; i++)
      {
          Buttons[i].SetActive(true);
          if(EightItems && i == 0 || EightItems && i == 8 || Items.Count >= 8 && i == 0 || Items.Count >= 8 && i >= 8)
          {

          }
          else
          {
              Buttons[i].transform.position = CenterPoint.transform.position;
              LeanTween.moveLocalX(Buttons[i], q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
          }
          q += 50;
          InventoryItemButton button = Buttons[i].GetComponent<InventoryItemButton>();
          if (Items[i] != null)
          {
              button.item = Items[i];
              button.SetGraphic();
              button.index = i;

              if(i == 0 && EightItems)
              {
                  Buttons[8].SetActive(true);
                  button = Buttons[8].GetComponent<InventoryItemButton>();
                  button.item = Items[0];
                  button.SetGraphic();
              }
          }
      }
      Buttons[4].GetComponent<Button>().Select();

  }*/

    void CreateAllButtons()
    {
        //total - total/2 is the one put in center
        int total = Items.Count - 1;
        int centerIndex = Items.Count - 1 - total / 2;

        int q = centerIndex - 1;
        for (int i = 0; i <= centerIndex - i; i++)
        {
            SpawnButton(i, (q + 1) * -50);
            q--;
        }
        q = 0;
        for (int i = centerIndex + 1; i < Items.Count; i++)
        {
            SpawnButton(i, (q + 1) * 50);
            q++;
        }
        SpawnButton(centerIndex, 0, true);
    }
    void SpawnButton(int index, int distance = 0, bool insertIntoList = false)
    {
        GameObject buttonGO = Instantiate(InvButton, HorizontalPanel.transform) as GameObject;
        buttonGO.transform.position = CenterPoint.transform.position;
        InventoryItemButton button = buttonGO.GetComponent<InventoryItemButton>();
        button.item = Items[index];
        button.index = index;
        button.SetGraphic();
        //LeanTween.scale(buttonGO, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalX(buttonGO, distance, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.7f);

        //   if (!insertIntoList) Buttons.Add(buttonGO);
        //    else Buttons.Insert(index, buttonGO);
    }
}

