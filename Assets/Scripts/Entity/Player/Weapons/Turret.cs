using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GeometricTools;

public class Turret : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int damage = 2;
    private int damageInit;
    [SerializeField]
    private float fireRate = 2f;
    private float fireRateInit;
    private float timerFireRate;
    [SerializeField]
    private float fireSpeed = 20f;
    private float fireSpeedInit;
    [SerializeField]
    private float range = 20f;
    private float rangeInit;
    [SerializeField]
    private float rotationSpeed = 2.5f;
    private float rotationSpeedInit;
    [SerializeField]
    private bool aimTarget;
    [SerializeField]
    private Transform target;


    [Header("Objects")]
    [SerializeField]
    private AIManager aiManager;
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private GameObject laserPref;
    [SerializeField]
    private List<Transform> laserMuzzles = new List<Transform>();
    private int indMuzzle = 0;

    [Header("Counter errors")]

    [SerializeField]
    private float errorRotation = 0f;
    
    void Update()
    {
        AimTarget();
    }

    public void SetManager(PlayerManager manager)
    {
        playerManager = manager;
    }
    public void SetManager(AIManager manager)
    {
        aiManager = manager;
    }

    public void SetStats(int damage, float fireRate, float fireSpeed, float range)
    {
        this.damage = damage;
        this.fireRate = fireRate;
        this.fireSpeed = fireSpeed;
        this.range = range;
    }


    public void SetTarget(Transform target, float errorRotation = 0)
    {
        this.target = target;
        this.errorRotation = errorRotation;
    }
    
    void AimTarget()
    {
        if(aimTarget && target != null)
        {
            Vector3 selfPos = new Vector3(transform.position.x, transform.position.z, 0);
            Vector3 targetPos = new Vector3(target.position.x, target.position.z, 0);
            Quaternion rotation = Quaternion.LookRotation(selfPos, targetPos);
            rotation = Quaternion.Euler(0, GetAngle(selfPos, targetPos) + errorRotation, 0);

            transform.rotation = rotation;
        }
        else if (!aimTarget && playerManager)
        {
            Camera cam = playerManager.playerCamera.self.GetComponent<Camera>();

            Vector2 selfPos = cam.WorldToScreenPoint(transform.position);
            float angle = GetAngle(selfPos, Input.mousePosition);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            
            transform.rotation = rotation;
        }
    }

    public void Fire()
    {
        if (timerFireRate > 0)
        {

            timerFireRate -= Time.deltaTime;
            return;
        }
        else
        {
            indMuzzle++;
            if (indMuzzle >= laserMuzzles.Count)
            {
                indMuzzle = 0;
            }

            Laser laser = Instantiate(laserPref).GetComponent<Laser>();
            if (aiManager)
            {
                laser.Set(damage, fireSpeed, range, aiManager.aiStats.Faction);
            }
            else
            {
                laser.Set(damage, fireSpeed, range);
            }

            laser.transform.position = transform.position;
            laser.transform.rotation = Quaternion.Euler(5, transform.rotation.eulerAngles.y, 0);

            timerFireRate = 1 / fireRate;
        }

    }
}
