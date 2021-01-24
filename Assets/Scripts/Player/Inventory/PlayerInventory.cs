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
    [SerializeField]
    private GameObject UIitemInv;
    [SerializeField]
    private Transform UIContentInv;

    private bool isOpen = false;

    void Start()
    {
        Close();
    }

    public void Open()
    {
        if (isOpen)
        {
            Close();
            return;
        }
        isOpen = true;
        UIInv.SetActive(true);

        RefreshInv();
    }

    public void Close()
    {
        isOpen = false;
        UIInv.SetActive(false);
    }

    public void GetItem(Item item)
    {
        if (item.Amount == 0)
        {
            return;
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
        RefreshInv();
    }

    public void RefreshInv()
    {
        foreach (Transform item in UIContentInv)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            ItemInv slot = Instantiate(UIitemInv, UIContentInv).GetComponent<ItemInv>();
            slot.Set(item);
        }
    }
}
