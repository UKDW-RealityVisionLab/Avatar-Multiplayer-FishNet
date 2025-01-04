using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Pastikan untuk menambahkan namespace ini jika menggunakan EventSystems

public class FullScreenMapController : MonoBehaviour
{
    public GameObject mapPanel; // Drag your map panel here in the Inspector
    

    private bool isMapOpen = false;

    void Update()
    {
        // Cek jika tombol "M" ditekan
        if (Input.GetKeyDown(KeyCode.M))
        {
        isMapOpen = !isMapOpen;
        mapPanel.SetActive(isMapOpen);
        Debug.Log("Map panel: " + mapPanel); // Tambahkan baris ini untuk debugging
        Debug.Log("Map is " + (isMapOpen ? "open" : "closed"));
        }
    }
}