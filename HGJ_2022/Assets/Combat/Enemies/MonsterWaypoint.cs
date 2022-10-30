using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaypoint : MonoBehaviour
{
    public MonsterWaypoint nextWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MonsterBehaviour monster = other.GetComponent<MonsterBehaviour>();

        if(monster != null && nextWaypoint != null)
        {
            monster.targettransform = nextWaypoint.transform;
        }
    }
}
