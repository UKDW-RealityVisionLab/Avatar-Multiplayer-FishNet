using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private GameObject uiButton; // The UI Button to show/hide
    private bool isPlayerNear = false; // Tracks if the player is near the object
    [SerializeField] public KeyCode interactionKey = KeyCode.E;
    private Collider other;

    // Start is called before the first frame update
    void Start()
    {
        if (uiButton != null)
        {
            uiButton.SetActive(false); // Hide the button at the start
        }
        else
        {
            Debug.LogError("UI Button is not assigned in the Inspector!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(interactionKey) && other.CompareTag("Player"))
        {
            ToggleUIButton();
        }
    }

    // Toggles the UI button visibility
    public void ToggleUIButton()
    {
        if (uiButton != null)
        {
            uiButton.SetActive(true);

        }
    }

    // Detect when the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Detect when the player exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // Ensure the button is hidden when the player leaves
            if (uiButton != null)
            {
                uiButton.SetActive(false);
            }
        }
    }
}
