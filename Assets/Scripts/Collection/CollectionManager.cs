﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemCollection))]
public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Instance déjà crée, il ne peut y avoir 2 CollectionManager.");
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}