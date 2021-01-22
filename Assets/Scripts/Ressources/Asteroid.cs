using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EntityStats
{
    void Start()
    {
        Init();
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();
    }
}
