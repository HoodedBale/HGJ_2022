using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public List<float> speedUpgrade;
    public int speedLevel = 0;

    public float movementspeed = 10;
    public float turntime = 0.1f;
    float turndamper = 0;

    public static MovementScript current;
    public bool disablemovement = false;

    public AudioSource footsteps;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disablemovement)
            Movement();
        else
            GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void Movement()
    {
        if (GetComponent<PlayerSkills>().Dashing())
            return;

        Vector3 movementaxis = Vector3.zero;
        if(Input.GetKey(KeyCode.A))
        {
            movementaxis.x = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            movementaxis.x = 1;
        }
        if(Input.GetKey(KeyCode.W))
        {
            movementaxis.z = 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            movementaxis.z = -1;
        }

        if(footsteps != null)
        {
            if (movementaxis.sqrMagnitude > 0)
            {
                footsteps.UnPause();
            }
            else
            {
                footsteps.Pause();
            }
        }
        animator.SetBool("running", movementaxis.sqrMagnitude > 0);

        movementaxis.Normalize();
        GetComponent<Rigidbody>().velocity = movementaxis * speedUpgrade[speedLevel];

        if (movementaxis.sqrMagnitude <= 0)
            return;

        float targetangle = Mathf.Atan2(movementaxis.x, movementaxis.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, targetangle, ref turndamper, turntime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void LevelUp(int level)
    {
        speedLevel += level;
        if (speedLevel >= speedUpgrade.Count)
        {
            speedLevel = speedUpgrade.Count - 1;
        }

        if(speedLevel < 0)
        {
            speedLevel = 0;
        }
    }
}
