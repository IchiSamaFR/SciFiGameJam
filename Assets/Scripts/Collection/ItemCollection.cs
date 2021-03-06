﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public static ItemCollection instance;

    public List<TurretItem> turretsItemList = new List<TurretItem>();
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

    /* Return items by Type
     */
    public List<Item> GetItemsType(string type)
    {
        List<Item> lst = new List<Item>();
        foreach (Item item in itemList)
        {
            if (item.Type == type)
            {
                lst.Add(new Item(item));
            }
        }
        return lst;
    }

    /* Return item by Id
     */
    public Item GetItem(string id)
    {
        foreach (Item item in itemList)
        {
            if(item.Id == id)
            {
                return new Item(item);
            }
        }
        foreach (Item item in turretsItemList)
        {
            if (item.Id == id)
            {
                return new Item(item);
            }
        }
        return null;
    }
    /* Return item by Id
     */
    public TurretItem GetTurretItem(string id)
    {
        foreach (TurretItem item in turretsItemList)
        {
            if (item.Id == id)
            {
                return new TurretItem(item);
            }
        }
        return null;
    }

    /* Return item Name by Id
     */
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

    /* Return item Sprite by Id
     */
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

    /* Return ressource Prefab Name by Id
     */
    public GameObject GetItemPrefab(string id)
    {
        Item item = GetItem(id);
        if (item != null)
        {
            return item.DropPrefab;
        }
        else
        {
            return null;
        }
    }

    /* Return item Price by Id
     */
    public int GetItemPrice(string id)
    {
        Item item = GetItem(id);
        if (item != null)
        {
            return item.Price;
        }
        else
        {
            return -1;
        }
    }

    public bool ItemExist(string id)
    {
        foreach (var item in itemList)
        {
            if(item.Id == id)
            {
                return true;
            }
        }
        return false;
    }
}
