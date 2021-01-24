using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GeometricTools;

[RequireComponent(typeof(Rigidbody))]
public class EntityStats : MonoBehaviour
{
    #region -- Assign variable --
    [Header("Stats Instantiate")]
    [SerializeField]
    private int maxHealth = 10;
    private int maxHealthInit;
    [SerializeField]
    private int actualHealth;

    [SerializeField]
    private int maxShield;
    private int maxShieldInit;
    [SerializeField]
    private int actualShield;

    [SerializeField]
    private float maxSpeed;
    private float maxSpeedInit;

    [SerializeField]
    private float accelSpeed = 2f;
    private float accelSpeedInit;

    [SerializeField]
    private float brakeSpeed = 0.15f;
    private float brakeSpeedInit;
    [SerializeField]
    private float rotationSpeed = 2f;
    private float rotationSpeedInit;


    [Header("Counter errors")]
    [SerializeField]
    private float errorBrake = 0.02f;
    [SerializeField]
    private float errorRotation = 2f;

    [Header("Objects")]
    [SerializeField]
    private GameObject getDestroyedEffects;

    private Rigidbody rb;
    #endregion


    #region -- Assign Getter Setter --
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int MaxHealthInit { get => maxHealthInit; set => maxHealthInit = value; }
    public int ActualHealth { get => actualHealth; set => actualHealth = value; }

    public int MaxShield { get => maxShield; set => maxShield = value; }
    public int MaxShieldInit { get => maxShieldInit; set => maxShieldInit = value; }
    public int ActualShield { get => actualShield; set => actualShield = value; }

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float MaxSpeedInit { get => maxSpeedInit; set => maxSpeedInit = value; }
    public float ActualSpeed { get => GetHypot(new Vector2(rb.velocity.x, rb.velocity.z)); }

    public float AccelSpeed { get => accelSpeed; set => accelSpeed = value; }
    public float AccelSpeedInit { get => accelSpeedInit; set => accelSpeedInit = value; }

    public float BrakeSpeed { get => brakeSpeed; set => brakeSpeed = value; }
    public float BrakeSpeedInit { get => brakeSpeedInit; set => brakeSpeedInit = value; }

    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float RotationSpeedInit { get => rotationSpeedInit; set => rotationSpeedInit = value; }

    public float ErrorBrake { get => errorBrake; set => errorBrake = value; }
    public float ErrorRotation { get => errorRotation; set => errorRotation = value; }

    public GameObject GetDestroyedEffects { get => getDestroyedEffects; set => getDestroyedEffects = value; }
    #endregion

    public void Init()
    {
        rb = GetComponent<Rigidbody>();

        actualHealth = maxHealth;
        actualShield = maxShield;

        maxSpeedInit = maxSpeed;
        brakeSpeedInit = brakeSpeed;
        rotationSpeedInit = rotationSpeed;
    }

    public virtual void GetDamage(int amount)
    {
        if (actualHealth <= 0)
        {
            return;
        }

        if(actualShield > 0)
        {
            actualShield -= amount;
            if(actualShield < 0)
            {
                actualHealth -= -actualShield;
            }
        }
        else
        {
            actualHealth -= amount;
        }

        if(actualHealth < 0)
        {
            actualHealth = 0;
        }
        if(actualHealth == 0)
        {
            GetDestroyed();
        }
    }
    public virtual void GetDestroyed()
    {
        if(GetDestroyedEffects != null)
        {
            GameObject _effect = Instantiate(GetDestroyedEffects);
            _effect.transform.position = transform.position;
            Destroy(_effect, 10);
        }
    }

    public virtual void GetHeal(int amount)
    {
        actualHealth += amount;
        if(actualHealth > maxHealth)
        {
            actualHealth = maxHealth;
        }
    }
    public virtual void GetShield(int amount)
    {
        if(actualHealth <= 0)
        {
            return;
        }

        actualShield += amount;
        if (actualShield > maxShield)
        {
            actualShield = maxShield;
        }
    }
}
