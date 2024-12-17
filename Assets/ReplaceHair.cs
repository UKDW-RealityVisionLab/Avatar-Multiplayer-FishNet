using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceHair : MonoBehaviour
{
    public GameObject newHairPrefab; // Assign the new hair prefab in the Inspector

    void Start()
    {
        if (newHairPrefab != null)
        {
            // Simpan transformasi lokal (posisi, rotasi, dan skala) dari rambut lama
            Transform oldTransform = transform;
            Vector3 localPosition = oldTransform.localPosition;
            Quaternion localRotation = oldTransform.localRotation;
            Vector3 localScale = oldTransform.localScale;

            // Instansiasi prefab rambut baru
            GameObject newHair = Instantiate(newHairPrefab, oldTransform.parent);
            newHair.transform.localPosition = localPosition;
            newHair.transform.localRotation = localRotation;
            newHair.transform.localScale = localScale;

            // Hapus prefab rambut lama
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Prefab rambut baru belum di-assign!");
        }
    }
}

