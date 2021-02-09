using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceCredits : Ressource
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CollisionRedirect>().parent.GetComponent<PlayerInventory>().GetMoney(Amount);

            GameObject obj = Instantiate(PrefabCollection.instance.GetPrefab("fxAudio"));
            obj.transform.position = transform.position;
            obj.GetComponent<FXAudio>().Set(AudioCollection.instance.GetAudio("pickup"));

            Destroy(gameObject);
        }
    }
}
