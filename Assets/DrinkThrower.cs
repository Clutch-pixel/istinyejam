using System.Collections;
using UnityEngine;

public class DrinkThrower : MonoBehaviour
{
    public GameObject drinkPrefab; // Assign the drink prefab in the Inspector
    public float throwSpeed = 10f; // Speed of the drink
    public float throwCooldown = 0.7f; // Initial cooldown time between throws
    public float cooldownDecreasePercentage = 0.8f; // Decrease the cooldown by 20% each cycle
    public float minThrowCooldown = 0.1f; // Minimum cooldown time to prevent it from becoming too fast

    private float nextThrowTime = 0f; // Time when the next throw is allowed
    private float elapsedTime = 0f;   // Timer to track elapsed time for cooldown adjustment
    public float maxCycleTime = 60f;  // Time after which the cooldown should decrease (e.g., every 60 seconds)

    void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if enough time has passed since the last throw and if the cooldown allows for another throw
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextThrowTime)
        {
            ThrowDrink();
        }

        // If the timer has reached the maxCycleTime, decrease the throwCooldown and reset the timer
        if (elapsedTime >= maxCycleTime)
        {
            // Decrease the throwCooldown by the specified percentage, but do not go below the minimum cooldown
            throwCooldown = Mathf.Max(throwCooldown * cooldownDecreasePercentage, minThrowCooldown);

            // Increase the throwSpeed by 20%
            throwSpeed *= 1.5f;

            // Debug message for the new throw speed and cooldown time
            Debug.Log("New Throw Speed: " + throwSpeed + " | New Throw Cooldown: " + throwCooldown);

            // Reset the timer for the next cycle
            elapsedTime = 0f;
        }
    }

    void ThrowDrink()
    {
        // Spawn a new drink at the bartender's position
        GameObject drink = Instantiate(drinkPrefab, transform.position, Quaternion.identity);

        // Add a Rigidbody2D for movement
        Rigidbody2D rb = drink.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Prevent the drink from falling
        rb.velocity = new Vector2(throwSpeed, 0); // Move the drink to the right

        // Set the next throw time based on the current throwCooldown
        nextThrowTime = Time.time + throwCooldown;
    }
}
