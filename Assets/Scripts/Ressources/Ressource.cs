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
    public string ItemId { get => itemId; set => itemId = value; }
    public int Amount { get => amount; set => amount = value; }

    public void Init()
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
    }

    public void Set(Item item)
    {
        this.item = item;
        itemId = item.Id;
        amount = item.Amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        Init();

        if (other.tag == "Player")
        {
            other.GetComponent<CollisionRedirect>().parent.GetComponent<PlayerInventory>().GetItem(item);
            
            GameObject obj = Instantiate(PrefabCollection.instance.GetPrefab("fxAudio"));
            obj.transform.position = transform.position;
            obj.GetComponent<FXAudio>().Set(AudioCollection.instance.GetAudio("pickup"));

            Destroy(gameObject);
        }
    }
}
