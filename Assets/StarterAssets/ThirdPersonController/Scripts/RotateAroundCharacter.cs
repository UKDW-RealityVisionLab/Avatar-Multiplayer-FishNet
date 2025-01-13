using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCharacter : MonoBehaviour
{
     public Transform character; // Referensi ke karakter
    public float rotationSpeed = 100.0f; // Kecepatan rotasi
    public float distance = 5.0f; // Jarak kamera dari karakter
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Offset untuk posisi kamera relatif terhadap karakter

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        if (character == null)
        {
            Debug.LogError("Character Transform is not assigned!");
            enabled = false;
            return;
        }

        // Inisialisasi rotasi awal berdasarkan posisi kamera
        Vector3 direction = transform.position - (character.position + offset);
        Quaternion initialRotation = Quaternion.LookRotation(direction);
        //xRotation = initialRotation.eulerAngles.x;
        yRotation = initialRotation.eulerAngles.y;
    }

    void Update()
    {
        // Dapatkan input mouse
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Tambahkan input mouse ke rotasi
        //xRotation -= mouseY;
        yRotation += mouseX;

        // Batasi rotasi pada sumbu X agar kamera tidak terlalu jauh ke atas/bawah
        //xRotation = Mathf.Clamp(xRotation, -30f, 60f);

        // Hitung posisi kamera baru
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 newPosition = rotation * new Vector3(0, 0, -distance) + (character.position + offset);

        // Terapkan posisi dan rotasi kamera
        transform.position = newPosition;
        transform.LookAt(character.position + offset);
    }
}
