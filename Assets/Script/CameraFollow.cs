using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Karakter yang akan diikuti kamera
    public Vector3 offset; // Posisi kamera relatif terhadap karakter
    public float smoothSpeed = 0.125f; // Kecepatan pergerakan kamera

    private void LateUpdate()
    {
        if (target != null)
        {
            // Hitung posisi kamera yang mengikuti target dengan rotasi
            Vector3 desiredPosition = target.position + target.TransformDirection(offset);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Kamera selalu menghadap ke karakter
            transform.LookAt(target);
        }
    }
}
