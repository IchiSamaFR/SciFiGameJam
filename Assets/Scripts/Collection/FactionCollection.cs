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
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
