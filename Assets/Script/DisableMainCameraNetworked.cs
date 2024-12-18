using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class DisableXRMainCameraNetworked : NetworkBehaviour
{
    [SerializeField] private GameObject xr_origin;
    public override void OnStartClient()
    {
        base.OnStartClient();

        // Pastikan ini adalah klien pemilik
        if (IsOwner)
        {
            DisableMainCameraForXR();
        }
    }

    private void DisableMainCameraForXR()
    {
        // Cari semua GameObject dengan nama "XR Origin"
        GameObject[] xrOrigins = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject xrOrigin in xrOrigins)
        {
            if (xrOrigin.name == "XR Origin" || xrOrigin.name.Contains("XR Origin"))
            {
                // Cari Camera Offset di dalam XR Origin
                Transform cameraOffset = xrOrigin.transform.Find("Camera Offset");

                if (cameraOffset != null)
                {
                    // Cari Main Camera di dalam Camera Offset
                    Transform mainCamera = cameraOffset.Find("Main Camera");

                    if (mainCamera != null && mainCamera.CompareTag("MainCameraVR"))
                    {
                        // Nonaktifkan Main Camera hanya untuk pemain lokal
                        mainCamera.gameObject.SetActive(false);
                        Debug.Log("Main Camera VR pada XR Origin telah dinonaktifkan untuk klien ini.");
                    }
                    else
                    {
                        Debug.LogWarning("Main Camera dengan tag 'MainCameraVR' tidak ditemukan di bawah Camera Offset.");
                    }
                }
                else
                {
                    Debug.LogWarning("Camera Offset tidak ditemukan di XR Origin.");
                }
            }
        }
    }

}
