﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GeometricTools;
using static KeyCollection;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    Quaternion rotationToGetEuler = new Quaternion();
    Rigidbody rb;

    public float actualSpeed { get => playerManager.playerStats.ActualSpeed; }

    public float accelSpeed { get => playerManager.playerStats.AccelSpeed; set => playerManager.playerStats.AccelSpeed = value; }
    public float maxSpeed { get => playerManager.playerStats.MaxSpeed; set => playerManager.playerStats.MaxSpeed = value; }
    public float rotationSpeed { get => playerManager.playerStats.RotationSpeed; set => playerManager.playerStats.RotationSpeed = value; }


    public float brakeSpeed { get => playerManager.playerStats.BrakeSpeed; set => playerManager.playerStats.BrakeSpeed = value; }
    public float errorBrake { get => playerManager.playerStats.ErrorBrake; set => playerManager.playerStats.ErrorBrake = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        UpdateRotation();
        UpdateMovement();
    }

    /*
     * 
     */
    void UpdateMovement()
    {
        Vector3 _forceToAdd = new Vector3();

        if (Input.GetKey(forwardKey))
        {
            _forceToAdd += transform.forward * accelSpeed;
            if (Input.GetKey(brakeKey))
            {
                _forceToAdd += Brake(0.6f);
            }
        }
        else if (Input.GetKey(backwardKey))
        {
            _forceToAdd += - transform.forward * accelSpeed;
            if (Input.GetKey(brakeKey))
            {
                _forceToAdd += Brake(0.6f);
            }
        }
        else if (Input.GetKey(brakeKey))
        {
            _forceToAdd += Brake();
        }

        if (actualSpeed > maxSpeed)
        {
            _forceToAdd += -rb.velocity * (actualSpeed * 2 - maxSpeed);
        }
        else if (actualSpeed < -maxSpeed)
        {
            _forceToAdd += -rb.velocity * (actualSpeed * 2 - maxSpeed);
        }

        if (!float.IsNaN(_forceToAdd.x) && !float.IsNaN(_forceToAdd.y) && !float.IsNaN(_forceToAdd.z))
        {
            rb.AddForce(_forceToAdd);
        }
    }

    Vector3 Brake(float intensity = 1)
    {
        Vector3 velocity = rb.velocity;


        if(!float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z))
        {
            return -velocity * brakeSpeed;
        }

        if (intensity == 1 
            && actualSpeed < errorBrake && actualSpeed > -errorBrake
            && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }

        return new Vector3();
    }
    /*
    void Brake(float intensity = 1)
    {
        Vector3 velocity = rb.velocity * intensity;

        if (GetHypot(velocity) < 0.2f)
        {
            velocity = velocity / GetHypot(velocity);
        }

        if (!float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z))
        {
            rb.AddForce(-velocity * brakeSpeed);
        }

        if (intensity == 1
            && actualSpeed < errorBrake && actualSpeed > -errorBrake
            && rb.velocity != Vector3.zero)
        {

            rb.velocity = Vector3.zero;
        }
    }
    */

    void UpdateRotation()
    {
        Camera cam = playerManager.playerCamera.self.GetComponent<Camera>();

        Vector2 selfPos = cam.WorldToScreenPoint(transform.position);
        rotationToGetEuler = Quaternion.Euler(0, GetAngle(selfPos, Input.mousePosition) - 180, 0);

        if (rotationToGetEuler.eulerAngles.y != transform.rotation.eulerAngles.y)
        {
            float _angleDiff = AngleDiff(rotationToGetEuler.eulerAngles.y, transform.rotation.eulerAngles.y);

            /* Lerp between actual position and angle to get
             */
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0),
                                                 Quaternion.Euler(0, rotationToGetEuler.eulerAngles.y + 180, 0),
                                                 rotationSpeed * Time.deltaTime);

            //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + -Time.deltaTime * rotationSpeed * 60, 0);
        }
    }
}
