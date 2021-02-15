using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnHangar : UIButton
{
    private HangarManager hangar;

    public void SetHangar(HangarManager hangar)
    {
        this.hangar = hangar;
    }
    public void SetPlayer(PlayerInventory inv)
    {
        hangar.playerInventory = inv;
    }
    public void SetModelManager(ModelManager modelManager)
    {
        hangar.modelManager = modelManager;
    }

    public override void Action()
    {
        if (!hangar.playerInventory || !hangar.modelManager)
            return;
        

        if (hangar.IsOpen)
        {
            Close();
        }
        else if (!hangar.IsOpen)
        {
            Open();
        }
    }
    public override void Open()
    {
        if (!hangar.playerInventory || !hangar.modelManager)
            return;

        if (hangar.playerManager.IsShop)
        {
            hangar.playerManager.playerInventory.ShopInventory.Close();
        }
        if (hangar.playerManager.IsBag)
        {
            hangar.playerManager.playerInventory.Close();
        }
        
        hangar.Open();
    }
    public override void Close()
    {
        if (!hangar.playerInventory || !hangar.modelManager)
            return;
        
        hangar.Close();
    }
}
