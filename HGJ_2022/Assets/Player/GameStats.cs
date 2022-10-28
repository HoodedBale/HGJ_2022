using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public enum RESOURCE_TYPE
    {
        CARDBOARD,
        PLASTIC,
        METAL,
        COUNT
    };

    public static int[] resources = new int[(int)RESOURCE_TYPE.COUNT];

    public static void Reset()
    {
        for(int i = 0; i < resources.Length; ++i)
        {
            resources[i] = 0;
        }
    }
}
