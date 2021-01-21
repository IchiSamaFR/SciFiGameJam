using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GeometricTools;
using static KeyCollection;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;

    [Header("Stats Instantiate")]
    [SerializeField]
    private float maxSpeed = 7;
    private float maxSpeedInit;
    [SerializeField]
    private float accelSpeed = 2.5f;
    private float accelSpeedInit;
    [SerializeField]
    private float brakeSpeed = 0.15f;
    private float brakeSpeedInit;
    [SerializeField]
    private float rotationSpeed = 2.5f;
    private float rotationSpeedInit;

    Quaternion rotationToGetEuler = new Quaternion();

    [Header("Counter errors")]
    [SerializeField]
    private float errorBrake = 0.02f;
    [SerializeField]
    private float errorRotation = 2f;


    Rigidbody rb;

    public float ActualSpeed { get => GetHypot(rb.velocity); }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float AccelSpeed { get => accelSpeed; set => accelSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();

        maxSpeedInit = maxSpeed;
    }

    void Update()
    {
        UpdateRotation();
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (Input.GetKey(forwardKey))
        {
            rb.AddForce(transform.forward * accelSpeed);
        }
        else if (Input.GetKey(backwardKey))
        {
            rb.AddForce(-transform.forward * accelSpeed);
            if (Input.GetKey(brakeKey))
            {
                Brake(0.1f);
            }
        }
        else if (Input.GetKey(brakeKey))
        {
            Brake();
        }

        if (ActualSpeed > MaxSpeed)
        {
            rb.AddForce(-transform.forward * (ActualSpeed - MaxSpeed));
        }
        else if (ActualSpeed < -MaxSpeed)
        {
            rb.AddForce(transform.forward * (ActualSpeed - MaxSpeed));
        }
    }

    void Brake(float intensity = 1)
    {
        Vector3 velocity = rb.velocity * intensity;

        if (GetHypot(velocity) < 0.2f)
        {
            velocity = velocity / GetHypot(velocity);
        }

        if(!float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z))
        {
            rb.AddForce(-velocity * brakeSpeed);
        }

        if (intensity == 1 
            && ActualSpeed < errorBrake && ActualSpeed > -errorBrake
            && rb.velocity != Vector3.zero)
        {
            
            rb.velocity = Vector3.zero;
        }
    }

    void UpdateRotation()
    {
        Camera cam = playerManager.playerCamera.self.GetComponent<Camera>();

        Vector2 selfPos = cam.WorldToScreenPoint(transform.position);
        rotationToGetEuler = Quaternion.Euler(0, GetAngle(selfPos, Input.mousePosition) - 180, 0);

        if (rotationToGetEuler.eulerAngles.y != transform.rotation.eulerAngles.y)
        {
            float _angleDiff = AngleDiff(rotationToGetEuler.eulerAngles.y, transform.rotation.eulerAngles.y);
            if (_angleDiff > errorRotation)
            {
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), 
                                                     Quaternion.Euler(0, rotationToGetEuler.eulerAngles.y + 180, 0), 
                                                     rotationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + -Time.deltaTime * rotationSpeed * 60, 0);
            }
            else if (_angleDiff < -errorRotation)
            {
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0),
                                                     Quaternion.Euler(0, rotationToGetEuler.eulerAngles.y + 180, 0),
                                                     rotationSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + Time.deltaTime * rotationSpeed * 60, 0);
            }
        }
    }
}
