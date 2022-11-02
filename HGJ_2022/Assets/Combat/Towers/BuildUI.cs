using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuildUI : MonoBehaviour
{
    public static BuildUI current;

    public TowerBaseBehaviour towerbase;
    public RectTransform buildPrompt;
    public GameObject buildOptions;
    public GameObject upgradeOptions;
    public Button upgradeBtn;

    public GameObject meleeTower;
    public GameObject sniperTower;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(towerbase != null)
        {
            Vector3 promptpos = towerbase.transform.position + new Vector3(0, 2, 0);
            buildPrompt.localPosition = Camera.main.WorldToScreenPoint(promptpos) - new Vector3(960, 540);
        }

        if(buildOptions.activeSelf || upgradeOptions.activeSelf)
        {
            DeactivateBuildOptions();
        }
        else
        {
            ActivateBuildOptions();
        }

        if(towerbase != null && towerbase.HasTower())
        {
            upgradeBtn.enabled = towerbase.tower.GetComponent<TowerBehaviour>().CanUpgrade();
        }

        MovementScript.current.disablemovement = buildOptions.activeSelf || upgradeOptions.activeSelf;
    }

    public void AssignTowerBase(TowerBaseBehaviour towerbase)
    {
        this.towerbase = towerbase;
        Vector3 promptpos = towerbase.transform.position + new Vector3(0, 2, 0);
        buildPrompt.localPosition = Camera.main.WorldToScreenPoint(promptpos) - new Vector3(960, 540);
        buildPrompt.localScale = new Vector3(0, 0, 1);
        buildPrompt.gameObject.SetActive(true);
        buildPrompt.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.InOutSine);
    }

    public void DeassignTowerBase(TowerBaseBehaviour towerbase)
    {
        if (this.towerbase != towerbase)
            return;

        this.towerbase = null;
        buildPrompt.localScale = new Vector3(0, 0, 1);
        buildPrompt.gameObject.SetActive(false);
        buildOptions.SetActive(false);
        upgradeOptions.SetActive(false);
    }

    void ActivateBuildOptions()
    {
        if(towerbase != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(!towerbase.HasTower())
                    buildOptions.SetActive(true);
                else
                {
                    upgradeOptions.SetActive(true);
                }

                buildPrompt.gameObject.SetActive(false);
            }
        }
    }

    void DeactivateBuildOptions()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            buildOptions.SetActive(false);
            upgradeOptions.SetActive(false);
            buildPrompt.gameObject.SetActive(true);
        }
    }

    public void BuildMelee()
    {
        if(towerbase == null)
        {
            return;
        }

        GameObject tower = TowerFactory.current.PurchaseTower(TowerFactory.TowerType.MELEE);
        if (tower == null)
            return;
        tower.transform.SetParent(towerbase.transform);
        tower.transform.localPosition = Vector3.zero;
        buildOptions.SetActive(false);
        towerbase.tower = tower;
    }
    public void BuildSniper()
    {
        if (towerbase == null)
        {
            return;
        }

        GameObject tower = TowerFactory.current.PurchaseTower(TowerFactory.TowerType.SNIPER);
        if (tower == null)
            return;
        tower.transform.SetParent(towerbase.transform);
        tower.transform.localPosition = Vector3.zero;
        buildOptions.SetActive(false);
        towerbase.tower = tower;
    }

    public void UpgradeTower()
    {
        if(towerbase != null && towerbase.HasTower())
        {
            towerbase.tower.GetComponent<TowerBehaviour>().Upgrade();
            upgradeOptions.SetActive(false);
        }
    }

    public void DestroyTower()
    {
        if (towerbase != null && towerbase.HasTower())
        {
            Destroy(towerbase.tower);
            towerbase.tower = null;
            upgradeOptions.SetActive(false);
        }
    }

}
