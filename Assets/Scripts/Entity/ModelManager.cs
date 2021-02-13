using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
    [SerializeField]
    private Image imageSelected;
    [SerializeField]
    private Image imageMinimap;

    
    [SerializeField]
    private List<TurretItem> turretsItem = new List<TurretItem>();
    [SerializeField]
    private List<TurretItem> thrustersItem = new List<TurretItem>();
    [SerializeField]
    private List<TurretItem> armorItem = new List<TurretItem>();

    [SerializeField]
    private List<Transform> turretsContent = new List<Transform>();
    [SerializeField]
    private List<Transform> thrustersContent = new List<Transform>();
    
    private bool aimTarget;
    private Transform target;
    private PlayerManager player;
    private AIManager aiManager;



    public int TurretsCount { get => turretsContent.Count; }
    public int ThrustersCount { get => thrustersContent.Count; }
    public int ShieldsCount = 1;

    public List<Transform> TurretsContent { get => turretsContent; set => turretsContent = value; }
    public List<Transform> ThrustersContent { get => thrustersContent; set => thrustersContent = value; }
    public Image ImageMinimap { get => imageMinimap; set => imageMinimap = value; }
    public Image ImageSelected { get => imageSelected; set => imageSelected = value; }
    public PlayerManager Player { get => player; set => player = value; }
    public AIManager AiManager { get => aiManager; set => aiManager = value; }

    void Start()
    {

    }
    
    void Update()
    {

    }

    public void SetManager(PlayerManager player)
    {
        this.player = player;

        foreach (Transform _turretTrans in TurretsContent)
        {
            if (_turretTrans.childCount > 0)
            {
                _turretTrans.GetChild(0).GetComponent<Turret>().SetManager(player);
            }
        }
    }
    public void SetManager(AIManager aiManager)
    {
        this.aiManager = aiManager;

        foreach (Transform _turretTrans in TurretsContent)
        {
            if (_turretTrans.childCount > 0)
            {
                _turretTrans.GetChild(0).GetComponent<Turret>().SetManager(aiManager);
            }
        }
    }

    public bool AddItem(Item item)
    {
        if (item.Type == "turret")
        {
            foreach (Transform _turretTrans in TurretsContent)
            {
                if (_turretTrans.childCount == 0)
                {
                    TurretItem _turretItem = ItemCollection.instance.GetTurretItem(item.Id);
                    Turret turret = Instantiate(_turretItem.InstancePrefab, _turretTrans).GetComponent<Turret>();
                    turret.SetStats(_turretItem);
                    turretsItem.Add(_turretItem);
                    if (player)
                    {
                        turret.SetManager(player);
                    }
                    if (aiManager)
                    {
                        turret.SetManager(aiManager);
                    }
                    return true;
                }
            }
        }
    
        return false;
    }
    public bool AddItem(string itemId)
    {
        return AddItem(ItemCollection.instance.GetItem(itemId));
    }

    public bool RemoveItem(Item item)
    {
        if (item.Type == "turret")
        {
            foreach (Transform _turretTrans in TurretsContent)
            {
                if (_turretTrans.childCount > 0
                    && _turretTrans.GetChild(0).GetComponent<Turret>().Id == item.Id)
                {
                    Destroy(_turretTrans.GetChild(0).gameObject);

                    int index = 0;
                    while(turretsItem.Count > index)
                    {
                        if(turretsItem[index].Id == item.Id)
                        {
                            break;
                        }
                        index++;
                    }
                    turretsItem.RemoveAt(index);
                    return true;
                }
            }
        }

        return false;
    }

    public void SetTurrets(Item item)
    {
        turretsItem = new List<TurretItem>();
        foreach (Transform _turretTrans in TurretsContent)
        {
            if (_turretTrans.childCount > 0)
            {
                Destroy(_turretTrans.GetChild(0).gameObject);
            }
            TurretItem _turretItem = ItemCollection.instance.GetTurretItem(item.Id);
            Turret turret = Instantiate(_turretItem.InstancePrefab, _turretTrans).GetComponent<Turret>();
            turret.SetStats(_turretItem);
            turretsItem.Add(_turretItem);

            if (player)
            {
                turret.SetManager(player);
            }
            if (aiManager)
            {
                turret.SetManager(aiManager);
            }
        }
    }
    public void SetTurrets(string itemId)
    {
        print("set");
        SetTurrets(ItemCollection.instance.GetItem(itemId));
    }


    public List<Item> GetEquipements()
    {
        List<Item> lst = new List<Item>();
        
        foreach (var item in turretsItem)
        {
            lst.Add(item);
        }
        foreach (var item in thrustersItem)
        {
            lst.Add(item);
        }
        foreach (var item in armorItem)
        {
            lst.Add(item);
        }

        return lst;
    }
}
