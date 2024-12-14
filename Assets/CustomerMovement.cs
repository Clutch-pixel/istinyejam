using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    
    private float speed; // Speed of the customer
    private bool movingLeft = true; // Determines direction (true = left, false = right)
    private Collider2D customerCollider; // Reference to the customer's collider

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {
        // Get the collider attached to the customer
        customerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Move in the current direction
        Vector3 direction = movingLeft ? Vector3.left : Vector3.right;
        transform.Translate(direction * speed * Time.deltaTime);

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the customer was hit by a drink
        if (other.CompareTag("DrinkThrower"))
        {
            // Change direction
            movingLeft = false;

            // Remove the customer's collider to prevent further collisions
            if (customerCollider != null)
            {
                customerCollider.enabled = false;
            }

            // OPTIONAL: Destroy the drink after it hits the customer
            Destroy(other.gameObject);
        }
    }
    
}