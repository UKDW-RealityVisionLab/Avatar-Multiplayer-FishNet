using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPositionRelative : MonoBehaviour
{
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    void Start()
    {
        // Simpan posisi dan rotasi lokal awal
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Pastikan posisi dan rotasi lokal tetap sama
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }
}

