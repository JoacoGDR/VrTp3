using UnityEngine;
using System.Collections;
using TMPro;
public class ParachuteController : MonoBehaviour
{
    public float fallSpeed = 9.8f; // The speed at which the character falls
    public float parachuteFallSpeed = 4f; // The reduced speed when the parachute is activated
    public float fallThreshold = 1f; // The threshold velocity to determine if the character is falling
    public KeyCode activationKey = KeyCode.Space; // The key to activate the parachute
    public float startingHeight;

    [HideInInspector] public bool isFalling = false; // Made it public so it can be accessed by other scripts
    private Rigidbody rb;
    private GameObject parachute;
    private bool hasLanded = false;
    private Animator animator;
    private bool wasActivated = false;

    [SerializeField] public AudioClip parachuteOpeningSound;
    private AudioSource audioSource;

     [SerializeField]public CharacterMovement characterObject;

    [SerializeField] public TMP_Text missionFailedText;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        parachute = transform.Find("Parachute").gameObject;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startingHeight = transform.position.y;
    }

    private void Update()
    {
        if (!isFalling && startingHeight > (transform.position.y + 10f) && !hasLanded)
        {
            isFalling = true;
        }else if (isFalling && !hasLanded)
        {

            if(rb.velocity.y == 0){
                if(wasActivated == false)
                {//DIED
                    characterObject.isAlive = false; 
                    missionFailedText.text = "Mission Failed, soldier \n Press R to restart.";
                }
                isFalling = false;
                parachute.SetActive(false);
                animator.SetBool("Falling", false);
                hasLanded = true;


            }

            if (Input.GetKeyDown(activationKey) && wasActivated == false)
            {
                ActivateParachute();
                wasActivated = true; 
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

        StartCoroutine(PlaySoundForDuration(parachuteOpeningSound, 2f));
    }

    private IEnumerator PlaySoundForDuration(AudioClip clip, float duration)
    {
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
    }
}
