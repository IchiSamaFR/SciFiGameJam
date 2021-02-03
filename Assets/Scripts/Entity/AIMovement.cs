using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GeometricTools;

public class AIMovement : MonoBehaviour
{
    AIManager aiManager;
    Quaternion rotationToGetEuler = new Quaternion();
    Rigidbody rb;

    [SerializeField]
    Transform target;

    public float actualSpeed { get => GetHypot(new Vector2(rb.velocity.x, rb.velocity.z)); }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* Turn direction of Target
         * if (distance < distanceMin && angleWithTarget < angleMin)
         *     angleWithTarget ++
         * if(target)
         *     Go direction of 
         * 
         */
        Movement();
    }

    void Movement()
    {

        float x = GetAngle(new Vector2(transform.forward.x, transform.forward.z));
        float y = GetAngle(new Vector2(target.transform.position.x - transform.position.x ,
                                       target.transform.position.z - transform.position.z));

        float diff = AngleDiff(x, y);
        float distBetween = GetHypot(new Vector2(transform.position.x - target.transform.position.x,
                                                 transform.position.z - target.transform.position.z));
        float yRotation = transform.rotation.eulerAngles.y;

        /* Check angle and distance with target to automatically make a distance between them
         */
        if (((diff <= 0 && diff > -10) || (diff >= 0 && diff < 10)) 
            && distBetween > 5)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation - 20, 3 * Time.deltaTime), 0);
        }
        else if (((diff <= 0 && diff > -180) || (diff <= 0 && diff > 180)) 
            && distBetween < 3)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation - 20, 5 * Time.deltaTime), 0);
        }
        else if (((diff <= 0 && diff > -50) || (diff >= 0 && diff < 50)) 
            && distBetween < 5)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation - 20, 5 * Time.deltaTime), 0);
        }
        /* -- 3
         */
        else if ((diff > 60 && diff < 180) || (diff < -60 && diff > -180))
        {
            if(distBetween > 4)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation - diff, Time.deltaTime), 0);
            }
            else if (distBetween < 3f)
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation + diff, 2 * Time.deltaTime), 0);
            }
        }

        transform.position += transform.forward * Time.deltaTime * 2;
    }
}
