using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    [Header("GUI Health Shield")]
    [SerializeField]
    private UIPlayerStats uiStats;

    void Start()
    {
        Init();
        RefreshUI();

        Vector3 pos = this.transform.position;

        /*
        Item _item = ItemCollection.instance.GetItem("turret_mk10");
        _item.SetAmount(1);
        GameObject _obj = Instantiate(_item.DropPrefab);
        _obj.GetComponent<Ressource>().Set(_item);
        _obj.transform.position = new Vector3(Random.Range(pos.x + 1f, pos.x + 1f),
                                              0,
                                              Random.Range(pos.z + 1f, pos.z + 1f));
        */
    }
    
    void Update()
    {
        CheckRegen();
    }

    public override void GetDamage(int amount)
    {
        base.GetDamage(amount);
        RefreshUI();
    }
    public override void GetHeal(int amount)
    {
        base.GetHeal(amount);
        RefreshUI();
    }
    public override void GetShield(int amount)
    {
        base.GetShield(amount);
        RefreshUI();
    }

    void RefreshUI()
    {
        uiStats.RefreshUI(ActualHealth, MaxHealth, ActualShield, MaxShield);
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();
        MenuManager.instance.ChangeMenu("death");
    }
}
