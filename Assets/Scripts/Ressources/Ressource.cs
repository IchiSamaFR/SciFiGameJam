using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour
{
    [SerializeField]
    private string itemId;
    [SerializeField]
    private int amount;
    private Item item;

    public Item Item { get => item; set => item = value; }

    public Ressource()
    {
        if (ItemCollection.instance)
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (item == null)
        {
            if (ItemCollection.instance && itemId != "credits")
            {
                item = ItemCollection.instance.GetItem(itemId);
                item.AddAmount(amount);
            }
            else
            {
                return;
            }
        }

        if(other.tag == "Player")
        {
            other.GetComponent<CollisionRedirect>().parent.GetComponent<PlayerInventory>().GetItem(item);
            Destroy(gameObject);
            //print(item.Name + " : " + item.Amount + "/" + item.MaxAmount);
        }
    }
}
