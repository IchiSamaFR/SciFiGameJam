using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using static KeyCollection;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCamera playerCamera;
    public PlayerStats playerStats;
    public PlayerInventory playerInventory;
    public HangarManager hangarManager;

    public ModelManager modelManager;
    
    UIOverCheck overCheck;

    bool canShop = false;
    bool canHangar = false;

    bool isShop = false;
    
    public bool IsShop { get => isShop; set => isShop = value; }
    public bool IsHangar { get => hangarManager.IsOpen; set => hangarManager.IsOpen = value; }
    public bool IsBag { get => playerInventory.IsOpen; set => playerInventory.IsOpen = value; }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponent<PlayerInventory>();
        overCheck = GetComponent<UIOverCheck>();
        hangarManager = GetComponent<HangarManager>();

        modelManager = transform.GetChild(0).GetComponent<ModelManager>();
    }

    void Start()
    {
        modelManager.ImageSelected.gameObject.SetActive(false);
        modelManager.SetManager(this);
        modelManager.SetTurrets("turret_mk10");

        UIButtonsManager.instance.ButtonNotInteractable("shop");
        UIButtonsManager.instance.ButtonNotInteractable("hangar");
        UIButtonsManager.instance.GetButton("shop").GetComponent<UIBtnShop>()
            .SetPlayer(playerInventory);
        UIButtonsManager.instance.GetButton("bag").GetComponent<UIBtnInventory>()
            .SetPlayer(playerInventory);

        UIButtonsManager.instance.GetButton("hangar").GetComponent<UIBtnHangar>()
            .SetHangar(hangarManager);
        UIButtonsManager.instance.GetButton("hangar").GetComponent<UIBtnHangar>()
            .SetPlayer(playerInventory);
        UIButtonsManager.instance.GetButton("hangar").GetComponent<UIBtnHangar>()
            .SetModelManager(modelManager);
    }

    private void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetMouseButton(0) && !overCheck.IsPointerOverUIElement())
        {
            foreach (Transform item in modelManager.TurretsContent)
            {
                if(item.childCount > 0)
                {
                    item.GetChild(0).GetComponent<Turret>().Fire();
                }
            }
        }
        if (Input.GetKeyDown(invKey) && !IsHangar)
        {
            if (canShop)
            {
                UIButtonsManager.instance.ActionButton("shop");
            }
            else
            {
                UIButtonsManager.instance.ActionButton("bag");
            }
        }
        else if (Input.GetKeyDown("a"))
        {
            if (canHangar)
            {
                if (IsShop)
                {
                    playerInventory.ShopInventory.Close();
                }
                if (IsBag)
                {
                    playerInventory.Close();
                }
                UIButtonsManager.instance.ActionButton("hangar");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Shop")
        {
            UIButtonsManager.instance.ButtonInteractable("shop");
            UIButtonsManager.instance.GetButton("shop").GetComponent<UIBtnShop>()
                    .SetShop(other.GetComponent<ShopInventory>());

            UIButtonsManager.instance.ButtonInteractable("hangar");
            canShop = true;
            canHangar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            UIButtonsManager.instance.ButtonNotInteractable("hangar");
            UIButtonsManager.instance.ButtonNotInteractable("shop");
            UIButtonsManager.instance.CloseButton("shop");
            canShop = false;
            canHangar = false;
        }
    }
}
