using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretItem : Item
{
    [Header("Turret Stats")]
    [SerializeField]
    private GameObject instancePrefab;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float fireRate = 1f;
    [SerializeField]
    private float fireSpeed = 20f;
    [SerializeField]
    private float range = 20f;

    public GameObject InstancePrefab { get => instancePrefab; set => instancePrefab = value; }
    public int Damage { get => damage; set => damage = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float FireSpeed { get => fireSpeed; set => fireSpeed = value; }
    public float Range { get => range; set => range = value; }

    public TurretItem(string Id, string Name, string Type, GameObject DropPrefab, Sprite Sprite, int MaxAmount, int Price, string Description, int damage, float fireRate) 
        : base(Id, Name, Type, DropPrefab, Sprite, MaxAmount, Price, Description)
    {
        this.damage = damage;
        this.fireRate = fireRate;
    }
    public TurretItem(string Id, string Name, string Type, GameObject DropPrefab, Sprite Sprite, int damage, float fireRate)
        : base(Id, Name, Type, DropPrefab, Sprite)
    {
        this.damage = damage;
        this.fireRate = fireRate;
    }
    public TurretItem(Item item, int damage, float fireRate)
        : base(item)
    {
        this.damage = damage;
        this.fireRate = fireRate;
    }
    public TurretItem(TurretItem turretItem)
        : base(turretItem)
    {
        this.damage = turretItem.damage;
        this.fireRate = turretItem.fireRate;
        this.instancePrefab = turretItem.instancePrefab;
    }
}
