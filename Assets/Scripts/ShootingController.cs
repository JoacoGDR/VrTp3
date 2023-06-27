using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab of the bullet
    // public Animator animator;  // Reference to the Animator component

    public float shootForce = 20f;  // Force to apply to the bullet when shooting
    public string shootTrigger = "Shoot";  // Name of the trigger parameter in the Animator
    public string stopTrigger = "Stop shooting";
    public KeyCode shootKey = KeyCode.Mouse0;  // Key to press to shoot
    public GameObject bulletSpawnPoint;  // Reference to the bullet spawn point

    private void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();  // Call the Shoot method when the space key is pressed
        }
    }

    private void Shoot()
    {
        // Play the shoot animation
        // animator.SetTrigger(shootTrigger);

        // Spawn and shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // if (bulletRigidbody != null)
        // {
        //     bulletRigidbody.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        // }

        // animator.SetTrigger(stopTrigger);
    }
}
