using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Menu pause
    private float cameraSpeed = 10f;
    private bool isCameraMoving = false;

    private ThirdPersonController thirdPersonController;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Only move the camera if not paused
        if (!IsPaused())
        {
            if (isCameraMoving)
            {
                float moveHorizontal = Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime;
                float moveVertical = Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;

                // Move the camera
                transform.Translate(moveHorizontal, 0, moveVertical);
            }
        }
    }

    public void TogglePause() 
    {
        if (Time.timeScale == 0f)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isCameraMoving = false;

        FindThirdPersonController();
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = false;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isCameraMoving = true;

        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = true;
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
    }

    // Check if the game is paused
    private bool IsPaused()
    {
        return Time.timeScale == 0f;
    }

    // Find the spawned ThirdPersonController object
    private void FindThirdPersonController()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Ensure your player prefab is tagged as "Player"
        if (player != null)
        {
            thirdPersonController = player.GetComponent<ThirdPersonController>();
        }
        else
        {
            Debug.LogWarning("Spawned player not found!");
        }
    }
}