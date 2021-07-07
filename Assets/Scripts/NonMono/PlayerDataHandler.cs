using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataHandler
{
    public static PlayerData data;
    
    public static void Addresource(ResourceType type, int amount)
    {
        data.Resources[(int)type] += amount;
    }

    public static void RemoveResource(ResourceType type, int amount)
    {
        data.Resources[(int)type] -= amount;
    }
}
