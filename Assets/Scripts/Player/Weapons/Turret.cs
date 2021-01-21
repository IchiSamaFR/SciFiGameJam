using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GeometricTools;

public class Turret : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float damage = 2.5f;
    private float damageInit;
    [SerializeField]
    private float fireRate = 5f;
    private float fireRateInit;
    [SerializeField]
    private float rotationSpeed = 2.5f;
    private float rotationSpeedInit;
    [SerializeField]
    private bool aimTarget;
    [SerializeField]
    Transform target;

    [SerializeField]
    PlayerManager playerManager;

    [Header("Counter errors")]
    [SerializeField]
    private float errorRotation = 2f;

    void Start()
    {
        
    }
    
    void Update()
    {
        AimTarget();
    }

    void AimTarget()
    {
        if(aimTarget && target != null)
        {
            Vector3 selfPos = new Vector3(transform.position.x, transform.position.z, 0);
            Vector3 targetPos = new Vector3(target.position.x, target.position.z, 0);
            Quaternion rotation = Quaternion.LookRotation(selfPos, targetPos);
            rotation = Quaternion.Euler(0, GetAngle(selfPos, targetPos), 0);
            
            float _angleDiff = AngleDiff(rotation.eulerAngles.y, transform.rotation.eulerAngles.y);

            if (_angleDiff < -errorRotation && _angleDiff > errorRotation)
            {
                print("Change");
                transform.rotation = rotation;
            }
        }
        else if (!aimTarget && playerManager)
        {
            Camera cam = playerManager.playerCamera.self.GetComponent<Camera>();

            Vector2 selfPos = cam.WorldToScreenPoint(transform.position);
            Quaternion rotation = Quaternion.LookRotation(selfPos, Input.mousePosition);

            float _angleDiff = AngleDiff(rotation.eulerAngles.y, transform.rotation.eulerAngles.y);

            if (_angleDiff < -errorRotation && _angleDiff > errorRotation)
            {
                transform.rotation = rotation;
            }
        }
    }
}
