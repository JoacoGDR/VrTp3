using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public Animator animator;
    public ParachuteController parachuteController; // Reference to the ParachuteController script


    private Camera characterCamera;
    private float cameraRotationX = 0f;

    private void Start()
    {
        characterCamera = GetComponentInChildren<Camera>();
        animator = GetComponent<Animator>();
        parachuteController = GetComponent<ParachuteController>(); // Assign the ParachuteController script


        // Lock cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()

    
    {
        // Camera Rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float cameraRotationY = mouseX * rotationSpeed;
        cameraRotationX -= mouseY * rotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);

        characterCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, cameraRotationY, 0f);

        if (parachuteController != null && parachuteController.isFalling){
            animator.SetBool("Running", false);
            return;
        }

        // Character Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveVertical) * movementSpeed;
        transform.Translate(movement * Time.deltaTime, Space.World);

        // Set the running parameter in the animator based on character movement
        bool isRunning = moveHorizontal != 0f || moveVertical != 0f;
        animator.SetBool("Running", isRunning);

        
    }
}
