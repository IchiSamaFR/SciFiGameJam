using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : InventoryContainer
{
    [System.Serializable]
    public struct ShopItemStat
    {
        public string Id;
        public bool Buy;
        public bool Sell;
        public int PriceDiff;
    }

    private PlayerInventory targetInv;

    [SerializeField]
    private int money = 100;
    [SerializeField]
    private List<Item> itemsInv = new List<Item>();
    [SerializeField]
    private List<ShopItemStat> itemsAble = new List<ShopItemStat>();

    [Header("UI Objects")]
    [SerializeField]
    private GameObject UIInv;
    private UIContainer container;

    private bool isOpen = true;

    private ItemCollection itemCollection { get => ItemCollection.instance; }
    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public List<ShopItemStat> ItemsAble { get => itemsAble; set => itemsAble = value; }

    void Start()
    {
        SetInv();
        container = Instantiate(UIInv, GameObject.Find("MainCanvas").transform)
                    .GetComponent<UIContainer>();
        Close();
    }

    #region -- Open Close --
    /* Set of the shop inventory by ItemsStat and ItemAlready In
     */
    void SetInv()
    {
        for (int i = 0; i < itemsInv.Count; i++)
        {
            if (itemsInv[i].Name == "" || itemsInv[i].Type == "" || itemsInv[i].DropPrefab == null)
            {
                Item wait = itemsInv[i];
                itemsInv[i] = itemCollection.GetItem(itemsInv[i].Id);
                itemsInv[i].Amount = wait.Amount;
            }
        }

        foreach (ShopItemStat item in itemsAble)
        {
            bool find = false;
            foreach (Item itemIn in itemsInv)
            {
                if (item.Id == itemIn.Id)
                {
                    find = true;
                    break;
                }
            }
            if (!find && itemCollection.ItemExist(item.Id))
            {
                itemsInv.Add(itemCollection.GetItem(item.Id));
            }
        }
        itemsInv.Sort((x, y) => x.Name.CompareTo(y.Name));
    }

    /* Open UI Shop
     */
    public void Open(PlayerInventory target = null)
    {
        if (isOpen || !target)
        {
            return;
        }
        
        targetInv = target;
        targetInv.Open();
        targetInv.SetShop(this);

        isOpen = true;
        UIButtonsManager.instance.ButtonActive("shop");


        container.Open();
        container.SetInventoryContainer(this);
        RefreshUI();
        RefreshMoney();
    }

    /* Close UI Shop
     */
    public void Close()
    {
        isOpen = false;
        UIButtonsManager.instance.ButtonUnactive("shop");

        if (targetInv)
        {
            targetInv.ResetShop(this);
            targetInv = null;
        }
        container.Close();
    }
    #endregion
    
    public override void ItemButtonAction(Item item, bool all)
    {
        int amount = 0;
        if (all)
            amount = item.Amount;
        else
            amount = 1;

        Item toSend = new Item(item);

        int costUnit = toSend.Price + (int)((float)toSend.Price * (float)GetPrice(item.Id) / 100);

        int returned = targetInv.Buy(toSend, amount, costUnit, this);

        RemoveItem(item, amount - returned);
    }
    
    public int Buy(Item item, int amount, PlayerInventory inv)
    {
        int costUnit = item.Price + (int)((float)item.Price * (float)GetPrice(item.Id) / 100);
        int canBuy = (int)(money / costUnit);
        int rest = 0;

        if (amount < canBuy)
        {
            item.Amount = amount;
            GetItem(item);
            PayMoney(amount * costUnit);
            inv.GetMoney(amount * costUnit);

            rest = 0;
        }
        else
        {
            item.Amount = canBuy;
            GetItem(item);
            PayMoney(canBuy * costUnit);
            inv.GetMoney(canBuy * costUnit);

            rest = amount - canBuy;
        }


        return rest;
    }

    #region -- Money --

    public int GetPrice(string id)
    {
        foreach (var item in itemsAble)
        {
            if (item.Id == id)
            {
                return item.PriceDiff;
            }
        }
        return 0;
    }

    public bool PayMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
            RefreshMoney();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetMoney(int amount)
    {
        money += amount;
        RefreshMoney();
    }
    #endregion


    /* Add item in the shop inventory
     */
    public void GetItem(Item item)
    {
        if (item.Amount == 0)
        {
            return;
        }

        foreach (Item _item in itemsInv)
        {
            if (item.Id == _item.Id)
            {
                // Force add amount
                item.Amount = _item.ForceAddAmount(item.Amount);

                if (item.Amount == 0)
                {
                    break;
                }
            }
        }

        if (item.Amount > 0)
        {
            itemsInv.Add(item);
        }
        RefreshUI();
    }

    /* Add item in the shop inventory
     */
    public void RemoveItem(Item item, int amount)
    {
        if (amount == 0)
        {
            return;
        }

        foreach (Item _item in itemsInv)
        {
            if (item == _item)
            {
                amount = _item.RemoveAmount(amount);

                if (amount == -1)
                {
                    break;
                }
            }
        }

        RefreshUI();
    }


    private void RefreshUI()
    {
        if(isOpen)
            container.RefreshContainer(itemsInv, true, itemsAble);
    }


    private void RefreshMoney()
    {
        container.RefreshMoney(money);
    }
}
