using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProfileScript : MonoBehaviour
{
    void Awake()
    {
        PlayerDataHandler.data = SaveSystem.Load();
        if (PlayerDataHandler.data == null)
        {
            PlayerDataHandler.data = new PlayerData();
        }
    }
}
