using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Resource
{
    public static Dictionary<ResourceType, GameObject> ResourcePrefabs = new Dictionary<ResourceType, GameObject>()
    {
        { ResourceType.wood, Resources.Load<GameObject>("Prefabs/wood") },
        { ResourceType.crystal, Resources.Load<GameObject>("Prefabs/gem") },
    };
}

public enum ResourceType
{
    wood,
    crystal,
    _NumberOfTypes
}