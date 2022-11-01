using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform targetWaypoint;
    public Transform targetGoal;

    public float startingWaveTimer; //duration between game start and first wave
    public float subsequentWaveTimer; //duration between end of any wave and start of subsequent wave

    float waveTimer = 0;

    bool isWaiting = false;

    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject monsterSpawned;
        public int monsterCount;
        public float nextSpawnDelay;
    }

    public SpawnEntry[] spawnList;

    // Start is called before the first frame update
    void Start()
    {
        waveTimer = startingWaveTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMonster();
    }

    IEnumerator spawnDelay(float d)
    {
        isWaiting = true;
        yield return new WaitForSeconds(d);
        isWaiting = false;
    }

    void SpawnMonster()
    {
        if(waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }
        else
        {
            foreach (SpawnEntry entry in spawnList)
            {
                for (int count = 0; count < entry.monsterCount; count++) //spawn monsters up to monster count in that entry
                {
                    GameObject monster = Instantiate(entry.monsterSpawned);
                    monster.GetComponent<MonsterBehaviour>().targettransform = targetWaypoint;
                    monster.GetComponent<MonsterBehaviour>().combattarget = targetGoal;
                    Vector3 pos = monster.transform.position;
                    pos.x = transform.position.x;
                    pos.z = transform.position.z;
                    monster.transform.position = pos;
                }

                spawnDelay(entry.nextSpawnDelay); //I forgot how coroutines work lol, hope this doesnt boom
                
                while (isWaiting)
                {
                    //do nothing until coroutine ends
                }
            }

            waveTimer = subsequentWaveTimer;
        }
    }
}
