using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For UI Image
using UnityEngine.SceneManagement;  // For scene management

public class Border : MonoBehaviour
{
    // Health system
    public int health = 3;

    // Array to hold references to the UI Image elements representing the health
    public Image[] healthImages;

    // The scene name for the game end screen
    public string gameEndSceneName = "GameEnd"; // Change this to the name of your Game End scene

 
    void OnTriggerExit2D(Collider2D other)
    {
        // Debug message to check which object is exiting
        Debug.Log("Object exited trigger: " + other.name);

        // Check if the object exiting the trigger is tagged as "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Destroy(other.gameObject);

            // Decrease the health
            health--;

            // Update the health UI to reflect the change
            UpdateHealthUI();
        }


      
    }

    // Function to update the health UI
    public void UpdateHealthUI()
    {
        // Ensure health doesn't go below 0 or above the number of health images
        if (health < 0) health = 0;

        // Deactivate the health images based on the current health
        for (int i = 0; i < healthImages.Length; i++)
        {
            // Set active only if health is more than the current index
            healthImages[i].gameObject.SetActive(i < health);
        }

        // If health reaches 0, load the Game End scene
        if (health <= 0)
        {
            LoadGameEndScene();
        }
    }

    // Function to load the game end scene
    void LoadGameEndScene()
    {
        // Log message to confirm game over logic
        Debug.Log("Game Over! Loading Game End Scene...");

        // Load the specified scene (Game End screen)
        SceneManager.LoadScene(gameEndSceneName);
    }

    // Function to spawn a sprite with a 15% chance
    
    }

