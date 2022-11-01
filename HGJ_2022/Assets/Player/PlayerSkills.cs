using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [System.Serializable]
    public class DashStat
    {
        public int damage;
        public float cooldown;
    }

    public List<DashStat> dashUpgrade;
    public int dashLevel;
    float dashTimer;
    bool dashing = false;

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
                //GetComponent<Rigidbody>().AddForce(direction * 1000);
                GetComponent<MovementScript>().movementspeed *= 2;
                dashing = true;
                StartCoroutine(DashTimer());
            }
        }
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(0.5f);
        dashing = false;
        GetComponent<MovementScript>().movementspeed /= 2;
    }

    public bool Dashing()
    {
        return dashing;
    }
}
