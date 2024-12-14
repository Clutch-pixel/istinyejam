using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For UI Image
using UnityEngine.SceneManagement;  // For scene management

public class Border : MonoBehaviour
{
    // Health system
    int Health = 3;

    // Array to hold references to the UI Image elements representing the health
    public Image[] healthImages;

    // The scene name for the game end screen
    public string gameEndSceneName = "GameEnd"; // Change this to the name of your Game End scene

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("check");

        // Check if the object exiting the trigger is tagged as "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy object
            Destroy(other.gameObject);

            // Decrease the health
            Health = Health - 1;

            // Update the health UI
            UpdateHealthUI();
        }
    }

    // Function to update the health UI
    void UpdateHealthUI()
    {
        // Make sure Health doesn't go below 0
        if (Health >= 0 && Health < healthImages.Length)
        {
            // Deactivate the last health image (remove 1 image each time health decreases)
            healthImages[Health].gameObject.SetActive(false);
        }

        // If health reaches 0, load the Game End screen
        if (Health <= 0)
        {
            LoadGameEndScene();
        }
    }

    // Function to load the game end scene
    void LoadGameEndScene()
    {
        // Load the specified scene (Game End screen)
        SceneManager.LoadScene(gameEndSceneName);
    }

    void Update()
    {
        Debug.Log(Health);
    }
}