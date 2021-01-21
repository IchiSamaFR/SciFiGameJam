using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform self;
    public Transform target;
    public float zoom = 0;
    Vector3 diff;

    void Start()
    {
        diff = self.transform.position - target.position;
    }
    
    void Update()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        self.transform.position = target.position + diff;
        self.transform.position += self.transform.forward * zoom;
    }
}
