using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : InventoryContainer
{
    [SerializeField]
    private List<string> itemsToAdd = new List<string>();
    [SerializeField]
    private List<Item> items = new List<Item>();

    [Header("UI Objects")]
    [SerializeField]
    private GameObject UIInv;
    private UIContainer container;
    private bool isOpen = true;
    [SerializeField]
    private int money = 100;
    private ShopInventory shopInventory;

    public int Money { get => money; set => money = value; }
    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public PlayerManager PlayerManager { get => GetComponent<PlayerManager>(); }
    public List<Item> Items { get => items; set => items = value; }
    public ShopInventory ShopInventory { get => shopInventory; set => shopInventory = value; }

    void Start()
    {
        
        container = Instantiate(UIInv, GameObject.Find("MainCanvas").transform)
                    .GetComponent<UIContainer>();
        Close();


        foreach (var item in itemsToAdd)
        {
            Item _item = ItemCollection.instance.GetItem(item);
            if (_item != null)
            {
                _item.Amount = 1;
                GetItem(_item);
            }
        }
    }
    private void Update()
    {

    }

    #region -- Open Close --
    /* Open UI inventory
     */
    public void Open(ShopInventory shop = null)
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        UIButtonsManager.instance.ButtonActive("bag");

        container.Open();
        container.SetInventoryContainer(this);
        RefreshUI();
        RefreshMoney();
    }

    /* Close UI inventory
     */
    public void Close()
    {
        if (!isOpen)
        {
            return;
        }

        isOpen = false;

        container.Close();
        
        UIButtonsManager.instance.ButtonUnactive("bag");
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
        

        int returned = shopInventory.Buy(toSend, amount, this);

        RemoveItem(item, amount - returned);
    }

    public int Buy(Item item, int amount, int costUnit, ShopInventory shop)
    {
        int canBuy = (int)(money / costUnit);
        int rest = 0;

        if (amount < canBuy)
        {
            item.Amount = amount;
            GetItem(item);
            PayMoney(amount * costUnit);
            shop.GetMoney(amount * costUnit);

            rest = 0;
        }
        else
        {
            item.Amount = canBuy;
            GetItem(item);
            PayMoney(canBuy * costUnit);
            shop.GetMoney(canBuy * costUnit);

            rest = amount - canBuy;
        }


        return rest;
    }

    /* Add item in inventory
     */
    public void GetItem(Item item, int amount = 0)
    {
        if (item.Amount == 0 && amount == 0)
            return;

        if(amount != 0)
        {
            item.Amount = amount;
        }

        foreach (Item _item in items)
        {
            if(item.Id == _item.Id && !_item.Full)
            {
                item.Amount = _item.AddAmount(item.Amount);

                if (item.Amount == 0)
                {
                    break;
                }
            }
        }

        if (item.Amount > 0)
        {
            items.Add(item);
        }

        RefreshUI();
    }


    #region -- Money --
    /* Add item in the shop inventory
     */
    public void RemoveItem(Item item, int amount)
    {
        if (amount == 0)
        {
            return;
        }

        
        List<Item> toDelete = new List<Item>();
        int ind = 0;
        for (int i = items.Count - 1; i >= 0; i--)
        {
            Item _item = items[i];
            if (item == _item)
            {
                amount = _item.RemoveAmount(amount);

                if (amount >= 0)
                {
                    toDelete.Add(_item);
                }
                if (amount == -1)
                {
                    break;
                }
            }
            ind++;
        }

        for (int i = 0; i < toDelete.Count; i++)
        {
            items.Remove(toDelete[i]);
        }

        RefreshUI();
    }

    public bool PayMoney(int amount)
    {
        if(amount <= money)
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

    #region -- Shop Set --
    /* Set a shop to interact
     */
    public void SetShop(ShopInventory shopInventory)
    {
        this.shopInventory = shopInventory;
        RefreshUI();
    }

    /* Reset shop to interact
     */
    public void ResetShop(ShopInventory shopInventory)
    {
        if(this.shopInventory == shopInventory)
        {
            this.shopInventory = null;

            container.RefreshContainer(items);
        }
    }
    #endregion


    private void RefreshUI()
    {
        if (isOpen)
        {
            if (shopInventory)
            {
                container.RefreshContainer(items, true, shopInventory.ItemsAble);
            }
            else
            {
                container.RefreshContainer(items);
            }
        }
    }
    private void RefreshMoney()
    {
        container.RefreshMoney(money);
    }
}
