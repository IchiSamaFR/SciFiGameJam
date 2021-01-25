using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public bool status = false;
    public List<Transform> lights;

    void Start()
    {
        SetStatus(status);
    }

    public void SetStatus(bool status)
    {
        this.status = status;
        foreach (Transform group in lights)
        {
            SetStatusChild(group);
        }
    }
    int x = 0;
    void SetStatusChild(Transform group)
    {
        foreach (Transform item in group)
        {
            Light li = item.GetComponent<Light>();
            if (li != null)
            {
                li.enabled = status;
            }
            else
            {
                SetStatusChild(item);
            }
            x++;
        }
    }
}
