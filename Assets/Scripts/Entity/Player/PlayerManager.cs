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

    public ModelManager modelManager;
    
    UIOverCheck overCheck;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponent<PlayerInventory>();
        overCheck = GetComponent<UIOverCheck>();

        modelManager = transform.GetChild(0).GetComponent<ModelManager>();
    }

    void Start()
    {
        modelManager.ImageSelected.gameObject.SetActive(false);
        modelManager.SetManager(this);
        modelManager.SetTurrets("turret_mk10");

        UIButtonsManager.instance.ButtonNotInteractable("shop");
        UIButtonsManager.instance.GetButton("shop").GetComponent<UIBtnShop>()
            .SetPlayer(playerInventory);
        UIButtonsManager.instance.GetButton("bag").GetComponent<UIBtnInventory>()
            .SetPlayer(playerInventory);
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
                if(item.GetChild(0) != null)
                {
                    item.GetChild(0).GetComponent<Turret>().Fire();
                }
            }
        }
        if (Input.GetKeyDown(invKey))
        {
            if (shop)
            {
                UIButtonsManager.instance.ActionButton("shop");
            }
            else
            {
                UIButtonsManager.instance.ActionButton("bag");
            }
        }
    }

    bool shop = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Shop")
        {
            UIButtonsManager.instance.ButtonInteractable("shop");
            UIButtonsManager.instance.GetButton("shop").GetComponent<UIBtnShop>()
                    .SetShop(other.GetComponent<ShopInventory>());
            shop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            UIButtonsManager.instance.ButtonNotInteractable("shop");
            UIButtonsManager.instance.CloseButton("shop");
            shop = false;
        }
    }
}
