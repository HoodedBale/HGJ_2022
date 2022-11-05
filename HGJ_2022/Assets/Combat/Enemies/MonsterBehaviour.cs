using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    public enum STATE
    {
        PATROLLING,
        ENGAGING,
        ATTACKING
    }

    public STATE currentstate;
    public float movementspeed;
    public Transform targettransform;
    public float turntime = 0.1f;
    public CombatBehaviour combatbehaviour;
    public Transform combattarget;
    public float engagerange = 5;
    
    float turndamper = 0;
    NavMeshAgent agent;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 localpos = transform.localPosition;
        localpos.x = 0;
        localpos.z = 0;
        transform.localPosition = localpos;
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentstate == STATE.PATROLLING)
        {
            agent.updatePosition = true;
            Patrolling();
        }

        if((transform.position - combattarget.position).sqrMagnitude <= engagerange * engagerange)
        {
            targettransform = combattarget;
        }

        if((transform.position - combattarget.position).sqrMagnitude <= combatbehaviour.attackrange * combatbehaviour.attackrange)
        {
            currentstate = STATE.ATTACKING;
            combatbehaviour.gameObject.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            combatbehaviour.gameObject.SetActive(true);
            currentstate = STATE.PATROLLING;
        }
    }

    void Patrolling()
    {
        if(targettransform == null)
        {
            return;
        }

        agent.SetDestination(targettransform.position);
        //Vector3 targetposition = targettransform.position;
        //Vector3 direction = targetposition - transform.position;
        //direction.Normalize();
        //GetComponent<Rigidbody>().velocity = direction * movementspeed;
        //
        //float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //float angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, targetangle, ref turndamper, turntime);
        //transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
