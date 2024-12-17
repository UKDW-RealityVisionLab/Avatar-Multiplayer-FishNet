using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Kecepatan rotasi

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Update()
    {
        // Dapatkan input mouse
        float mouseX = Input.GetAxis("Mouse X") * -rotationSpeed * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Tambahkan input mouse ke rotasi
        //xRotation -= mouseY;
        yRotation += mouseX;

        // Batasi rotasi pada sumbu X agar objek tidak terbalik
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Terapkan rotasi pada objek
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
    }
}
