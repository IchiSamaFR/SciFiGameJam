using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

using static GeometricTools;


[RequireComponent(typeof(AIStats))]
[RequireComponent(typeof(AIMovement))]
public class AIManager : MonoBehaviour
{
    public AIStats aiStats;
    public AIMovement aiMovement;

    public ModelManager modelManager;
    
    List<Turret> turrets = new List<Turret>();

    public Transform obj;

    public Transform target;

    void Awake()
    {
        aiStats = GetComponent<AIStats>();
        aiMovement = GetComponent<AIMovement>();

        modelManager = transform.GetChild(0).GetComponent<ModelManager>();
    }

    private void Start()
    {
        modelManager.ImageSelected.gameObject.SetActive(true);

        foreach (Transform item in modelManager.TurretsContent)
        {
            if (item.GetChild(0) != null)
            {
                item.GetChild(0).GetComponent<Turret>().SetStats(1, 1, 12, 20);
            }
        }
    }

    int x = 0;
    void Update()
    {
        CheckFire();
    }

    void CheckFire()
    {
        float distBetween = GetHypot(new Vector2(transform.position.x - target.transform.position.x,
                                                 transform.position.z - target.transform.position.z));

        if(distBetween < 6)
        {
            foreach (Transform item in modelManager.TurretsContent)
            {
                if (item.GetChild(0) != null)
                {
                    int x = Random.Range(0, 2);
                    int rand = Random.Range(5, 10);

                    if(x == 0)
                    {
                        rand = -rand;
                    }

                    item.GetChild(0).GetComponent<Turret>().SetTarget(target, rand);
                    item.GetChild(0).GetComponent<Turret>().Fire();
                }
            }
        }
        else
        {
            foreach (Transform item in modelManager.TurretsContent)
            {
                if (item.GetChild(0) != null)
                {
                    item.GetChild(0).GetComponent<Turret>().SetTarget(null);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!target)
        {
            EntityStats entity = other.GetComponent<EntityStats>();
            if (entity != null && FactionCollection.instance.GetRelation(entity.Faction, aiStats.Faction) < 0)
            {
                target = entity.transform;
            }
        }
    }
}
