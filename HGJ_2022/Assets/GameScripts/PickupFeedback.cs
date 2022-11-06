using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PickupFeedback : MonoBehaviour
{
    public Vector3 goTo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deleteSelf());
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOLocalMove(goTo, 0.05f).SetEase(Ease.InSine);
    }

    IEnumerator deleteSelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
