using UnityEngine;

public class AlternateSpriteMovement : MonoBehaviour
{
    private float speed;  // The speed of the customer (can be set dynamically)
    private Rigidbody2D rb; // Rigidbody2D for movement control

    // Set the speed from the CustomerSpawner or other sources
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {
        // Get the Rigidbody2D component attached to the sprite
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the sprite towards the left across the screen (or right depending on direction)
        // You can also make it move in any direction you'd like
        Vector2 moveDirection = Vector2.left; // Move left by default
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y); // Update the velocity for movement
    }
}
