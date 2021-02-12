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

    [SerializeField]
    private GameObject prebaStatsPanel;

    private GameObject statsPanel;

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
    public void RefreshContainer(List<Item> itemsInv, bool sellBuy = false, List<ShopInventory.ShopItemStat> shopItemStats = null)
    {

        int toGet = itemsInv.Count;
        int get = 0;

        /* Replace data from all slot already create
         */
        foreach (Transform item in uiContentInv)
        {
            if(get < toGet)
            {
                if (sellBuy)
                {
                    int price = 0;
                    if (shopItemStats != null)
                    {
                        foreach (var itemStat in shopItemStats)
                        {
                            if (itemStat.Id == itemsInv[get].Id)
                            {
                                price = itemsInv[get].Price +
                                        (int)((float)itemsInv[get].Price * (float)itemStat.PriceDiff / 100);
                                break;
                            }
                        }
                    }

                    item.GetComponent<ItemInv>().Set(itemsInv[get], prebaStatsPanel, sellBuy, price);
                }
                else
                {
                    item.GetComponent<ItemInv>().Set(itemsInv[get], prebaStatsPanel);
                }
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
                    if (sellBuy)
                    {
                        int price = 0;
                        if (shopItemStats != null)
                        {
                            foreach (var itemStat in shopItemStats)
                            {
                                if (itemStat.Id == itemsInv[r].Id)
                                {
                                    price = itemsInv[r].Price +
                                            (int)((float)itemsInv[r].Price * (float)itemStat.PriceDiff / 100);
                                    break;
                                }
                            }
                        }

                        ItemInv slot = Instantiate(UIItemInv, UIContentInv).GetComponent<ItemInv>();
                        slot.SetParent(this);
                        slot.Set(item, prebaStatsPanel, sellBuy, price);
                    }
                    else
                    {
                        ItemInv slot = Instantiate(UIItemInv, UIContentInv).GetComponent<ItemInv>();
                        slot.SetParent(this);
                        slot.Set(item, prebaStatsPanel);
                    }
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
        if (statsPanel)
        {
            Destroy(statsPanel);
        }
    }

    public void ItemButtonAction(Item item, bool all = false)
    {
        inventoryContainer.ItemButtonAction(item, all);
    }

    public void SetItemStatsOpen(GameObject newStatsPanel)
    {
        if (statsPanel)
        {
            Destroy(statsPanel);
        }
        statsPanel = newStatsPanel;
    }
}
