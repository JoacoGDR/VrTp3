using UnityEngine;

public class FallingSoldier : MonoBehaviour
{
    public float parachuteFallSpeed = 4f; // The reduced speed when the parachute is activated
    public float fallThreshold = 0f; // The threshold velocity to determine if the character is falling

    [HideInInspector] public bool isFalling = false; // Made it public so it can be accessed by other scripts
    private Rigidbody rb;
    private bool hasLanded = false;
    public GameObject parachute;
    private Animator animator;
    public Transform target;  // The target location for the NPC to run towards
    private UnityEngine.AI.NavMeshAgent navMeshAgent;  // Reference to the NavMeshAgent component


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isFalling && !hasLanded)
        {
            isFalling = true;
            ActivateParachute();
        }else if (isFalling)
        {
            if(rb.velocity.y == 0){
                isFalling = false;
                parachute.SetActive(false);
                animator.SetBool("Falling", false);
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.useGravity = false;
                hasLanded = true;
                RunToTarget();

            }
        }
        else if (hasLanded){
            if(navMeshAgent != null & navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
                animator.SetBool("Running", false);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("Shooting", true);
                navMeshAgent.enabled = false;
                Destroy(navMeshAgent);
            }
        }
    }


    private void ActivateParachute()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, -parachuteFallSpeed, rb.velocity.z);
        parachute.SetActive(true);
        animator.SetBool("Falling", true);
        
    }

     private void RunToTarget()
    {
        // Set the destination of the NavMeshAgent to the target location
        animator.SetBool("Running", true);
        navMeshAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();  // Add the NavMeshAgent component to the NPC
        navMeshAgent.SetDestination(target.position);
    }
}