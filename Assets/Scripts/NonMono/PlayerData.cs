using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    // 32 positions for resources must have for future purposes. Can be increased later
    public int[] Resources = new int[32];

    public PlayerData()
    {
        for (int i = 0; i < Resources.Length; i++)
        {
            Resources[i] = 0;
        }
    }
}
