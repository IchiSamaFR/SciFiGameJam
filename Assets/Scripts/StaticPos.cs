using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPos : MonoBehaviour
{
    public bool rota = false;
    public Vector3 vecRota = new Vector3();
    public bool pos = false;
    public Vector3 vecPos = new Vector3();

    int z;

    void FixedUpdate()
    {
        if (pos)
        {
            transform.position = vecPos;
        }
        if (rota)
        {
            z++;
            Vector3 r = transform.parent.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3((r.x - r.x) + vecRota.x,
                                                              (r.y - r.y) + vecRota.y,
                                                              (r.z - r.z) + vecRota.z));
        }
    }
}
