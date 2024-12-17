using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllGameObjectOfParent : MonoBehaviour
{
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
