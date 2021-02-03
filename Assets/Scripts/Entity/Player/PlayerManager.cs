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
    
    UIOverCheck overCheck;

    [SerializeField]
    List<Turret> turrets = new List<Turret>();

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponent<PlayerInventory>();
        overCheck = GetComponent<UIOverCheck>();

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
            foreach (Turret item in turrets)
            {
                item.Fire();
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
