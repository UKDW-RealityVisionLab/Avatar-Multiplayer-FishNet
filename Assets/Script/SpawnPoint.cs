using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class SpawnPoint : NetworkBehaviour
{
    public static SpawnPoint instance;
    private void Awake()
    {
        instance = this;
    }
}
