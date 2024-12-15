using System.Collections;
using UnityEngine;
using TMPro;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab; // Drag your Customer Prefab here in Unity
    public float minSpawnTime = 0.2f; // Minimum time between spawns
    public float maxSpawnTime = 1f; // Maximum time between spawns
    public float minSpeed = 3f;       // Minimum speed for customers
    public float maxSpeed = 10f;      // Maximum speed for customers
    public float[] spawnYPositions = { 4f, 1f, -1f, -4f }; // Y positions for each lane
    public float[] spawnXPositions = { 4f, 1f, -1f, -4f }; // X positions for lanes, you can adjust as needed

    public float maxSpawningTime = 10f; // Time after which customer spawning will stop (in seconds)
    public float restartDelay = 4f;     // Delay before restarting the spawning process (in seconds)

    private float elapsedTime = 0f; // Timer to track elapsed time for spawning
    public TextMeshProUGUI timerText; // UI Text to display the remaining time
    public BartenderController bartenderController; // Reference to the BartenderController script

    public float minCooldown = 0.1f; // Minimum cooldown value (you can adjust this value)
    private bool isSpawning = false;  // To prevent starting multiple coroutines

    void Start()
    {
        // Start the spawning process if not already spawning
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnCustomer());
        }

        // Ensure the BartenderController is assigned
        if (bartenderController == null)
        {
            bartenderController = GetComponent<BartenderController>();
        }
    }

    void Update()
    {
        // Update the UI text with the remaining time
        if (elapsedTime < maxSpawningTime)
        {
            float timeLeft = maxSpawningTime - elapsedTime;
            timerText.text = "Time Left: " + Mathf.Ceil(timeLeft).ToString() + "s";
        }
        else
        {
            // When time's up, reset the timer and increase the parameters by 20%
            timerText.text = "Spawning Stopped!";

            // Reset the timer for the next round
            elapsedTime = 0f;

            // Increase the spawn values by 20%
            minSpawnTime *= -1.2f;
            maxSpawnTime *= -1.2f;
            minSpeed *= 1.2f;
            maxSpeed *= 1.2f;

            // Start the delay before restarting the spawning process
            StartCoroutine(RestartSpawningWithDelay());
        }
    }

    // Coroutine to handle the delay before restarting the spawning process
    IEnumerator RestartSpawningWithDelay()
    {
        // Wait for the specified restart delay
        yield return new WaitForSeconds(restartDelay);

        // After the delay, restart the customer spawning process with updated values
        isSpawning = true;
        StartCoroutine(SpawnCustomer());
    }

    IEnumerator SpawnCustomer()
    {
        while (elapsedTime < maxSpawningTime) // Check if the elapsed time is less than maxSpawningTime
        {
            // Wait for a random amount of time
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // Pick a random lane (Y position)
            float randomY = spawnYPositions[Random.Range(0, spawnYPositions.Length)];

            // Spawn the customer at a random lane
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);
            GameObject customer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);

            // Add a Rigidbody2D for movement
            Rigidbody2D rb = customer.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Prevent the drink from falling

            // Set a random speed for the customer
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            customer.GetComponent<CustomerMovement>().SetSpeed(randomSpeed);

            // Increase elapsed time
            elapsedTime += waitTime;
        }

        // After maxSpawningTime, stop spawning and log a message
        Debug.Log("Customer Spawning Stopped!");
        isSpawning = false; // Allow restart
    }
}
