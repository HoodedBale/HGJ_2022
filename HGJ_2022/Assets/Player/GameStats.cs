using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public static float BGM_VOLUME = 1;
    public static float SFX_VOLUME = 1;
    public enum RESOURCE_TYPE
    {
        CARDBOARD,
        PLASTIC,
        METAL,
        COUNT
    };

    public static int[] resources = new int[(int)RESOURCE_TYPE.COUNT];
    public static int resourceCapacity = 100;
    public static int health = 100, maxhealth = 100;

    /*
    public static bool ResourceFull(int newResource = 0)
    {
        int totalres = newResource;
        for(int i = 0; i < (int)RESOURCE_TYPE.COUNT; ++i)
        {
            totalres += resources[i];
        }

        return totalres > resourceCapacity;
    }
    */

    public static bool ResourceFull(RESOURCE_TYPE resource, int toadd = 0)
    {
        return (resources[(int)resource] + toadd > resourceCapacity);
    }

    public static void Reset()
    {
        for(int i = 0; i < resources.Length; ++i)
        {
            resources[i] = 0;
        }
        health = 100;
        maxhealth = 100;
    }
}
