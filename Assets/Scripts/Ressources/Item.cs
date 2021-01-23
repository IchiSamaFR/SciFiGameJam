using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public string Id;
    public string Name;
    public GameObject Prefab;
    public Sprite Sprite;
    public int Amount;
    public int MaxAmount;

    public bool Full => Amount / MaxAmount == 1 ? true : false;

    public Item(string Id, string Name, GameObject Prefab, Sprite Sprite, int MaxAmount = 100)
    {
        this.Id = Id;
        this.Name = Name;
        this.Prefab = Prefab;
        this.Sprite = Sprite;
        this.Amount = 0;
        this.MaxAmount = MaxAmount;
    }

    public Item(Item item)
    {
        this.Id = item.Id;
        this.Name = item.Name;
        this.Prefab = item.Prefab;
        this.Sprite = item.Sprite;
        this.Amount = item.Amount;
        this.MaxAmount = item.MaxAmount;
    }

    public int AddAmount(int amount)
    {
        Amount += amount;

        if (Amount > MaxAmount)
        {
            int diff = Amount - MaxAmount;
            Amount = MaxAmount;
            return diff;
        }

        return 0;
    }
}
