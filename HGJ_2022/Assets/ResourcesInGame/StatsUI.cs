using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StatsUI : MonoBehaviour
{
    public Text cardboardTxt, plasticTxt, metalTxt;
    public HealthBehaviour lorryHealth;
    public RectTransform healthbar;
    float maxhealthsize;

    // Start is called before the first frame update
    void Start()
    {
        maxhealthsize = healthbar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        cardboardTxt.text = string.Format("{0}", GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD]);
        plasticTxt.text = string.Format("{0}", GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC]);
        metalTxt.text = string.Format("{0}", GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL]);
        UpdateHealth();
    }

    void UpdateHealth()
    {
        Vector2 healthbarsize = healthbar.sizeDelta;
        healthbarsize.x = lorryHealth.currenthealth / (float)lorryHealth.maxhealth * maxhealthsize;
        healthbar.sizeDelta = healthbarsize;
        //healthbar.DOSizeDelta(healthbarsize, 0.5f).SetEase(Ease.InSine);
    }
}
