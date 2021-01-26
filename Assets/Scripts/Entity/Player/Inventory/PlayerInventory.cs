using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    List<Item> items = new List<Item>();

    [Header("UI Objects")]
    [SerializeField]
    private GameObject UIInv;
    private UIContainer container;
    private bool isOpen = true;
    private int money;
    private ShopInventory shopInventory;

    public int Money { get => money; set => money = value; }
    public bool IsOpen { get => isOpen; set => isOpen = value; }

    void Start()
    {
        container = Instantiate(UIInv, GameObject.Find("MainCanvas").transform)
                    .GetComponent<UIContainer>();
        Close();
    }
    private void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            RemoveItem("ressource_iridium", 10);
        }
    }

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
        container.RefreshContainer(items);
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

    /* Add item in inventory
     */
    public void GetItem(Item item)
    {
        if (item.Amount == 0)
            return;
        
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
            items.Add(item);

        if(isOpen)
            container.RefreshContainer(items);
    }


    /* Add item in the shop inventory
     */
    public void RemoveItem(string id, int amount)
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
            if (id == _item.Id)
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

        container.RefreshContainer(items);
    }
}
