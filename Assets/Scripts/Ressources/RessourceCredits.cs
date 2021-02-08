using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceCredits : MonoBehaviour
{
    [SerializeField]
    private int amount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CollisionRedirect>().parent.GetComponent<PlayerInventory>().GetMoney(amount);
            Destroy(gameObject);
        }
    }
}
