using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBtnPause : UIButton
{
    PlayerInventory playerInventory;

    public void SetPlayer(PlayerInventory inv)
    {
        playerInventory = inv;
    }

    public override void Action()
    {
        if (MenuManager.instance.actualMenu == "resume" || MenuManager.instance.actualMenu == ""
            || MenuManager.instance.actualMenu == "main")
        {
            Open();
        }
        else
        {
            Close();
        }
    }
    public override void Open()
    {
        playerInventory.Close();
        //playerInventory.PlayerManager.hangarManager.Close();
        MenuManager.instance.ChangeMenu("pause");
        UIButtonsManager.instance.ButtonActive("menu");
    }
    public override void Close()
    {
        MenuManager.instance.ChangeMenu("resume");
        UIButtonsManager.instance.ButtonUnactive("menu");
    }
}
