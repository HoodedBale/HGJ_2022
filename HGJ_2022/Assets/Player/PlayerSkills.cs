using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [System.Serializable]
    public class DashStat
    {
        public int damage;
        public float force;
        public float cooldown;
    }

    public List<DashStat> dashUpgrade;
    public int dashLevel;
    float dashTimer;
    bool dashing = false;
    HashSet<GameObject> dashedEnemies = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    void Dash()
    {
        dashTimer += Time.deltaTime;
        if(dashTimer >= dashUpgrade[dashLevel].cooldown)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                float angle = transform.eulerAngles.y;
                Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
                GetComponent<Rigidbody>().AddForce(direction * dashUpgrade[dashLevel].force);
                //GetComponent<MovementScript>().movementspeed *= 2;
                dashing = true;
                StartCoroutine(DashTimer());
                dashTimer = 0;
                GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(0.5f);
        dashing = false;
        dashedEnemies.Clear();
        GetComponent<CapsuleCollider>().isTrigger = false;
        //GetComponent<MovementScript>().movementspeed /= 2;
    }

    public bool Dashing()
    {
        return dashing;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != null)
        {
            if(collision.gameObject.tag == "Enemy"&& Dashing())
            {
                collision.gameObject.GetComponent<HealthBehaviour>().DamageHealth(dashUpgrade[dashLevel].damage);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != null && !dashedEnemies.Contains(collision.gameObject))
        {
            if (collision.gameObject.tag == "Enemy" && Dashing())
            {
                collision.gameObject.GetComponent<HealthBehaviour>().DamageHealth(dashUpgrade[dashLevel].damage);
                dashedEnemies.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject != null && !dashedEnemies.Contains(other.gameObject))
        {
            if (other.gameObject.tag == "Enemy" && Dashing())
            {
                other.gameObject.GetComponent<HealthBehaviour>().DamageHealth(dashUpgrade[dashLevel].damage);
                dashedEnemies.Add(other.gameObject);
            }
        }
    }
}
