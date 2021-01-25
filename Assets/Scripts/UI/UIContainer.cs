using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject uiInv;
    [SerializeField]
    private GameObject uiItemInv;
    [SerializeField]
    private Transform uiContentInv;

    public GameObject UIInv { get => uiInv; set => uiInv = value; }
    public GameObject UIItemInv { get => uiItemInv; set => uiItemInv = value; }
    public Transform UIContentInv { get => uiContentInv; set => uiContentInv = value; }


    public void RefreshContainer(List<Item> itemsInv)
    {
        foreach (Transform item in UIContentInv)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in itemsInv)
        {
            ItemInv slot = Instantiate(UIItemInv, UIContentInv).GetComponent<ItemInv>();
            slot.Set(item);
        }
    }

    public void Open()
    {
        uiInv.SetActive(true);
    }

    public void Close()
    {
        uiInv.SetActive(false);
    }
}
