using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseBehaviour : MonoBehaviour
{
    bool inrange = false;
    public GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasTower()
    {
        return tower != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inrange = true;
            BuildUI.current.AssignTowerBase(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            inrange = false;
            BuildUI.current.DeassignTowerBase(this);
        }
    }
}
