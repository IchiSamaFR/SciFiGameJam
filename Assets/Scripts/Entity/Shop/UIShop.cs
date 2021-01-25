using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    [System.Serializable]
    public struct ShopItemStat
    {
        public string Id;
        public bool Buy;
        public bool Sell;
        public float PriceDiff;
    }

    private PlayerInventory targetInv;
    private ItemCollection itemCollection { get => ItemCollection.instance; }

    [SerializeField]
    List<Item> itemsInv = new List<Item>();
    [SerializeField]
    List<ShopItemStat> itemsAble = new List<ShopItemStat>();

    [Header("UI Objects")]
    [SerializeField]
    private GameObject UIInv;
    private UIContainer container;

    private bool isOpen = false;

    void Start()
    {
        SetInv();
        Open(null);
    }

    /* Set of the shop inventory by ItemsStat and ItemAlready In
     */
    void SetInv()
    {
        for (int i = 0; i < itemsInv.Count; i++)
        {
            if (itemsInv[i].Name == "" || itemsInv[i].Type == "" || itemsInv[i].Prefab == null)
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
            if (!find)
            {
                print(item.Id + " added");
                itemsInv.Add(itemCollection.GetItem(item.Id));
            }
        }
        itemsInv.Sort((x, y) => x.Name.CompareTo(y.Name));
    }

    /* Open UI Shop
     */
    public void Open(PlayerInventory target)
    {
        if(container == null)
        {
            container = Instantiate(UIInv, GameObject.Find("MainCanvas").transform).GetComponent<UIContainer>();
        }

        if (isOpen || target)
        {
            Close();
            return;
        }

        targetInv = target;

        isOpen = true;

        container.Open();
        container.RefreshContainer(itemsInv);
    }

    /* Close UI Shop
     */
    public void Close()
    {
        if (targetInv)
        {
            targetInv = null;
        }

        isOpen = false;
        container.Close();
    }

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
            if (item.Id == _item.Id && !_item.Full)
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
            itemsInv.Add(item);
        }
        container.RefreshContainer(itemsInv);
    }
}
