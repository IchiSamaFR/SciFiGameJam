using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIContainer : MonoBehaviour
{
    private InventoryContainer inventoryContainer;
    [SerializeField]
    private GameObject uiInv;
    [SerializeField]
    private GameObject uiItemInv;
    [SerializeField]
    private Transform uiContentInv;
    [SerializeField]
    private TextMeshProUGUI txtMoney;

    public GameObject UIInv { get => uiInv; set => uiInv = value; }
    public GameObject UIItemInv { get => uiItemInv; set => uiItemInv = value; }
    public Transform UIContentInv { get => uiContentInv; set => uiContentInv = value; }


    public void SetInventoryContainer(InventoryContainer inventoryContainer)
    {
        this.inventoryContainer = inventoryContainer;
    }

    public void RefreshMoney(int amount)
    {
        txtMoney.text = amount + "c";
    }

    public void RefreshContainer(List<Item> itemsInv, bool sellBuy = false)
    {

        int toGet = itemsInv.Count;
        int get = 0;

        /* Replace data from all slot already create
         */
        foreach (Transform item in uiContentInv)
        {
            if(get < toGet)
            {
                item.GetComponent<ItemInv>().Set(itemsInv[get], sellBuy);
                get++;
            }
            else
            {
                break;
            }
        }

        if(get < toGet)
        {
            int r = 0;
            foreach (var item in itemsInv)
            {
                if (get <= r)
                {
                    ItemInv slot = Instantiate(UIItemInv, UIContentInv).GetComponent<ItemInv>();
                    slot.SetParent(this);
                    slot.Set(item, sellBuy);
                }
                r++;
            }
        }
        else if (get >= toGet)
        {
            int r = 0;
            foreach (Transform item in UIContentInv)
            {
                if (toGet <= r)
                {
                    Destroy(item.gameObject);
                }
                r++;
            }
        }
    }

    public void Open()
    {
        uiInv.SetActive(true);
    }
    public void Close()
    {
        uiInv.SetActive(false);
    }

    public void ItemButtonAction(Item item, bool all = false)
    {
        inventoryContainer.ItemButtonAction(item, all);
    }
}
