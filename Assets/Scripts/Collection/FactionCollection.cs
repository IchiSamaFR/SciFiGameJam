using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionCollection : MonoBehaviour
{
    public static FactionCollection instance;

    [System.Serializable]
    public class Faction
    {
        public string id;
        public string name;
    }

    [System.Serializable]
    public class FactionRelation
    {
        public string id;
        public string idOther;
        public float relation;
    }

    [SerializeField]
    private List<Faction> factions = new List<Faction>();

    [SerializeField]
    private List<FactionRelation> factionsRelations = new List<FactionRelation>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Instance déjà crée, il ne peut y avoir 2 CollectionManager.");
            return;
        }
        instance = this;
    }

    public float GetRelation(string id, string idOther)
    {
        foreach (FactionRelation item in factionsRelations)
        {
            if((item.id == id && item.idOther == idOther) || (item.id == idOther && item.idOther == id))
            {
                return item.relation;
            }
        }
        return 0;
    }
}
