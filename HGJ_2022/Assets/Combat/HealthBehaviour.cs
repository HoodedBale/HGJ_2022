using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public int currenthealth, maxhealth;
    public AudioClip[] hitsound, diesound;
    public GameObject explosionVFX;
    public Image healthBarFlash;
    Color healthbarflashColour;
    float healthbarflashColourAlpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthbarflashColour = healthBarFlash.color;
    }

    // Update is called once per frame
    void Update()
    {
        healthbarflashColourAlpha -= 0.1f;
        healthbarflashColour.a = healthbarflashColourAlpha;
        healthBarFlash.color = healthbarflashColour;
    }

    public void DamageHealth(int damage = 0)
    {
        PlayHitSound();
        currenthealth -= damage;
        if (currenthealth < 0)
            currenthealth = 0;
        if (currenthealth > maxhealth)
            currenthealth = maxhealth;

        if (currenthealth == 0)
            Die();

        healthbarflashColourAlpha = 1;
    }

    public void UpgradeHealth(int upgrade = 0)
    {
        maxhealth += upgrade;
        currenthealth += upgrade;

        if (currenthealth < 0)
            currenthealth = 0;

        if (currenthealth == 0)
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
