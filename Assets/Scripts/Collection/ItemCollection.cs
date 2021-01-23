﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public static ItemCollection instance;

    public List<Item> SpecialItemList = new List<Item>();
    public List<Item> itemList = new List<Item>();

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Instance déjà crée, il ne peut y avoir 2 ItemCollection.");
            return;
        }
        instance = this;
    }


    public Item GetItem(string id)
    {
        foreach (Item item in itemList)
        {
            if(item.Id == id)
            {
                return new Item(item);
            }
        }
        return null;
    }
    public string GetItemName(string id)
    {
        Item item = GetItem(id);
        if(item != null)
        {
            return item.Name;
        }
        else
        {
            return "";
        }
    }
    public Sprite GetItemSprite(string id)
    {
        Item item = GetItem(id);
        if (item != null)
        {
            return item.Sprite;
        }
        else
        {
            return null;
        }
    }
    public GameObject GetItemPrefab(string id)
    {
        Item item = GetItem(id);
        if (item != null)
        {
            return item.Prefab;
        }
        else
        {
            return null;
        }
    }
}