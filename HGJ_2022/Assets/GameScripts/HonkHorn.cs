using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkHorn : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float currDuration = 0;
    public float maxDuration = 0.3f;
    
    Vector3 startScale;
    public float maxScale = 20f;

    Renderer renderer;
    Color startColor;

    float progress;

    void Start()
    {
        startScale = transform.localScale;

        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        currDuration += Time.deltaTime;
        progress = currDuration / maxDuration;
        
        if (progress >= 1)
        {
            Destroy(gameObject);
        }

        updateEffect();
    }

    void updateEffect()
    {
        transform.localScale = Vector3.Lerp(startScale, startScale * maxScale, progress);
        renderer.material.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0), progress);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House"))
        {
            other.GetComponent<HouseBehaviour>().SpawnImmediate();
        }
        //if other collider is house, do X
    }
}
