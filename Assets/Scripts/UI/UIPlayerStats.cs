using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [Header("GUI Health Shield")]
    [SerializeField]
    private float pixelPerCent = 34f;
    [SerializeField]
    private float padding = 8f;
    [SerializeField]
    private float maxWidth = 500f;
    
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

    float newDivider = 1;

    public void RefreshUI(float hValue, float hMax, float sValue, float sMax)
    {
        if (pixelPerCent * hMax > maxWidth && newDivider < maxWidth / pixelPerCent * hMax)
        {
            newDivider = maxWidth / (pixelPerCent * hMax);
        }
        if (pixelPerCent * sMax > maxWidth && newDivider < maxWidth / pixelPerCent * sMax)
        {
            newDivider = maxWidth / (pixelPerCent * sMax);
        }

        healthBorder.sizeDelta = new Vector2(pixelPerCent * hMax * newDivider,
                                            healthBorder.sizeDelta.y);
        healthFill.sizeDelta = new Vector2((healthBorder.sizeDelta.x - padding * 2) * (hValue / hMax),
                                            healthFill.sizeDelta.y);

        shieldBorder.sizeDelta = new Vector2(pixelPerCent * sMax * newDivider,
                                            shieldBorder.sizeDelta.y);
        shieldFill.sizeDelta = new Vector2((shieldBorder.sizeDelta.x - padding * 2) * (sValue / sMax),
                                            shieldFill.sizeDelta.y);
    }
}
