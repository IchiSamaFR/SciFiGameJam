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
    Transform target { get => aiManager.target; }

    public float actualSpeed { get => GetHypot(new Vector2(rb.velocity.x, rb.velocity.z)); }

    private void Awake()
    {
        aiManager = GetComponent<AIManager>();
    }

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
        if(target != null)
        {
            TrackTarget();
        }
    }

    void TrackTarget()
    {
        float x = GetAngle(new Vector2(transform.forward.x, transform.forward.z));
        float y = GetAngle(new Vector2(target.transform.position.x - transform.position.x,
                                       target.transform.position.z - transform.position.z));

        float diff = AngleDiff(x, y);
        float distBetween = GetHypot(new Vector2(transform.position.x - target.transform.position.x,
                                                 transform.position.z - target.transform.position.z));
        float yRotation = transform.rotation.eulerAngles.y;


        if (CheckRaycast("right"))
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation - 50, 3 * Time.deltaTime), 0);
        }
        else if (CheckRaycast("left"))
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(yRotation, yRotation + 50, 3 * Time.deltaTime), 0);
        }
        /* Check angle and distance with target to automatically make a distance between them
         */
        else if (((diff <= 0 && diff > -10) || (diff >= 0 && diff < 10))
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
        /* Turn around the target
         */
        else if ((diff > 60 && diff < 180) || (diff < -60 && diff > -180))
        {
            if (distBetween > 4)
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

    bool CheckRaycast(string pos, int dist = 1)
    {
        if(pos == "right")
        {
            Vector2 vec = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 0, dist);
            Vector3 endPos = new Vector3(vec.x, 0, vec.y);

            Vector2 vec2 = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 45, dist);
            Vector3 endPos2 = new Vector3(vec2.x, 0, vec2.y);

            Vector2 vec3 = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 70, dist);
            Vector3 endPos3 = new Vector3(vec3.x, 0, vec3.y);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(endPos), out hit, dist)
                || Physics.Raycast(transform.position, transform.TransformDirection(endPos2), out hit, dist)
                || Physics.Raycast(transform.position, transform.TransformDirection(endPos3), out hit, dist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (pos == "left")
        {
            Vector2 vec = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 135, dist);
            Vector3 endPos = new Vector3(vec.x, 0, vec.y);

            Vector2 vec2 = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 180, dist);
            Vector3 endPos2 = new Vector3(vec2.x, 0, vec2.y);

            Vector2 vec3 = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 205, dist);
            Vector3 endPos3 = new Vector3(vec3.x, 0, vec3.y);


            Debug.DrawLine(transform.position, endPos + transform.position, new Color(1, 0, 0));
            Debug.DrawLine(transform.position, endPos2 + transform.position, new Color(1, 0, 0));
            Debug.DrawLine(transform.position, endPos3 + transform.position, new Color(1, 0, 0));

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(endPos), out hit, dist)
                || Physics.Raycast(transform.position, transform.TransformDirection(endPos2), out hit, dist)
                || Physics.Raycast(transform.position, transform.TransformDirection(endPos3), out hit, dist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (pos == "forward")
        {
            Vector2 vec = GetSidesByAngleHypot((360 - transform.rotation.eulerAngles.y) + 90, dist);
            Vector3 endPos = new Vector3(vec.x, 0, vec.y);
            Debug.DrawLine(transform.position, endPos + transform.position, new Color(1, 0, 0));

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(endPos), out hit, dist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
