using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public string Id;
    public string Name;
    public string Type;
    public GameObject Prefab;
    public Sprite Sprite;
    public int Amount;
    public int MaxAmount;
    public int Price;

    public bool Full => Amount / MaxAmount == 1 ? true : false;

    public Item(string Id, string Name, string Type, GameObject Prefab, Sprite Sprite, int MaxAmount = 100, int Price = 0)
    {
        this.Id = Id;
        this.Name = Name;
        this.Type = Type;
        this.Prefab = Prefab;
        this.Sprite = Sprite;
        this.Amount = 0;
        this.MaxAmount = MaxAmount;
        this.Price = Price;
    }

    public Item(Item item)
    {
        this.Id = item.Id;
        this.Name = item.Name;
        this.Type = item.Type;
        this.Prefab = item.Prefab;
        this.Sprite = item.Sprite;
        this.Amount = item.Amount;
        this.MaxAmount = item.MaxAmount;
        this.Price = item.Price;
    }

    public int AddAmount(int amount)
    {
        Amount += amount;

        if (Amount > MaxAmount && MaxAmount > 0)
        {
            int diff = Amount - MaxAmount;
            Amount = MaxAmount;
            return diff;
        }

        return 0;
    }
}
