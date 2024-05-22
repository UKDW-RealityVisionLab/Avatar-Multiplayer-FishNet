using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetActive : MonoBehaviour
{
    [SerializeField] private GameObject characterSelectorPanel;
    [SerializeField] private GameObject canvasObject;

    public void ActivateCanvas()
    {
        canvasObject.SetActive(true);
        characterSelectorPanel.SetActive(true);
    }
}
