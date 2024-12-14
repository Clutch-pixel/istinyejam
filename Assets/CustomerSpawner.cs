using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Ensure this namespace is included to use UI Text
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

    private float elapsedTime = 0f; // Timer to track elapsed time for spawning
    public TextMeshProUGUI timerText; // UI Text to display the remaining time

    void Start()
    {
        StartCoroutine(SpawnCustomer());
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
            timerText.text = "Spawning Stopped!";
        }
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
    }
}