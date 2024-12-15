using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float speed;  // Customer's movement speed
    private Vector3 targetPosition;  // Target position where the customer will move to
    private bool isMoving = true;    // Whether the customer is still moving

    // Set the movement speed for the customer
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void Start()
    {
        // Initialize the target position. We set the target position to the left of the screen
        // by changing the x-coordinate. You can adjust the value (-10f) based on your scene's scale.
        targetPosition = new Vector3(-10f, transform.position.y, transform.position.z);  // Target position 10 units to the left
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the customer towards the target position (leftward)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // If the customer reaches the target position, stop moving
            if (transform.position.x <= targetPosition.x)
            {
                isMoving = false;
                // Optionally destroy the customer or handle any other logic when they reach the left side
                Destroy(gameObject); // Destroy the customer when it reaches the end
            }
        }
    }
}
