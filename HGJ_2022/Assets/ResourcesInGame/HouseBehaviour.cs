using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HouseBehaviour : MonoBehaviour
{
    //public List<GameObject> resourcePrefabs = new List<GameObject>();
    public LootTableSO resourceLootTable;
    public float minTime, maxTime;
    public Vector2 minSpawnArea, maxSpawnArea;

    float currentTime;
    bool hasResource = false;
    int currentResources = 0;

    public Transform resourceTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnResource();
        SpawnResources();
    }

    void SpawnResource()
    {
        if(currentTime > 0 && !hasResource)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentTime <= 0)
        {
            GameObject resource = Instantiate(resourceLootTable.RollLoot());
            resource.transform.position = resourceTargetPosition.position;
            currentTime = Random.Range(minTime, maxTime);
            hasResource = true;
            resource.GetComponent<ResourceBehaviour>().onconsume += () =>
             {
                 hasResource = false;
             };
        }
    }

    void SpawnResources()
    {
        if (currentTime > 0 && currentResources == 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentTime <= 0)
        {
            GameObject[] resources = resourceLootTable.RollLoots();
            for(int i = 0; i < resources.Length; ++i)
            {
                GameObject resource = Instantiate(resources[i]);
                resource.transform.position = transform.position;

                Vector3 targetloc = resourceTargetPosition.position;
                targetloc.x += Random.Range(minSpawnArea.x, maxSpawnArea.x);
                targetloc.z += Random.Range(minSpawnArea.y, maxSpawnArea.y);
                resource.transform.DOLocalMove(targetloc, 0.5f).SetEase(Ease.InSine);

                currentTime = Random.Range(minTime, maxTime);
                resource.GetComponent<ResourceBehaviour>().onconsume += () =>
                {
                    --currentResources;
                };

                ++currentResources;
            }
        }
    }
}
