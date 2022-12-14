using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float dashTimer = 0;
    bool dashing = false;
    HashSet<GameObject> dashedEnemies = new HashSet<GameObject>();

    public GameObject hornEffect;
    public AudioSource hornSound;
    float hornTimer = 0;
    public float hornCD = 5;

    public Text hornCDText, dashCDText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        HonkHorn();
    }

    void Dash()
    {
        dashTimer -= Time.deltaTime;
        dashCDText.text = string.Format("{0}", (int)dashTimer);    
        if(dashTimer <= 0)
        {
            dashCDText.text = "SPC";
            if(Input.GetKeyDown(KeyCode.Space))
            {
                float angle = transform.eulerAngles.y;
                Vector3 direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
                GetComponent<Rigidbody>().AddForce(direction * dashUpgrade[dashLevel].force);
                //GetComponent<MovementScript>().movementspeed *= 2;
                dashing = true;
                StartCoroutine(DashTimer());
                dashTimer = dashUpgrade[dashLevel].cooldown;
                GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(0.4f);
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

    void HonkHorn()
    {
        hornTimer -= Time.deltaTime;
        hornCDText.text = string.Format("{0}", (int)hornTimer);
        if(hornTimer <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!hornSound.isPlaying)
                {
                    hornSound.Play();
                }
                Instantiate(hornEffect, transform);
                hornTimer = hornCD;
            }
            hornCDText.text = "RMB";
        }
    }
}
