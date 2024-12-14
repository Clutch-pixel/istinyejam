using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkThrower : MonoBehaviour
{
    public GameObject drinkPrefab; // Assign the drink prefab in the Inspector
    public float throwSpeed = 10f; // Speed of the drink
    public float throwCooldown = 0.7f; // Time between throws

    private float lastThrowTime = 0f; // Tracks the time of the last throw

    void Update()
    {
        // Check if enough time has passed since the last throw
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastThrowTime + throwCooldown)
        {
            ThrowDrink();
        }
    }

    void ThrowDrink()
    {
        // Update the last throw time
        lastThrowTime = Time.time;

        // Spawn a new drink at the bartender's position
        GameObject drink = Instantiate(drinkPrefab, transform.position, Quaternion.identity);

        // Add a Rigidbody2D for movement
        Rigidbody2D rb = drink.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Prevent the drink from falling
        rb.velocity = new Vector2(throwSpeed, 0); // Move the drink to the right
    }
}