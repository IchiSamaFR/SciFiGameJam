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

    public void Set(Item item)
    {
        this.item = item;
        UIname.text = item.Name;
        UIamount.text = "x" + item.Amount;
    }
}
