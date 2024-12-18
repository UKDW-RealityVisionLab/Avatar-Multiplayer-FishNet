using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class XRPrefabManager : NetworkBehaviour
{
    void Start()
    {
        // Cari objek dengan tag MainCamera
        GameObject XRprefabs = GameObject.Find("KONTOL");
        Camera camera = XRprefabs.GetComponentInChildren<Camera>();


        // Periksa apakah mainCamera ditemukan
        if (camera != null)
        {
            //mainCamera.SetActive(false);

            Destroy(camera);
        }
    }
}
