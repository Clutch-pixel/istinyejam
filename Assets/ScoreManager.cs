using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI Text element to display the score
    private int score = 0; // Player's score

    void Start()
    {
        // Initialize score display
        UpdateScoreDisplay();
    }

    // Function to add points to the score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay(); // Update the UI Text with the new score
    }

    // This function is called by the Projectile to add 50 points when the projectile is destroyed
    public void IncreaseScoreBy50()
    {
        AddScore(50); // Adds 50 points
    }

    // Function to update the score on screen
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + score.ToString();
        }
    }
}
