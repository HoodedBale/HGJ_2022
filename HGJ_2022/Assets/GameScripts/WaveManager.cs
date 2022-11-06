using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class WaveManager : MonoBehaviour
{
    public float startingWaveTimer; //duration between game start and first wave
    public float subsequentWaveTimer; //duration between end of any wave and start of subsequent wave

    public float currWaveTimer = 0;

    public List<MonsterSpawner> spawnerList;

    public int currentWaveMaster = 0;
    public int maxWave = 5;

    [SerializeField]
    bool waveActive = false;

    public

    // Start is called before the first frame update
    void Start()
    {
        currWaveTimer = startingWaveTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (currWaveTimer > 0 && !waveActive)
        {
            currWaveTimer -= Time.deltaTime;
        }

        else if (currWaveTimer <= 0 && !waveActive)
        {
            startWave();
        }
    }

    public void addSpawner(MonsterSpawner spawner)
    {
        spawnerList.Add(spawner);
    }

    public void startWave()
    {
        waveActive = true;
        ++currentWaveMaster;
        foreach (MonsterSpawner spawner in spawnerList)
        {
            spawner.startWave(currentWaveMaster - 1);
        }
    }

    public void checkSpawnFinish()
    {
        if (waveActive)
        {
            foreach (MonsterSpawner spawner in spawnerList)
            {
                if (!spawner.spawningWave)
                {
                    Debug.Log(spawner.gameObject.name + " finished for wave " + currentWaveMaster);
                }
                else
                {
                    Debug.Log(spawner.gameObject.name + "not finished for wave " + currentWaveMaster);
                    return;
                }
            }
            waveActive = false;
            currWaveTimer = subsequentWaveTimer;
        }
    }
}
