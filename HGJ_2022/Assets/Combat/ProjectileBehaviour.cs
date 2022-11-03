using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public int damage;
    public float aoe;
    public string targettag;

    public GameObject projectileHitVFX;

    HashSet<GameObject> withinrange = new HashSet<GameObject>();
    bool kaboom = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = aoe;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            Destroy(gameObject);
        else
        {
            Homing();
        }
    }

    void Homing()
    {
        if((transform.position - target.transform.position).sqrMagnitude > 0.5f)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.Normalize();
            GetComponent<Rigidbody>().velocity = direction * speed;
        }
        if(kaboom)
        {
            foreach(GameObject body in withinrange)
            {
                if (body == null)
                    continue;
                HealthBehaviour health = body.GetComponent<HealthBehaviour>();
                if(health != null)
                {
                    health.DamageHealth(damage);
                }
            }
            Instantiate(projectileHitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targettag))
            return;
        if(!withinrange.Contains(other.gameObject))
        {
            withinrange.Add(other.gameObject);
        }

        if(other.gameObject == target)
        {
            kaboom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(targettag))
            return;
        if (withinrange.Contains(other.gameObject))
        {
            withinrange.Remove(other.gameObject);
        }
    }
}
