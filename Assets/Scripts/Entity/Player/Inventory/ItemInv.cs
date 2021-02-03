﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInv : MonoBehaviour
{
    private Item item;
    private UIContainer uiContainer;
    [Header("UI Objects")]
    [SerializeField]
    private TextMeshProUGUI UIname;
    [SerializeField]
    private TextMeshProUGUI UIamount;
    [SerializeField]
    private GameObject UIsellBuy;
    private bool statsOpen = false;
    private bool isSellBuy = false;

    Vector2 baseSize = new Vector2();
    Vector2 statsSize = new Vector2();

    [Header("UI Objects")]
    private GameObject prebaStatsPanel;
    private GameObject statsPanel;
    private int actualValue = 0;


    public void SetParent(UIContainer uiContainer)
    {
        this.uiContainer = uiContainer;
    }

    public void Set(Item item, GameObject statsPanelType)
    {
        this.item = item;
        UIname.text = item.Name;
        UIamount.text = "x" + item.Amount;
        prebaStatsPanel = statsPanelType;

        UIsellBuy.SetActive(false);
        isSellBuy = false;
    }

    public void Set(Item item, GameObject statsPanelType, bool sellBuy, int price)
    {
        Set(item, statsPanelType);

        if (sellBuy)
        {
            UIsellBuy.SetActive(true);
            isSellBuy = true;
            actualValue = price;
        }
        else
        {
            UIsellBuy.SetActive(false);
            isSellBuy = false;
        }
    }

    public void ActionBtn()
    {
        if(Input.GetKey("left shift"))
        {
            uiContainer.ItemButtonAction(item, true);
        }
        else
        {
            uiContainer.ItemButtonAction(item, false);
        }
    }

    public void CreateStatsPanel()
    {
        if(statsPanel != null)
        {
            Destroy(statsPanel);
        }
        else
        {
            string stats = GetStringStats();

            statsPanel = Instantiate(prebaStatsPanel, GameObject.Find("MainCanvas").transform);
            statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Name;
            statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = stats;
            uiContainer.SetItemStatsOpen(statsPanel);
        }
    }

    private string GetStringStats()
    {
        string stats = "";

        stats += "Price         : ";
        if (actualValue > 0)
        {
            if (actualValue > item.Price)
            {
                stats += "<color=red>" + actualValue + " <color=white>(" + item.Price + ")";
            }
            else if (actualValue < item.Price)
            {
                stats += "<color=green>" + actualValue + " <color=white>(" + item.Price + ")";
            }
            else
            {
                stats += "" + actualValue + " (" + item.Price + ")";
            }
            stats += "\n";
        }
        else
        {
            stats += item.Price;
            stats += "\n";
        }

        return stats;
    }
}
