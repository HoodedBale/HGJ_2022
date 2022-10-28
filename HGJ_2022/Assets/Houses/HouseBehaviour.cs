using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    public GameObject resourcePrefab;
    public float minTime, maxTime;

    float currentTime;

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
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            GameObject resource = Instantiate(resourcePrefab);
            resource.GetComponent<ResourceBehaviour>().resourceType = (GameStats.RESOURCE_TYPE)Random.Range(0, (int)GameStats.RESOURCE_TYPE.COUNT);
            resource.GetComponent<ResourceBehaviour>().count = Random.Range(1, 6) * 10;
            resource.transform.position = resourceTargetPosition.position;
            currentTime = Random.Range(minTime, maxTime);
        }
    }
}
