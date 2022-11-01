using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public CombatBehaviour combatbehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanUpgrade()
    {
        if(combatbehaviour.level == combatbehaviour.statTree.Count - 1)
        {
            return false;
        }

        CombatStat stat = combatbehaviour.statTree[combatbehaviour.level + 1];
        return
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD] >= stat.cardboardCost &&
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC] >= stat.plasticCost &&
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL] >= stat.metalCost;
    }

    public void Upgrade()
    {
        if(CanUpgrade())
        {
            CombatStat stat = combatbehaviour.statTree[combatbehaviour.level + 1];
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.CARDBOARD] -= stat.cardboardCost;
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.PLASTIC] -= stat.plasticCost;
            GameStats.resources[(int)GameStats.RESOURCE_TYPE.METAL] -= stat.metalCost;
            combatbehaviour.LevelUp(1);
        }
    }
}
