using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Speed of the bullet

    private void Start()
    {
        Destroy(gameObject, 2f);  // Destroy the bullet after 2 seconds if it doesn't collide with anything
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with any object
        Destroy(gameObject);  // Destroy the bullet
    }
}

