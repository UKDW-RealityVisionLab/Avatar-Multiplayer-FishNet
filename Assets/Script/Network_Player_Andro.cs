using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using FishNet.Connection;
using FishNet.Object;

public class Network_Player_Andro : NetworkBehaviour
{

    [Header("Components to Disable")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable;
    [SerializeField] private Camera clientCamera;
    [SerializeField] private AudioListener clientAudioListener;

    [SerializeField] private GameObject Joystick;
    [SerializeField] private GameObject UISystem;



    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            EnableLocalPlayerInput();
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

    #if UNITY_IOS || (UNITY_ANDROID && !UNITY_EDITOR)
        Debug.Log("This code runs on Android!");
        if (Joystick != null)
            Joystick.SetActive(true);

        if (UISystem != null)
            UISystem.SetActive(true);
    #elif UNITY_EDITOR
        Debug.Log("This code runs on Editor!");
        if (Joystick != null)
            Joystick.SetActive(false);

        if (UISystem != null)
            UISystem.SetActive(false);
    #else
        Debug.Log("This code not runs on Server Or Windows!");
        if (Joystick != null)
            Joystick.SetActive(false);

        if (UISystem != null)
            UISystem.SetActive(false);
    #endif

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

        if (Joystick != null)
            Joystick.SetActive(false);

        if (UISystem != null)
            UISystem.SetActive(false);

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