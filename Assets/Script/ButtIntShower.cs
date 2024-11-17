using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtIntShower : MonoBehaviour
{
    private bool isPlayerNear = false;
    [SerializeField] private GameObject uiButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ensure the button is hidden when the player leaves
            if (uiButton != null)
            {
                uiButton.SetActive(true);
            }
        }
    }

    // Detect when the player exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Ensure the button is hidden when the player leaves
            if (uiButton != null)
            {
                uiButton.SetActive(false);
            }
        }
    }
}
