using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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

    public List<GameObject> Cameras = new List<GameObject>();

    [Header("Interract")]
    [SerializeField]
    private GameObject interactContent;
    [SerializeField]
    private GameObject interactKeyPref;
    private GameObject interactKey;
    private GameObject interactKeyHang;


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

        UIButtonsManager.instance.GetButton("menu").GetComponent<UIBtnPause>()
            .SetPlayer(playerInventory);

        interactKey = Instantiate(interactKeyPref, interactContent.transform);
        interactKeyHang = Instantiate(interactKeyPref, interactContent.transform);


        SetCamera(0);
    }

    public void SetCamera(int index)
    {
        int x = 0;
        foreach (GameObject item in Cameras)
        {
            if (index == x)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
            x++;
        }
    }

    private void Update()
    {
        CheckInputs();

        if (canShop)
        {
            interactKey.SetActive(true);
            interactKey.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = KeyCollection.interactKey.ToUpper();
        }
        else
        {
            interactKey.SetActive(false);
        }

        if (canHangar)
        {
            interactKeyHang.SetActive(true);
            interactKey.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = KeyCollection.hangarKey.ToUpper();
        }
        else
        {
            interactKeyHang.SetActive(false);
        }
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown("escape"))
        {
            UIButtonsManager.instance.ActionButton("menu");
        }

        /* Check inputs to fire turrets
         */
        if (Input.GetMouseButton(0) && !overCheck.IsPointerOverUIElement() && !IsHangar)
        {
            foreach (Transform item in modelManager.TurretsContent)
            {
                if(item.childCount > 0)
                {
                    item.GetChild(0).GetComponent<Turret>().Fire();
                }
            }
        }


        if (canShop && Input.GetKeyDown(KeyCollection.interactKey) && !IsHangar)
        {
            UIButtonsManager.instance.ActionButton("shop");
        }
        else if (Input.GetKeyDown(KeyCollection.invKey) && !IsHangar)
        {
            UIButtonsManager.instance.ActionButton("bag");
        }
        else if (Input.GetKeyDown(KeyCollection.hangarKey) && canHangar)
        {
            UIButtonsManager.instance.ActionButton("hangar");
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
