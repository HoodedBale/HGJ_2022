using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
    //public GameStats.RESOURCE_TYPE resourceType = GameStats.RESOURCE_TYPE.CARDBOARD;
    //public int count = 10;

    public int cardboardmin, cardboardmax, plasticmin, plasticmax, metalmin, metalmax;

    public delegate void OnConsume();
    public OnConsume onconsume;
    int cardboard, plastic, metal;

    public AudioClip[] pickupSound;

    public GameObject feedbackFX, feedbackFXSpawner;

    // Start is called before the first frame update
    void Start()
    {
        cardboard = Random.Range(cardboardmin, cardboardmax + 1);
        plastic = Random.Range(plasticmin, plasticmax + 1);
        metal = Random.Range(metalmin, metalmax + 1);

        feedbackFXSpawner = GameObject.Find("PickupFeedback");
    }

    // Update is called once per frame
    void Update()
    {
        //Billboard();
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
            if(GameStats.ResourceFull(GameStats.RESOURCE_TYPE.CARDBOARD, cardboard) ||
                GameStats.ResourceFull(GameStats.RESOURCE_TYPE.PLASTIC, plastic) ||
                GameStats.ResourceFull(GameStats.RESOURCE_TYPE.METAL, metal))
            {
                return;
            }
            CitizenVoices.current.PlayCitizenVoice(transform.position);

            Destroy(gameObject);
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD] += cardboard;
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC] += plastic;
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL] += metal;
            PlayPickupAudio();
            //CitizenVoices.current.PlayCitizenVoice();

            Instantiate(feedbackFX, feedbackFXSpawner.transform);

            if (onconsume != null)
            {
                onconsume();
            }

        }
    }

    void PlayPickupAudio()
    {
        int id = Random.Range(0, pickupSound.Length);
        SoundManager.current.PlaySound(pickupSound[id]);
    }
}
