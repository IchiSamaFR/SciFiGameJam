using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCamera playerCamera;
    public PlayerStats playerStats;

    public List<Turret> turrets = new List<Turret>();

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCamera = GetComponent<PlayerCamera>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetMouseButton(0))
        {
            foreach (Turret item in turrets)
            {
                item.Fire();
            }
        }
    }
}
