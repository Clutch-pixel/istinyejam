using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    Border border;
    [Header("Game Managers")]
    public ScoreManager scoreManager;
    public Border borderScript;

    // Add VideoPlayer component and VideoClip for the video
    public VideoPlayer videoPlayer;  // VideoPlayer to play the video
    public VideoClip videoClip;      // The video to play
    public RawImage videoRawImage;   // Reference to the RawImage to display the video (if using UI)

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
            Debug.Log("Enemy collided");
            other.GetComponent<CustomerMovement>().speed *= -1;
        }

        if (other.gameObject.CompareTag("AlternateSprite"))
        {
            Destroy(other.gameObject);  // Destroy the AlternateSprite

            // Play the video when colliding with AlternateSprite
            PlayVideoAtPosition(other.transform.position);

            // Reduce score and health
            scoreManager?.AddScore(-200);
            Debug.Log("Score reduced by 200");
            border.health--;
            borderScript?.UpdateHealthUI();
            Debug.Log("Health reduced by 1 due to AlternateSprite");

            Destroy(gameObject);  // Destroy the projectile
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

    // Method to play the video at the position of the AlternateSprite
    void PlayVideoAtPosition(Vector3 position)
    {
        if (videoPlayer != null && videoClip != null)
        {
            // Set the position of the VideoPlayer (optional: if using UI, you may need to convert to screen space)
            // Here, we set it to the collision position in world space.
            videoPlayer.transform.position = position;

            // Debug the position to make sure it's correct
            Debug.Log("Playing video at position: " + position);

            // Set the video clip to the VideoPlayer
            videoPlayer.clip = videoClip;

            // Play the video
            videoPlayer.Play();

            // Optional: If using UI RawImage, set the texture to the RenderTexture
            if (videoRawImage != null)
            {
                videoRawImage.texture = videoPlayer.targetTexture;
            }

            // Optionally, make the video stop after playing by adding a listener to detect when it finishes
            StartCoroutine(StopVideoAfterPlay());
        }
        else
        {
            Debug.LogError("VideoPlayer or VideoClip is missing!");
        }
    }

    // Coroutine to stop the video after it finishes playing (if you want to hide it after it's done)
    IEnumerator StopVideoAfterPlay()
    {
        yield return new WaitForSeconds((float)videoPlayer.clip.length); // Wait for the duration of the video
        videoPlayer.Stop();
        // Optionally, disable the RawImage or the VideoPlayer component
        if (videoRawImage != null)
        {
            videoRawImage.enabled = false;
        }
    }
}
