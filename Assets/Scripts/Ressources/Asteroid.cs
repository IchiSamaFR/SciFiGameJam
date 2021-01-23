using System.Collections;
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

    public List<RessourcesDroped> RessourcesToDrop { get => ressourcesToDrop; set => ressourcesToDrop = value; }

    void Start()
    {
        Init();
    }

    public override void GetDestroyed()
    {
        CreateDrop();
        base.GetDestroyed();
    }

    void CreateDrop()
    {
        ItemCollection it = ItemCollection.instance;
        Vector3 pos = transform.position;
        foreach (RessourcesDroped item in ressourcesToDrop)
        {
            Item _item = it.GetItem(item.id);
            GameObject _obj = Instantiate(_item.Prefab);
            _obj.GetComponent<Ressource>().Item = _item;
            _obj.GetComponent<Ressource>().Item.AddAmount(item.amount);
            _obj.transform.position = new Vector3(Random.Range(pos.x - 0.5f, pos.x + 0.5f),
                                                  0,
                                                  Random.Range(pos.z - 0.5f, pos.z + 0.5f));
        }
    }
}
