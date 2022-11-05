using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform targetWaypoint;
    public Transform targetGoal;

    public WaveManager waveMgr;

    public WaveSO[] waveData;
    public bool spawningWave = false;

    bool isWaiting = false;
    int currentSet = 0;

    // Start is called before the first frame update
    void Start()
    {
        waveMgr.addSpawner(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnDelay(WaveSO currWave)
    {
        Debug.Log("spawnDelay " + gameObject.name);
        foreach (WaveSO.SpawnEntry entry in currWave.spawnList)
        {
            
            for (int count = 0; count < entry.monsterCount; count++) //spawn monsters up to monster count in that entry
                {

                    GameObject monster = Instantiate(entry.monsterSpawned);
                    MonsterBehaviour monsterbehaviour = monster.GetComponentInChildren<MonsterBehaviour>();
                    monsterbehaviour.targettransform = targetWaypoint;
                    monsterbehaviour.combattarget = targetGoal;
                    Vector3 pos = monster.transform.position;
                    pos.x = transform.position.x;
                    //pos.y = transform.position.y;
                    pos.z = transform.position.z;
                    monster.transform.position = pos;
                yield return new WaitForSeconds(0.75f);
                }
            yield return new WaitForSeconds(entry.nextSpawnDelay);
        }
        endSpawning();
    }

    void endSpawning()
    {
        Debug.Log("endSpawning " + gameObject.name);
        spawningWave = false;
        waveMgr.checkSpawnFinish();
    }

    public void startWave(int waveNumber)
    {
        Debug.Log("startWave " + waveNumber + " " + gameObject.name);
        if (!spawningWave && waveData[waveNumber] != null)
        {
            spawningWave = true;

            StartCoroutine(spawnDelay(waveData[waveNumber]));
        }
    }
}
