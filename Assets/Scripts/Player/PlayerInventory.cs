using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    List<Item> items = new List<Item>();
    
    void Start()
    {
        
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
    }
}
