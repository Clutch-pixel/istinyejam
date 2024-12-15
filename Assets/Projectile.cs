using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Reference to the ScoreManager
    private ScoreManager scoreManager;

    void Start()
    {
        // Get the ScoreManager from the scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // When the projectile collides with a target, destroy the target and itself
    void OnTriggerEnter2D(Collider2D other)

    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);  // Destroy the projectile
            Debug.Log("Enmy gördü");
            other.GetComponent<CustomerMovement>().speed *= -1;


        }
        if (other.gameObject.CompareTag("AlternateSprite"))
        {
            Destroy(other.gameObject);  // Destroy the projectile
        }
    }
    

    // When the projectile is destroyed, update the score
    void OnDestroy()
    {
        if (scoreManager != null)
        {
            scoreManager.IncreaseScoreBy50(); // Add 50 points when the projectile is destroyed
        }
    }
}
