using FishNet.Connection;
using FishNet.Object;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;
using TMPro;
using FishNet;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private List<GameObject> character = new List<GameObject>(); // List for boy and girl characters
    [SerializeField] private GameObject characterSelectorPanel; // Panel to select character
    [SerializeField] private GameObject canvasObject; // Canvas containing character selection
    [SerializeField] private GameObject vrPlayerPrefab; // VR player prefab
    [SerializeField] private string cameraTag;
    private GameObject vrInstance;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
        {
            string deviceModel = SystemInfo.deviceModel;
            bool isVR = deviceModel.Contains("Pico");
            GameObject cameraObject = GameObject.FindWithTag(cameraTag);
            TMP_Text textDebug = cameraObject.GetComponentInChildren<TMP_Text>();

            if (isVR)
            {

                bool isCameraActive = cameraObject.activeSelf;
                textDebug.SetText("device: " + deviceModel + "camera? " + isCameraActive);

                SpawnVRCharacter();
            }
            else
            {
                Debug.Log("Non-VR device detected, showing character selection panel.");
                textDebug.SetText(deviceModel);
                canvasObject.SetActive(true);
            }
        }
        else
        {
            // Hide canvas if the client is not the owner
            canvasObject.SetActive(false);
        }
    }

    // Called when the "Boy" button is clicked
    public void SpawnBoy()
    {
        characterSelectorPanel.SetActive(false);
        SpawnCharacter(0);

    }

    // Called when the "Girl" button is clicked
    public void SpawnGirl()
    {
        characterSelectorPanel.SetActive(false);
        SpawnCharacter(1);
    }

    private void SpawnVRCharacter()
    {
        if (IsOwner && vrPlayerPrefab != null)
        {
            SpawnVRRequest(Owner);
        }
    }

    private void SpawnCharacter(int spawnIndex)
    {
        if (IsOwner)
        {
            SpawnRequest(spawnIndex, Owner);
            GameObject cameraObject = GameObject.FindWithTag(cameraTag);
            if (cameraObject != null)
            {
                cameraObject.SetActive(false);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnRequest(int spawnIndex, NetworkConnection conn)
    {
        // Validasi prefab dari list karakter
        if (spawnIndex < 0 || spawnIndex >= character.Count || character[spawnIndex] == null)
        {
            Debug.LogError("Invalid spawn index or character prefab is null.");
            return;
        }

        GameObject prefabToSpawn = character[spawnIndex];

        // Spawn karakter
        GameObject playerInstance = Instantiate(
            prefabToSpawn,
            SpawnPoint.instance.transform.position,
            quaternion.identity
        );
        InstanceFinder.ServerManager.Spawn(playerInstance, conn);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnVRRequest(NetworkConnection conn)
    {

        // Spawn karakter VR
        vrInstance = Instantiate(
            vrPlayerPrefab,
            SpawnPoint.instance.transform.position,
            quaternion.identity
        );

        InstanceFinder.ServerManager.Spawn(vrInstance, conn);
        Debug.Log($"Spawned VR player for connection {conn.ClientId}.");
    }
}

