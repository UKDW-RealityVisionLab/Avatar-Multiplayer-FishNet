using UnityEngine;
using UnityEngine.UI; // Pastikan Anda menambahkan ini untuk menggunakan UI

public class DisableObjectOnButtonClick : MonoBehaviour
{
    public Button yourUIButton; // Seret tombol UI Anda ke sini di Inspector
    public GameObject objectToDisable; // Seret objek yang ingin dinonaktifkan ke sini di Inspector
    public GameObject objectToDisable2; // Seret objek yang ingin dinonaktifkan ke sini di Inspector

    // Metode untuk menonaktifkan objek
    public void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
            objectToDisable2.SetActive(false);
        }
    }
}
