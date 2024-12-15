using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))] // Ensure the object has a Collider2D
public class CustomerSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject customerPrefab;
    public GameObject alternateSpritePrefab;

    [Header("Spawning Parameters")]
    public float minSpawnTime = 0.1f;
    public float maxSpawnTime = 1f;
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    public float[] spawnYPositions = { 4f, 1f, -1f, -4f };
    public float[] spawnXPositions = { 4f, 1f, -1f, -4f };

    [Header("Round Settings")]
    public float maxSpawningTime = 30f;
    public float restartDelay = 4f;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI roundText;

    [Header("Game Managers")]
    public ScoreManager scoreManager;
    public Border borderScript;

    private bool isSpawning = false;
    private float elapsedTime = 0f;
    private int round = 1;
    private float spawnChance = 0.15f;

    private void Start()
    {
        // Start the spawning process if it's not already spawning
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnCustomer());
        }

        // Assign the ScoreManager and BorderScript through Unity's automatic assignment (if missing)
        scoreManager = scoreManager ? scoreManager : FindObjectOfType<ScoreManager>();
        borderScript = borderScript ? borderScript : FindObjectOfType<Border>();

        // Log errors if manager references are not set
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager reference is missing!");
        }

        if (borderScript == null)
        {
            Debug.LogError("BorderScript reference is missing!");
        }

        // Ensure that prefabs are set
        if (customerPrefab == null || alternateSpritePrefab == null)
        {
            Debug.LogError("Prefab references are missing!");
        }
    }

    private void Update()
    {
        // Update the timer display
        if (elapsedTime < maxSpawningTime)
        {
            float timeLeft = maxSpawningTime - elapsedTime;
            timerText.text = "Time Left Until Next Round: " + Mathf.Ceil(timeLeft).ToString() + "s";
            roundText.text = "Round " + round;
        }
        else
        {
            // After time runs out, reset and start a new round
            timerText.text = "Spawning Stopped!";
            elapsedTime = 0f;
            round++;

            // Increase difficulty by 20% for the next round
            AdjustSpawnDifficulty();

            // Restart spawning with a delay
            StartCoroutine(RestartSpawningWithDelay());
        }
    }

    // Coroutine to restart spawning after the specified delay
    private IEnumerator RestartSpawningWithDelay()
    {
        yield return new WaitForSeconds(restartDelay);

        isSpawning = true;
        StartCoroutine(SpawnCustomer());
    }

    // Adjust spawn parameters for the new round to increase difficulty
    private void AdjustSpawnDifficulty()
    {
        minSpawnTime *= 0.8f;
        maxSpawnTime *= 0.8f;
        minSpeed *= 1.2f;
        maxSpeed *= 1.2f;
    }

    // Coroutine to spawn customers periodically with a chance to use an alternate sprite
    private IEnumerator SpawnCustomer()
    {
        while (elapsedTime < maxSpawningTime)
        {
            // Wait for a random time interval before spawning the next customer
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // Randomly pick a Y position for the customer
            float randomY = spawnYPositions[Random.Range(0, spawnYPositions.Length)];

            // Decide whether to spawn a normal or alternate sprite based on the spawn chance
            GameObject Bomb = (Random.value <= spawnChance) ? Instantiate(alternateSpritePrefab) : Instantiate(customerPrefab);

            // Set the spawn position for the customer
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);
            Bomb.transform.position = spawnPosition;


            // Set a random speed for the customer
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            Bomb.GetComponent<CustomerMovement>().SetSpeed(randomSpeed);

            // If alternate sprite is used, adjust its collider properties
            if (Random.value <= spawnChance)
            {
                // Adjust the collider for the alternate sprite (example for BombCollider2D)
                CircleCollider2D collider = Bomb.GetComponent<CircleCollider2D>();
                if (collider != null)
                {
                    collider.radius = 0.5f;
                }
            }

            // Update elapsed time
            elapsedTime += waitTime;
        }

        Debug.Log("Customer Spawning Stopped!");
        isSpawning = false;
    }

    // Handle collisions with DrinkThrower or Border
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DrinkThrower"))
        {
            // Decrease score by 200
            scoreManager?.AddScore(-200);
            Debug.Log("Score reduced by 200");

            // Decrease health by 1
            borderScript?.UpdateHealthUI();
            Debug.Log("Health reduced by 1");

            // Destroy the customer object when it hits the DrinkThrower
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Border"))
        {
            // Just destroy the sprite if it hits the border
            Destroy(gameObject);
            

        }
    }
}