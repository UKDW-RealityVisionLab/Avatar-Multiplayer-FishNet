using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInter : MonoBehaviour
{
    [SerializeField]private int raylength = 5;
    [SerializeField]private LayerMask layerMaskInteract;
    [SerializeField]private string excludeLayerName = null;

    private ControlDoor raycastedObj;
    [SerializeField]private KeyCode openDoorKey = KeyCode.Mouse0;
    [SerializeField]private Image crosshair;
    private bool isCrosshairActive;
    private bool doOnce;
    private const string interactableTag= "InteractableObject";
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd =  transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer (excludeLayerName) | layerMaskInteract.value;
        if (Physics.Raycast (transform.position, fwd, out hit, raylength, mask)){
            if (hit.collider.CompareTag(interactableTag)){
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<ControlDoor>(); 
                    CrosshairChange(true);
                }
                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(openDoorKey))
                { 
                    raycastedObj.PlayAnimation();
                }
            }
        }
        else
        {
            if (isCrosshairActive){
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if(on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}