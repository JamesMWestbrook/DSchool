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
        HorizontalPanel.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(HorizontalPanel.gameObject, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutQuad);
        if (Items.Count <= 7)
        {
            //total - total/2 is the one put in center
            int total = Items.Count - 1;
            int centerIndex = Items.Count - 1 - total / 2;
            
            int q = 0;
            for (int i = centerIndex - 1; i >= 0; i--)
            {
                SpawnButton(i, (q + 1) * -50);
                q++;
            }
            q = 0;
            for(int i = centerIndex + 1; i < Items.Count; i++)
            {
                SpawnButton(i, (q + 1) * 50);
                q++;
            }
            SpawnButton(centerIndex, 0);
        }
        else //more than 7 items aka more than visible in inventory at once
        {

        }
    }

    void SpawnButton(int index, int distance = 0)
    {
        GameObject buttonGO = Instantiate(InvButton, HorizontalPanel.transform) as GameObject;
        buttonGO.transform.position = CenterPoint.transform.position;
        InventoryItemButton button = buttonGO.GetComponent<InventoryItemButton>();
        button.item = Items[index];
        button.index = index;
        button.SetGraphic();
        //LeanTween.scale(buttonGO, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalX(buttonGO, distance, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(0.7f);

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
