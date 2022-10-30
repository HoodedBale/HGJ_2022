using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    //public List<GameObject> resourcePrefabs = new List<GameObject>();
    public LootTableSO resourceLootTable;
    public float minTime, maxTime;

    float currentTime;
    bool hasResource = false;

    public Transform resourceTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnResource();
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
}
