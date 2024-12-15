using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For UI Image
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    Border border;
    [Header("Game Managers")]
    public ScoreManager scoreManager;
    public Border borderScript;
     

    void Awake()
    {
        border = FindObjectOfType<Border>();
    }
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
            
            scoreManager?.AddScore(-200);
            Debug.Log("Score reduced by 200");
            border.health--;
            borderScript?.UpdateHealthUI();
            Debug.Log("Health reduced by 1 due to AlternateSprite");
            
            Destroy(gameObject);



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
