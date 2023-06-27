using UnityEngine;

public class ParachuteController : MonoBehaviour
{
    public float fallSpeed = 9.8f; // The speed at which the character falls
    public float parachuteFallSpeed = 4f; // The reduced speed when the parachute is activated
    public float fallThreshold = -1f; // The threshold velocity to determine if the character is falling
    public KeyCode activationKey = KeyCode.Space; // The key to activate the parachute

    [HideInInspector] public bool isFalling = false; // Made it public so it can be accessed by other scripts
    private Rigidbody rb;
    private GameObject parachute;
    private bool hasLanded = false;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        parachute = transform.Find("Parachute").gameObject;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isFalling && rb.velocity.y < fallThreshold && !hasLanded)
        {
            isFalling = true;
        }else if (isFalling && !hasLanded)
        {

            if(rb.velocity.y == 0){
                isFalling = false;
                parachute.SetActive(false);
                animator.SetBool("Falling", false);
                hasLanded = true;


            }

            if (Input.GetKeyDown(activationKey))
            {
                ActivateParachute();
                
            }

            // if (isParachuteActivated)
            // {
            //     rb.velocity = new Vector3(rb.velocity.x, -parachuteFallSpeed, rb.velocity.z);
            // }
            // else
            // {
            //     rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallSpeed, rb.velocity.z);
            // }
        }
    }

    private void onCollisionEnter(Collision other){
        if(other.gameObject.tag == "Ground"){
            isFalling = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.useGravity = true;
            parachute.SetActive(false);
        }

    }

    private void ActivateParachute()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, -parachuteFallSpeed, rb.velocity.z);
        parachute.SetActive(true);
        animator.SetBool("Falling", true);

    }
}
