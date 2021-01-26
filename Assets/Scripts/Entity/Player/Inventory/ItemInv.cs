using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInv : MonoBehaviour
{
    private Item item;
    private UIContainer uiContainer;

    [SerializeField]
    private TextMeshProUGUI UIname;
    [SerializeField]
    private TextMeshProUGUI UIamount;
    [SerializeField]
    private GameObject UIsellBuy;
    private bool statsOpen = false;

    Vector2 baseSize = new Vector2();
    Vector2 statsSize = new Vector2();

    public void SetParent(UIContainer uiContainer)
    {
        this.uiContainer = uiContainer;
    }

    public void Set(Item item, bool sellBuy = false)
    {
        this.item = item;
        UIname.text = item.Name;
        UIamount.text = "x" + item.Amount;

        if (sellBuy)
        {
            UIsellBuy.SetActive(true);
        }
        else
        {
            UIsellBuy.SetActive(false);
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
}
