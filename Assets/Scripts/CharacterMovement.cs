using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public Animator animator;
    public ParachuteController parachuteController; // Reference to the ParachuteController script

    public bool isAlive = true;

    private Camera characterCamera;
    private float cameraRotationX = 0f;

    [SerializeField] public AudioClip radioSound;
    [SerializeField] public AudioClip gogogoSound;
    private AudioSource audioSource;

    private void Start()
    {
        characterCamera = GetComponentInChildren<Camera>();
        animator = GetComponent<Animator>();
        parachuteController = GetComponent<ParachuteController>(); // Assign the ParachuteController script
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(startingIndicationsSounds());

        // Lock cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()    
    {
        if (isAlive)
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
        else
        {
            if(Input.GetKey("r"))
            {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            }
        }
    }

    private IEnumerator startingIndicationsSounds()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(radioSound);
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(gogogoSound);
    }

    public void kill()
    {
        this.isAlive = false;
    }
}
