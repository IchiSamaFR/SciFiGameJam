using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnShop : UIButton
{
    private PlayerInventory playerInventory;
    private ShopInventory shopInv;

    public void SetPlayer(PlayerInventory inv)
    {
        playerInventory = inv;
    }

    public void SetShop(ShopInventory shop)
    {
        shopInv = shop;
    }

    public override void Action()
    {
        if (!shopInv)
            return;

        if (shopInv.IsOpen)
        {
            Close();
        }
        else if (!shopInv.IsOpen)
        {
            Open();
        }
    }
    public override void Open()
    {
        if (!shopInv)
            return;

        if (playerInventory.PlayerManager.IsHangar)
        {
            playerInventory.PlayerManager.hangarManager.Close();
        }

        playerInventory.Close();
        shopInv.Open(playerInventory);
    }
    public override void Close()
    {
        if (!shopInv)
            return;

        shopInv.Close();
    }
}
