using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector3 difference;

    private void Start()
    {
        target = FindObjectOfType<PlayerManager>().transform;
        difference = transform.position - target.position;
    }

    void Update()
    {
        transform.position = target.position + difference - target.position * speed;
    }
}
