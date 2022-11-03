using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [System.Serializable]
    public class TowerItem
    {
        public GameObject prefab;
        public int cardboardCost, plasticCost, metalCost;
    }

    public enum TowerType
    {
        MELEE,
        SNIPER,
        COUNT
    }

    public List<TowerItem> towermenu;
    public static TowerFactory current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject PurchaseTower(TowerType towertype)
    {
        TowerItem item = towermenu[(int)towertype];
        if(GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD] >= item.cardboardCost &&
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC] >= item.plasticCost &&
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL] >= item.metalCost)
        {
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD] -= item.cardboardCost;
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC] -= item.plasticCost;
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL] -= item.metalCost;

            return Instantiate(item.prefab);
        }

        return null;
    }

    public int GetCardboardCost(TowerType towerType)
    {
        return towermenu[(int)towerType].cardboardCost;
    }
    public int GetPlasticCost(TowerType towerType)
    {
        return towermenu[(int)towerType].plasticCost;
    }
    public int GetMetalCost(TowerType towerType)
    {
        return towermenu[(int)towerType].metalCost;
    }
}
