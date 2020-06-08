using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
  [SerializeField]  private GameObject InvButton;
    public Image HorizontalPanel;
    public Image CenterPoint;
    [SerializeField] private Image RightPoint;
    public List<Item> Items;
    public List<CombatItem> CombatItems;
   [SerializeField] private List<GameObject> Buttons;
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

            }else if (actions.Right.WasPressed)
            {

            }
        }
    }

    public void ShowInventory()
    {
        HorizontalPanel.gameObject.SetActive(true);
        HorizontalPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(HorizontalPanel.gameObject, new Vector3(1, 1, 1), 0.3f).setEase(LeanTweenType.easeOutQuad);

        int q = -150;
        int i;
        int itemModifier;
        EightItems =  Items.Count == 8;
        if (Items.Count <= 7)
        {
            i = 1;
            itemModifier = 1;
        }
        else //more than 7 items (aka more than visible in inventory at once)
        {

            q = -200;
            i = 0;
            itemModifier = 0;
            if (Items.Count >= 9)
            {

            }
            else if (Items.Count == 8)
            {
                EightItems = true;
            }
        }
        Debug.Log("Eight items " + EightItems);
        foreach (GameObject button in Buttons)
        {
            button.GetComponent<InventoryItemButton>().item = null;
            button.SetActive(false);
        }
        for ( ;  i < Items.Count || i < 8; i++)
        {
            Buttons[i].SetActive(true);
            if(EightItems && i == 0 || EightItems && i == 8 || Items.Count >= 8 && i == 0 || Items.Count >= 8 && i >= 8)
            {
                if(EightItems && i >= 7 )
                {

                }
            }
            else
            {
                Buttons[i].transform.position = CenterPoint.transform.position;
                LeanTween.moveLocalX(Buttons[i], q, 0.3f).setEase(LeanTweenType.easeOutQuad).setDelay(0.3f);
            }

            q += 50;
            InventoryItemButton button = Buttons[i].GetComponent<InventoryItemButton>();
            Debug.Log(Items[i - itemModifier] + " " + i);
            if (Items[i - itemModifier] != null)
            {
                button.item = Items[i - itemModifier];
                button.SetGraphic();
                button.index = i - itemModifier;

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


    }



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

        if (!insertIntoList) Buttons.Add(buttonGO);
        else Buttons.Insert(index, buttonGO);
    }

  


}
