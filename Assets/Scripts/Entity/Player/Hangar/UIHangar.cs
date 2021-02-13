using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHangar : MonoBehaviour
{
    HangarManager hangar;

    [Header("Objects")]
    public GameObject Panel;
    public Transform ContentHangar;
    public Transform ContentInventory;

    [Header("Prafab")]
    public GameObject TitleObject;
    public GameObject StatsPanel;
    public GameObject Item;
    public GameObject ItemShip;

    private GameObject statsPanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Open(PlayerInventory inventory, ModelManager model)
    {
        Panel.SetActive(true);
        Refresh(inventory, model);
    }
    public void Close()
    {
        Panel.SetActive(false);

        if (statsPanel)
        {
            Destroy(statsPanel);
        }
    }

    public void Set(HangarManager hangar)
    {
        this.hangar = hangar;
    }
    public void Refresh(PlayerInventory inventory, ModelManager model)
    {
        Refresh(inventory.Items, "inventory");
        Refresh(model.GetEquipements(), "ship");
    }

    public void DeleteContent(string stockage)
    {
        if (stockage == "inventory")
        {
            foreach (Transform item in ContentInventory)
            {
                Destroy(item.gameObject);
            }
        }
        else if (stockage == "ship")
        {
            foreach (Transform item in ContentHangar)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public void Refresh(List<Item> items, string stockage)
    {
        DeleteContent(stockage);

        string[] types = { "turret", "thruster", "shield" };
        string[] name = { "Turrets", "Thrusters", "Shields" };

        Transform content = ContentHangar;

        if(stockage == "inventory")
        {
            content = ContentInventory;

            int index = 0;
            foreach (string type in types)
            {
                Instantiate(TitleObject, content).GetComponent<ItemHangar>().SetName(name[index]);

                foreach (var item in items)
                {
                    if (item.Type == type)
                    {
                        for (int i = 0; i < item.Amount; i++)
                        {
                            ItemHangar hang = Instantiate(Item, content).GetComponent<ItemHangar>();
                            hang.SetParent(hangar);
                            hang.Set(item, StatsPanel, stockage);
                        }
                    }
                }

                index++;
            }
        }
        else if (stockage == "ship")
        {
            content = ContentHangar;

            int index = 0;
            foreach (string type in types)
            {
                Instantiate(TitleObject, content).GetComponent<ItemHangar>().SetName(name[index]);

                foreach (var item in items)
                {
                    if (item.Type == type)
                    {
                        ItemHangar hang = Instantiate(Item, content).GetComponent<ItemHangar>();
                        hang.SetParent(hangar);
                        hang.Set(item, StatsPanel, stockage);
                    }
                }

                index++;
            }
        }
    }

    public void SetItemStatsOpen(GameObject newStatsPanel)
    {
        if (statsPanel)
        {
            Destroy(statsPanel);
        }
        statsPanel = newStatsPanel;
    }
}
