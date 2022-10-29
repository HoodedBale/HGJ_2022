using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
    public GameStats.RESOURCE_TYPE resourceType = GameStats.RESOURCE_TYPE.CARDBOARD;
    public int count = 10;

    public delegate void OnConsume();
    public OnConsume onconsume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Billboard();
    }

    void Billboard()
    {
        Vector3 back = transform.position + (transform.position - Camera.main.transform.position);
        transform.LookAt(back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!GameStats.ResourceFull(count))
            {
                Destroy(gameObject);
                GameStats.resources[(int)resourceType] += count;

                if (onconsume != null)
                {
                    onconsume();
                }
            }
        }
    }
}
