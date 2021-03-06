﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCollection : MonoBehaviour
{
    [System.Serializable]
    public struct prefabObj
    {
        public string id;
        public GameObject prefab;
    }

    public static PrefabCollection instance;

    public List<prefabObj> prefabList = new List<prefabObj>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Instance déjà crée, il ne peut y avoir 2 CollectionManager.");
            return;
        }
        instance = this;
    }

    public GameObject GetPrefab(string id)
    {
        foreach (prefabObj item in prefabList)
        {
            if(item.id == id)
            {
                return item.prefab;
            }
        }
        return null;
    }
}
