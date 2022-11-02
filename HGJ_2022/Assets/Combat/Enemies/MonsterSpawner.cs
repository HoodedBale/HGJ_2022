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

    int currentWave = 0;
    public WaveSystemSO[] waveData;

    bool spawningWave = false;
    bool isWaiting = false;
    int currentSet = 0;

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

    IEnumerator spawnDelay()
    {
        foreach (WaveSystemSO.SpawnEntry entry in waveData[currentWave - 1].spawnList)
        {
            for (int count = 0; count < entry.monsterCount; count++) //spawn monsters up to monster count in that entry
            {
                GameObject monster = Instantiate(entry.monsterSpawned);
                MonsterBehaviour monsterbehaviour = monster.GetComponentInChildren<MonsterBehaviour>();
                monsterbehaviour.targettransform = targetWaypoint;
                monsterbehaviour.combattarget = targetGoal;
                Vector3 pos = monster.transform.position;
                pos.x = transform.position.x;
                pos.z = transform.position.z;
                monster.transform.position = pos;
            }

            yield return new WaitForSeconds(entry.nextSpawnDelay);
        }

        spawningWave = false;
    }

    void SpawnMonster()
    {
        if(waveTimer > 0 && !spawningWave)
        {
            waveTimer -= Time.deltaTime;
        }
        else if (!spawningWave)
        {
            ++currentWave;
            spawningWave = true;

            StartCoroutine(spawnDelay());

            waveTimer = subsequentWaveTimer;
        }
    }
}
