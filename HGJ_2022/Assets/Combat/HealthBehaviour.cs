using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int currenthealth, maxhealth;
    public AudioClip[] hitsound, diesound;
    public GameObject explosionVFX;
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
        PlayHitSound();
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
        PlayDieSound();
        Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void PlayHitSound()
    {
        int id = Random.Range(0, hitsound.Length);
        SoundManager.current.PlaySound(hitsound[id], transform.position);
    }

    void PlayDieSound()
    {
        int id = Random.Range(0, diesound.Length);
        SoundManager.current.PlaySound(diesound[id], transform.position);
    }
}
