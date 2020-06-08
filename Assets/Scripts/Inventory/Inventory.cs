using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
  [SerializeField]  private GameObject InvButton;
    public Image HorizontalPanel;
    public Image CenterPoint;
    public List<Item> Items;
    public List<CombatItem> CombatItems;
    // Start is called before the first frame update

    
    public void ShowInventory()
    {
        HorizontalPanel.enabled = true;


        //4 is center one
        if (Items.Count <= 7)
        {
            //total - 4/2 is the one put in center
            int total = Items.Count - 1;
            int centerIndex = Items.Count - 1 - 4 / 2;
            SpawnButton(centerIndex, 0);

            for (int i = centerIndex - 1; i >= 0; i--)
            {
                SpawnButton(i, i * 50);
            }


        }
        else //more than 7 items aka more than visible in inventory at once
        {

        }
    }

    void SpawnButton(int index, int distance = 0)
    {
        GameObject centerButton = Instantiate(InvButton, HorizontalPanel.transform) as GameObject;
        centerButton.transform.position = CenterPoint.transform.position;
        LeanTween.moveLocalX(centerButton, distance, 1f);
        InventoryItemButton button = centerButton.GetComponent<InventoryItemButton>();
        button.item = Items[index];
        button.SetGraphic();
        
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
        HorizontalPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
