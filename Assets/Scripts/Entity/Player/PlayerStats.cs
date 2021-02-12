﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    [Header("GUI Health Shield")]
    [SerializeField]
    private float pixelPerCent = 34f;
    [SerializeField]
    private float padding = 8f;
    [SerializeField]
    private float maxWidth = 500f;

    float newDivider = 1;


    [SerializeField]
    private RectTransform healthFill;
    [SerializeField]
    private RectTransform healthBorder;
    [SerializeField]
    private RectTransform shieldFill;
    [SerializeField]
    private RectTransform shieldBorder;
    float baseHealthWidth;
    float baseShieldWidth;

    void Start()
    {
        Init();
        RefreshUI();

        Vector3 pos = this.transform.position;

        Item _item = ItemCollection.instance.GetItem("turret_mk10");
        _item.SetAmount(1);
        GameObject _obj = Instantiate(_item.DropPrefab);
        _obj.GetComponent<Ressource>().Set(_item);
        _obj.transform.position = new Vector3(Random.Range(pos.x + 1f, pos.x + 1f),
                                              0,
                                              Random.Range(pos.z + 1f, pos.z + 1f));
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
        if(pixelPerCent * MaxHealth > maxWidth && newDivider < maxWidth / pixelPerCent * MaxHealth)
        {
            newDivider = maxWidth / (pixelPerCent * MaxHealth);
        }
        if (pixelPerCent * MaxShield > maxWidth && newDivider < maxWidth / pixelPerCent * MaxShield)
        {
            newDivider = maxWidth / (pixelPerCent * MaxShield);
        }

        healthBorder.sizeDelta = new Vector2(pixelPerCent * MaxHealth * newDivider,
                                            healthBorder.sizeDelta.y);
        healthFill.sizeDelta = new Vector2((healthBorder.sizeDelta.x - padding * 2) * ((float)ActualHealth / MaxHealth),
                                            healthFill.sizeDelta.y);

        shieldBorder.sizeDelta = new Vector2(pixelPerCent * MaxShield * newDivider,
                                            shieldBorder.sizeDelta.y);
        shieldFill.sizeDelta = new Vector2((shieldBorder.sizeDelta.x - padding * 2) * ((float)ActualShield / MaxShield), 
                                            shieldFill.sizeDelta.y);
    }
}
