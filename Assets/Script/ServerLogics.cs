using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Transporting;
using FishNet.Object;
using FishNet.Connection;
using System;
using Unity.Mathematics;

public class ServerLogics : NetworkBehaviour
{

    [SerializeField]
    private List<GameObject> playerPrefabs = new List<GameObject>();


    private void OnEnable()
    {
        // InstanceFinder.ClientManager.OnClientConnectionState += OnClientConnectionState;
        InstanceFinder.ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;
    }

    private void OnDisable()
    {
        // InstanceFinder.ClientManager.OnClientConnectionState -= OnClientConnectionState;
        InstanceFinder.ServerManager.OnRemoteConnectionState -= OnRemoteConnectionState;
    }

    // private void OnRemoteConnectionState(NetworkConnection connection, RemoteConnectionStateArgs args)
    // {
    //     if (args.ConnectionState == LocalConnectionState.Stopping)
    //     {
    //         // Call ResetInputActions on all connected players when a client disconnects
    //         ResetAllPlayersInputActions();
    //          Debug.Log("MUdaaaaaaa");
    //     }
    // }

    private void OnRemoteConnectionState(NetworkConnection connection, RemoteConnectionStateArgs args)
    {
        switch (args.ConnectionState)
        {
            case RemoteConnectionState.Stopped:
                // Call ResetInputActions on all connected players when a client disconnects
                ResetAllPlayersInputActions();
                break;
        }
    }


    // Method to reset input actions for all players
    private void ResetAllPlayersInputActions()
    {
        Network_Player[] networkPlayers = FindObjectsOfType<Network_Player>();

        foreach (Network_Player player in networkPlayers)
        {
            // Only the server can invoke the ServerRpc
            player.ResetInputActions_ServerRpc();  // Reset input actions on all clients

        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void LogdeviceModel(string deviceModel)
    {
        Debug.Log($"Device Model: {deviceModel}");
    }

    // Sends a request to the server to spawn a character
    [ServerRpc(RequireOwnership = false)]
    public void SpawnRequestServerRpc(int spawnIndex, NetworkConnection conn, GameObject playerInstances)
    {
        NetworkConnection connectionsave = conn;
        DespawnRequestServerRpc(playerInstances);
        // Ensure spawn index is valid
        if (spawnIndex < 0 || spawnIndex >= playerPrefabs.Count)
        {
            Debug.LogError("Invalid spawn index");
            return;
        }

        // Ensure the spawn point exists
        if (SpawnPoint.instance == null)
        {
            Debug.LogError("Spawn point is not defined");
            return;
        }



        // Instantiate the player prefab at the designated spawn point
        GameObject playerInstance = Instantiate(
            playerPrefabs[spawnIndex],
            SpawnPoint.instance.transform.position,
            Quaternion.identity
        );

        // Spawn the instantiated player on the network
        Spawn(playerInstance, connectionsave);
        // Transfer ownership of the targetObject to the newOwner

    }


    public void DespawnRequestServerRpc(GameObject playerInstances)
    {
        // Transfer ownership of the targetObject to the newOwner
        Despawn(playerInstances.gameObject, DespawnType.Destroy);
    }
}
