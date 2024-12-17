using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchOldHairTransform : MonoBehaviour
{
    public Transform oldHair; // drag & drop the old hair object here

    void Start()
    {
        // Copy local position, rotation, and scale
        transform.localPosition = oldHair.localPosition;
        transform.localRotation = oldHair.localRotation;
        transform.localScale = oldHair.localScale;
    }
}

