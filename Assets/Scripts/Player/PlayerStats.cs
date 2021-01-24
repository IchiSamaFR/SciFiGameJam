using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    [SerializeField]
    private float pixelPerCent = 34f;
    [SerializeField]
    private float padding = 8f;
    [SerializeField]
    private float maxWidth = 500f;

    float newDivider;


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
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetDamage(2);
        }
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
