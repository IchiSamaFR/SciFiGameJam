using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int damage;
    private float speed;
    private float range;
    private string faction;

    public void Set(int damage, float speed, float range, string faction = "player")
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, speed * Time.deltaTime))
        {
            EntityStats _entity = hit.transform.GetComponent<EntityStats>();
            if (_entity != null && _entity.Faction != faction)
            {
                _entity.GetDamage(damage);

            }
            GameObject obj = Instantiate(PrefabCollection.instance.GetPrefab("fxAudio"));
            obj.transform.position = transform.position;
            obj.GetComponent<FXAudio>().Set(AudioCollection.instance.GetAudio("hit"));
            Destroy(gameObject);
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            if(transform.position.y < 0.1f)
            {
                transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }

    }
}
