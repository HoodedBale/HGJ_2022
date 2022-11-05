using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatStat
{
    public int damage;
    public float attacksPerSec;
    public float attackAnimDelay;
    public float range;
    public int targetcount;

    public int cardboardCost;
    public int plasticCost;
    public int metalCost;
}

public class CombatBehaviour : MonoBehaviour
{
    public enum CombatType
    {
        RANGE,
        MELEE,
        COUNT
    }

    public int level = 0;
    public List<CombatStat> statTree = new List<CombatStat>();

    public float attackrange;
    public float attackanimdelay;
    public float attackcooldown;
    public int targetcount = 1;
    public int damage;
    public GameObject projectilePrefab;
    public string targetTag;
    public CombatType combatType = CombatType.RANGE;

    public GameObject meleeAtkVFX;

    float atktimer = 0;
    bool attackanimplayed = false;
    List<GameObject> targets = new List<GameObject>();

    public GameObject turningModel;

    [Header("Sound")]
    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        LevelUp(0);
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

            Queue<GameObject> removequeue = new Queue<GameObject>();

            if(combatType == CombatType.RANGE)
            {
                int targetsHit = 0;
                for (int i = 0; (targetsHit < targetcount) && i < targets.Count; ++i)
                {
                    if (targets[i] == null)
                    {
                        removequeue.Enqueue(targets[i]);
                        continue;
                    }
                    if (turningModel != null)
                    {
                        Vector3 facedir = targets[i].transform.position - transform.position;
                        Vector3 rot = turningModel.transform.localRotation.eulerAngles;
                        float turnangle = Mathf.Atan2(facedir.x, facedir.z) * Mathf.Rad2Deg;
                        rot.y = turnangle;
                        turningModel.transform.localRotation = Quaternion.Euler(rot);
                    }

                    ++targetsHit;
                    GameObject projectile = Instantiate(projectilePrefab);
                    projectile.transform.position = transform.position;
                    projectile.GetComponent<ProjectileBehaviour>().target = targets[i];
                    projectile.GetComponent<ProjectileBehaviour>().damage = damage;

                }

                while (removequeue.Count > 0)
                {
                    GameObject toremove = removequeue.Dequeue();
                    targets.Remove(toremove);
                }

                if(targets.Count > 0)
                    PlayAttackSound();
            }
            else if(combatType == CombatType.MELEE)
            {
                for (int i = 0; i < targets.Count; ++i)
                {
                    if (targets[i] == null)
                    {
                        removequeue.Enqueue(targets[i]);
                        continue;
                    }

                    targets[i].GetComponentInChildren<HealthBehaviour>().DamageHealth(damage);
                    //GameObject projectile = Instantiate(projectilePrefab);
                    //projectile.transform.position = transform.position;
                    //projectile.GetComponent<ProjectileBehaviour>().target = targets[i];
                    //projectile.GetComponent<ProjectileBehaviour>().damage = damage;
                }

                while (removequeue.Count > 0)
                {
                    GameObject toremove = removequeue.Dequeue();
                    targets.Remove(toremove);
                }
                if (targets.Count > 0)
                {
                    GameObject particle = Instantiate(meleeAtkVFX, transform.position, Quaternion.Euler(90, 0, 0));
                }
                    PlayAttackSound();
            }
        }
        else
        {
            atktimer += Time.deltaTime;
        }
    }

    public void LevelUp(int newlevel)
    {
        level += newlevel;
        if (level < 0)
            level = 0;
        if (level >= statTree.Count)
            level = statTree.Count - 1;

        attackrange = statTree[level].range;
        attackanimdelay = statTree[level].attackAnimDelay;
        damage = statTree[level].damage;
        targetcount = statTree[level].targetcount;
        attackcooldown = 1 / statTree[level].attacksPerSec;

        if (GetComponent<CapsuleCollider>() != null)
            GetComponent<CapsuleCollider>().radius = attackrange;
        if (GetComponent<SphereCollider>() != null)
            GetComponent<SphereCollider>().radius = attackrange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag))
            return;
        if(!targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(targetTag))
            return;
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

    void PlayAttackSound()
    {
        if(attackSound != null)
        {
            SoundManager.current.PlaySound(attackSound, transform);
        }
    }
}
