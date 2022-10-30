using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    public float attackrange;
    public float attackanimdelay;
    public float attackcooldown;
    public int targetcount = 1;
    public GameObject projectilePrefab;

    float atktimer = 0;
    bool attackanimplayed = false;
    List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider>().radius = attackrange;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (atktimer >= attackcooldown - attackanimdelay && !attackanimplayed)
        {
            attackanimplayed = true;
        }
        else if (atktimer >= attackcooldown)
        {
            attackanimplayed = false;
            atktimer = 0;

            for(int i = 0; i < targetcount && i < targets.Count; ++i)
            {
                GameObject projectile = Instantiate(projectilePrefab);
                projectile.transform.position = transform.position;
                projectile.GetComponent<ProjectileBehaviour>().target = targets[i].transform;
            }
        }
        else
        {
            atktimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }
}
