using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour
{
    [SerializeField]
    private Item item;

    public Item Item { get => item; set => item = value; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CollisionRedirect>().parent.GetComponent<PlayerInventory>().GetItem(item);
            Destroy(gameObject);
            //print(item.Name + " : " + item.Amount + "/" + item.MaxAmount);
        }
    }
}
