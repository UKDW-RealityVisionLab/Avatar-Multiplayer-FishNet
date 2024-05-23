using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class CanvasSetActive : NetworkBehaviour
{
    [SerializeField] private GameObject characterSelectorPanel;
    [SerializeField] private GameObject canvasObject;

    public void ActivateCanvas()
    {
        canvasObject.SetActive(true);
        characterSelectorPanel.SetActive(true);
    }
}
