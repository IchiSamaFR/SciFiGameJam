using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnInventory : UIButton
{
    private PlayerInventory playerInventory;

    public void SetPlayer(PlayerInventory inv)
    {
        playerInventory = inv;
    }

    public override void Action()
    {
        if (playerInventory.IsOpen)
        {
            Close();
        }
        else if (!playerInventory.IsOpen)
        {
            Open();
        }
    }
    public override void Open()
    {
        playerInventory.Open();
    }
    public override void Close()
    {
        UIButtonsManager.instance.GetButton("shop").Close();
        playerInventory.Close();
    }
}
