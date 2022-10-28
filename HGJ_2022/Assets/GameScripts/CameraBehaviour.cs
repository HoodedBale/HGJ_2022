using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform initialFollow;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - initialFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = Vector3.Slerp(transform.position, initialFollow.position + offset, 0.1f);
    }
}
