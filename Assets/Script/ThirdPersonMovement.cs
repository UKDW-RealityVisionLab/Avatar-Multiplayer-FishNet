using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller; // Drag your CharacterController here
    public Transform cameraTransform; // Drag your Camera object here
    public Animator animator; // Drag your Animator component here

    public float speed = 6f;
    public float mouseSensitivity = 100f;
    public Transform cameraPivot; // Pivot for the camera to rotate around the character

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the screen
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
    }

    void HandleMovement()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Update Animator parameters
        bool isMoving = direction.magnitude >= 0.1f;
        animator.SetBool("isMoving", isMoving);

        // if (isMoving)
        // {
            // Get the forward direction relative to the camera
            Vector3 moveDirection = Quaternion.Euler(0f, cameraPivot.eulerAngles.y, 0f) * direction;

            // Move the character
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        // }
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust camera's vertical angle
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f); // Limit vertical rotation of the camera

        // Rotate cameraPivot horizontally
        yRotation += mouseX;

        cameraPivot.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
