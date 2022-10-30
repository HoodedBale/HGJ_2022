using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int currenthealth, maxhealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DamageHealth(int damage = 0)
    {
        currenthealth -= damage;
        if (currenthealth < 0)
            currenthealth = 0;
        if (currenthealth > maxhealth)
            currenthealth = maxhealth;
        Die();
    }

    public void UpgradeHealth(int upgrade = 0)
    {
        maxhealth += upgrade;
        currenthealth += upgrade;

        if (currenthealth < 0)
            currenthealth = 0;
        Die();
    }

    void Die()
    {
        if(currenthealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
