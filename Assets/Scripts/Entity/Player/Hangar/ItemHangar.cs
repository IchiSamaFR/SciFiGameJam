using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemHangar : MonoBehaviour
{
    private Item item;
    private HangarManager hangar;
    [Header("UI Objects")]
    [SerializeField]
    private TextMeshProUGUI UIname;
    [SerializeField]
    private GameObject UIaction;
    private string stockage;

    [Header("UI Objects")]
    private GameObject prebaStatsPanel;
    private GameObject statsPanel;


    public void SetParent(HangarManager hangar)
    {
        this.hangar = hangar;
    }

    public void Set(Item item, GameObject statsPanelType, string stockage)
    {
        this.item = item;
        this.stockage = stockage;

        UIname.text = item.Name;
        prebaStatsPanel = statsPanelType;
    }
    public void SetName(string title)
    {
        UIname.text = title;
    }

    public void ActionBtn()
    {
        if (Input.GetKey("left shift"))
        {
            hangar.ItemButtonAction(item, true, stockage);
        }
        else
        {
            print(item.Name);
            print(stockage);
            print(hangar);
            hangar.ItemButtonAction(item, false, stockage);
        }
        FXAudio fxAudio = Instantiate(PrefabCollection.instance.GetPrefab("fxAudioBTN")).GetComponent<FXAudio>();
        fxAudio.Set(AudioCollection.instance.GetAudio("button"));
    }

    public void CreateStatsPanel()
    {
        if (statsPanel != null)
        {
            Destroy(statsPanel);
        }
        else
        {
            string stats = GetStringStats();

            statsPanel = Instantiate(prebaStatsPanel, GameObject.Find("MainCanvas").transform);
            statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Name;
            statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = stats;
            hangar.Hangar.SetItemStatsOpen(statsPanel);
        }
    }

    private string GetStringStats()
    {
        string stats = "";

        if (item.Type == "turret")
        {
            TurretItem turretItem = ItemCollection.instance.GetTurretItem(item.Id);
            stats += "Damage  : " + turretItem.Damage;
            stats += "\nFire Rate : " + 1 / turretItem.FireRate + " /s";
            stats += "\nDps           : " + turretItem.Damage * 1 / turretItem.FireRate + " /s";
        }

        if (item.Description != "")
        {
            stats += "\n";
            stats += "\n" + item.Description;
        }

        return stats;
    }
}
