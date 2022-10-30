using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LootTable")]
public class LootTableSO : ScriptableObject
{
    [System.Serializable]
    public class LootItem
    {
        public GameObject item;
        public int chance;
    }

    public List<LootItem> table = new List<LootItem>();

    public GameObject RollLoot()
    {
        int roll = Random.Range(0, 100);

        for(int i = 0; i < table.Count; ++i)
        {
            if(roll < table[i].chance)
            {
                return table[i].item;
            }
            else
            {
                roll -= table[i].chance;
            }
        }

        return null;
    }
}
