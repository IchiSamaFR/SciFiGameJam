using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInv : MonoBehaviour
{
    private Item item;

    [SerializeField]
    private TextMeshProUGUI UIname;
    [SerializeField]
    private TextMeshProUGUI UIamount;
    [SerializeField]
    private GameObject UIsellBuy;

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
}
