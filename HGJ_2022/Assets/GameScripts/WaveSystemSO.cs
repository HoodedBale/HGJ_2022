using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/WaveSystem")]
public class WaveSystemSO : ScriptableObject
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject monsterSpawned;
        public int monsterCount;
        public float nextSpawnDelay;
    }

    //public SpawnEntry[] spawnList;
    public List<SpawnEntry> spawnList = new List<SpawnEntry>();
}
