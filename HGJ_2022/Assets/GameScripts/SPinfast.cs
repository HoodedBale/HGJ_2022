using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPinfast : MonoBehaviour
{
    [SerializeField] private Vector3 Rot;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Rot * speed * Time.deltaTime);
    }
}
