using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlInteractionn : MonoBehaviour
{
    [SerializeField] public KeyCode interactionKey = KeyCode.E; // Key to interact
    [SerializeField] public GameObject showUI;
    [SerializeField]private LayerMask layerMaskInteract;
    [SerializeField]private string excludeLayerName = null;
    [SerializeField]private int raylength = 5;

    private bool isCrosshairActive;
    private bool doOnce;
    private ButtonInteraction raycastedObj;
    [SerializeField]private Image crosshair;
    private const string interactableTag= "InteractableObject";
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd =  transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer (excludeLayerName) | layerMaskInteract.value;
        if (Physics.Raycast (transform.position, fwd, out hit, raylength, mask)){
            if (hit.collider.CompareTag(interactableTag)){
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<ButtonInteraction>(); 
                    CrosshairChange(true);
                }
                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(interactionKey))
                { 
                    raycastedObj.ToggleUIButton();
                }
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
