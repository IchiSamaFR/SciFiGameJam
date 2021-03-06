﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EntityStats
{
    [System.Serializable]
    public struct RessourcesDroped
    {
        public string id;
        public int amount;
    }

    [Header("Ressources")]
    [SerializeField]
    private List<RessourcesDroped> ressourcesToDrop = new List<RessourcesDroped>();
    private ItemCollection it { get => ItemCollection.instance; }

    public List<RessourcesDroped> RessourcesToDrop { get => ressourcesToDrop; set => ressourcesToDrop = value; }

    void Start()
    {
        Init();
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();
        CreateDrop();


        GameObject obj = Instantiate(PrefabCollection.instance.GetPrefab("fxAudio"));
        obj.transform.position = transform.position;
        obj.GetComponent<FXAudio>().Set(AudioCollection.instance.GetAudio("asteroid_explosion"));

        Destroy(gameObject);
    }

    void CreateDrop()
    {
        Vector3 pos = transform.position;

        for (int i = 0; i < ressourcesToDrop.Count; i++)
        {
            RessourcesDroped item = ressourcesToDrop[i];
            int amount = item.amount;

            while (amount > 0)
            {
                Item _item = it.GetItem(item.id);

                GameObject _obj = Instantiate(_item.DropPrefab);
                _obj.GetComponent<Ressource>().Item = _item;
                amount = _obj.GetComponent<Ressource>().Item.AddAmount(amount);
                _obj.transform.position = new Vector3(Random.Range(pos.x - 0.5f, pos.x + 0.5f),
                                                      0,
                                                      Random.Range(pos.z - 0.5f, pos.z + 0.5f));
            }
        }
    }
}
