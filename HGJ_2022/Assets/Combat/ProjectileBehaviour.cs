using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;
    public float aoe;

    HashSet<GameObject> withinrange = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = aoe;
    }

    // Update is called once per frame
    void Update()
    {
        Homing();
    }

    void Homing()
    {
        if((transform.position - target.position).sqrMagnitude > 0.5f)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            GetComponent<Rigidbody>().velocity = direction * speed;
        }
        else
        {
            foreach(GameObject body in withinrange)
            {
                HealthBehaviour health = body.GetComponent<HealthBehaviour>();
                if(health != null)
                {
                    health.DamageHealth(damage);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!withinrange.Contains(other.gameObject))
        {
            withinrange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(withinrange.Contains(other.gameObject))
        {
            withinrange.Remove(other.gameObject);
        }
    }
}
