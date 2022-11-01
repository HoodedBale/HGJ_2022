using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildUI : MonoBehaviour
{
    public static BuildUI current;

    public TowerBaseBehaviour towerbase;
    public RectTransform buildPrompt;
    public GameObject buildOptions;

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

        if(buildOptions.activeSelf)
        {
            DeactivateBuildOptions();
        }
        else
        {
            ActivateBuildOptions();
        }

        MovementScript.current.disablemovement = buildOptions.activeSelf;
    }

    public void AssignTowerBase(TowerBaseBehaviour towerbase)
    {
        this.towerbase = towerbase;
        Vector3 promptpos = towerbase.transform.position + new Vector3(0, 2, 0);
        if(Camera.main == null)
        {
            Debug.Log("help");
        }
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
    }

    void ActivateBuildOptions()
    {
        if(towerbase != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                buildOptions.SetActive(true);
            }
        }
    }

    void DeactivateBuildOptions()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            buildOptions.SetActive(false);
        }
    }

    public void BuildMelee()
    {
        if(towerbase == null)
        {
            return;
        }

        GameObject tower = Instantiate(meleeTower, towerbase.transform);
        tower.transform.localPosition = Vector3.zero;
        buildOptions.SetActive(false);
    }
    public void BuildSniper()
    {
        if (towerbase == null)
        {
            return;
        }

        GameObject tower = Instantiate(sniperTower, towerbase.transform);
        tower.transform.localPosition = Vector3.zero;
        buildOptions.SetActive(false);
    }
}
