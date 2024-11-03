using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Transporting;
using System;


public enum ConnectionType
{
    Host,
    Client
}
public class ConnectionStarter : MonoBehaviour
{

    public ConnectionType connectionType;

#if UNITY_EDITOR

    private void OnDisable()
    {
        InstanceFinder.ClientManager.OnClientConnectionState -= OnClientConnectionState;
    }

    private void OnEnable()
    {
        InstanceFinder.ClientManager.OnClientConnectionState += OnClientConnectionState;
    }

    private void OnClientConnectionState(ClientConnectionStateArgs args)
    {
        if (args.ConnectionState == LocalConnectionState.Stopping)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

#endif
    private void Start()
    {
        #if UNITY_SERVER && !UNITY_EDITOR
        // Dedicated server will only start the server side
        Debug.Log("Starting as DEDICATED_SERVER.");
        InstanceFinder.ServerManager.StartConnection();
        
        #elif UNITY_EDITOR
        if (connectionType == ConnectionType.Host)
        {
            Debug.Log("Starting as HOST in UNITY_EDITOR.");
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        }
        else
        {
            Debug.Log("Starting as CLIENT in UNITY_EDITOR.");
            InstanceFinder.ClientManager.StartConnection();
        }

        #elif UNITY_STANDALONE_WIN
        // Standalone Windows will only start the client side
        Debug.Log("Starting as CLIENT in UNITY_STANDALONE_WIN.");
        InstanceFinder.ClientManager.StartConnection();

        #elif UNITY_ANDROID && !UNITY_EDITOR
        // Android build should only start the client
        Debug.Log("Starting as CLIENT in UNITY_ANDROID.");
        InstanceFinder.ClientManager.StartConnection();
        #endif
}
}
