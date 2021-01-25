using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    List<Item> items = new List<Item>();

    [Header("UI Objects")]
    [SerializeField]
    private GameObject UIInv;
    private UIContainer container;

    private bool isOpen = false;

    void Start()
    {

    }

    /* Open UI inventory
     */
    public void Open()
    {

        if (!container)
            container = Instantiate(UIInv, GameObject.Find("MainCanvas").transform).GetComponent<UIContainer>();
        

        if (isOpen)
        {
            Close();
            return;
        }

        isOpen = true;
        UIButtonsManager.instance.ButtonActive("bag");

        container.Open();
        container.RefreshContainer(items);
    }

    /* Close UI inventory
     */
    public void Close()
    {
        isOpen = false;
        if(container)
            container.Close();
        UIButtonsManager.instance.ButtonUnactive("bag");
    }

    /* Add item in inventory
     */
    public void GetItem(Item item)
    {
        if (item.Amount == 0)
            return;
        
        foreach (Item _item in items)
        {
            if(item.Id == _item.Id && !_item.Full)
            {
                item.Amount = _item.AddAmount(item.Amount);

                if (item.Amount == 0)
                {
                    break;
                }
            }
        }

        if (item.Amount > 0)
            items.Add(item);

        if(isOpen)
            container.RefreshContainer(items);
    }
}
