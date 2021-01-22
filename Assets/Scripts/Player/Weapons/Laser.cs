using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int damage;
    private float speed;
    private float range;

    public void Set(int damage, float speed, float range)
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, speed * Time.deltaTime)
            && hit.transform.tag != "Player")
        {
            EntityStats _entity = hit.transform.GetComponent<EntityStats>();
            if(_entity != null)
            {
                _entity.GetDamage(damage);
            }
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
