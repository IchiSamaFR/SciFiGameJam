using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCamera playerCamera;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
