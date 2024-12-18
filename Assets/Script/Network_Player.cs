  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Network_Player : NetworkBehaviour
{

    [Header("Components to Disable")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable;
    [SerializeField] private Camera clientCamera;
    [SerializeField] private AudioListener clientAudioListener;
    [SerializeField] private InputActionManager inputActions;

    [ServerRpc(RequireOwnership = false)]
    public void ResetInputActions_ServerRpc()
    {
        // Notify all clients to reset their input actions
        ResetInputActions_ClientRpc();
    }

    [ObserversRpc]
    public void ResetInputActions_ClientRpc()
    {
        Debug.Log("Resetting Input Actions on Client");
        if (inputActions != null)
        {
            inputActions.enabled = false;
            inputActions.enabled = true;  // Enable them again to reinitialize
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            EnableLocalPlayerInput();
            ResetInputActions_ServerRpc(); // Reset input on all clients when this player connects
        }
        else
        {
            DisableOtherPlayersInput();
        }
    }

    public void EnableLocalPlayerInput()
    {
        if (clientCamera != null)
            clientCamera.enabled = true;

        if (clientAudioListener != null)
        clientAudioListener.enabled = true;

        if (scriptsToDisable != null)
        {
            foreach (var script in scriptsToDisable)
            {
                if (script != null)
                {
                    script.enabled = true;
                }
            }
        }

    }

    public void DisableOtherPlayersInput()
    {
        if (clientCamera != null)
            clientCamera.enabled = false;

        if (clientAudioListener != null)
        clientAudioListener.enabled = false;

        if (scriptsToDisable != null)
        {
            foreach (var scriptdisable in scriptsToDisable)
            {
                if (scriptdisable != null)
                {
                    scriptdisable.enabled = false;
                }
            }
        }

    }
}