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
    public string Description;

    public bool Full => Amount / MaxAmount == 1 ? true : false;

    public Item(string Id, string Name, string Type, GameObject Prefab, Sprite Sprite, int MaxAmount = 100, int Price = 0, string Description = "")
    {
        this.Id = Id;
        this.Name = Name;
        this.Type = Type;
        this.Prefab = Prefab;
        this.Sprite = Sprite;
        this.Amount = 0;
        this.MaxAmount = MaxAmount;
        this.Price = Price;
        this.Description = Description;
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
        this.Description = item.Description;
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
    public int ForceAddAmount(int amount)
    {
        Amount += amount;

        return 0;
    }
    public int RemoveAmount(int amount)
    {
        Amount -= amount;
        if (Amount <= 0)
        {
            return -Amount;
        }
        return -1;
    }
}
