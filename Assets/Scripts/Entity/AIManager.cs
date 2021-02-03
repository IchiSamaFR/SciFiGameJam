using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AIStats))]
[RequireComponent(typeof(AIMovement))]
public class AIManager : MonoBehaviour
{
    public AIStats aiStats;
    public AIMovement aiMovement;

    [SerializeField]
    List<Turret> turrets = new List<Turret>();

    void Start()
    {
        aiStats = GetComponent<AIStats>();
        aiMovement = GetComponent<AIMovement>();
    }

    void Update()
    {
        
    }
}
