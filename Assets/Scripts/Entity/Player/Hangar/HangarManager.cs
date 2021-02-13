using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarManager : MonoBehaviour
{
    private bool isOpen = true;

    public PlayerManager playerManager;
    public PlayerInventory playerInventory;
    public ModelManager modelManager;

    [SerializeField]
    private GameObject UIHangar;
    private UIHangar hangar;

    private GameObject statsPanel;

    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public UIHangar Hangar { get => hangar; set => hangar = value; }

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerInventory = GetComponent<PlayerInventory>();
        modelManager = playerInventory.PlayerManager.modelManager;

        hangar = Instantiate(UIHangar, GameObject.Find("MainCanvas").transform)
                 .GetComponent<UIHangar>();
        hangar.Set(this);
        Close();
    }
    void Update()
    {
        
    }

    public void Open()
    {
        print(isOpen);
        if (isOpen)
        {
            return;
        }

        UIButtonsManager.instance.ButtonActive("hangar");
        isOpen = true;
        hangar.Open(playerInventory, modelManager);
    }
    public void Close()
    {
        if (!isOpen)
        {
            return;
        }
        
        UIButtonsManager.instance.ButtonUnactive("hangar");

        isOpen = false;
        hangar.Close();
    }

    public void Refresh()
    {
        hangar.Refresh(playerInventory, modelManager);
    }


    public void ItemButtonAction(Item item, bool all, string stockage)
    {
        if(stockage == "ship")
        {
            playerInventory.GetItem(item, 1);
            modelManager.RemoveItem(item);
        }
        else if (stockage == "inventory")
        {
            if (modelManager.AddItem(item))
            {
                playerInventory.RemoveItem(item, 1);
            }
        }
        Refresh();
    }
}
