using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnCooldown;
    public Transform targetWaypoint;
    public Transform targetGoal;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMonster();
    }

    void SpawnMonster()
    {
        if(timer < spawnCooldown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            GameObject monster = Instantiate(monsterPrefab);
            monster.GetComponent<MonsterBehaviour>().targettransform = targetWaypoint;
            monster.GetComponent<MonsterBehaviour>().combattarget = targetGoal;
            Vector3 pos = monster.transform.position;
            pos.x = transform.position.x;
            pos.z = transform.position.z;
            monster.transform.position = pos;
        }
    }
}
