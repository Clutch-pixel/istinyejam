using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to support TextMeshPro

public class BartenderController : MonoBehaviour
{
    public float[] lanePositions; // Define Y positions for each lane in the Inspector
    private int currentLane = 0;  // Keeps track of the current lane index
    public float horizontalShift = 1f; // How much to move on the x-axis
    public float moveSpeed = 5f; // Speed at which the bartender moves horizontally
    public float moveSmoothTime = 0.1f; // Time to smoothly transition to the new lane
    public float moveCooldown = 0.7f; // Delay between movements (in seconds)
    public float minMoveCooldown = 0.1f; // Minimum cooldown value

    private Vector3 targetPosition; // Target position the bartender should move towards
    private Vector3 velocity = Vector3.zero; // For smooth movement
    private bool canMove = true; // Flag to check if movement is allowed

    void Start()
    {
        // Set the initial position based on the first lane's Y value
        targetPosition = new Vector3(transform.position.x, lanePositions[currentLane], transform.position.z);

        // Find the smallest x-value from lanePositions (assuming lanePositions is an array of Y positions)
        float smallestX = Mathf.Min(lanePositions);

        // Set the initial position based on the smallest X value and the first lane's Y value
        targetPosition = new Vector3(smallestX, lanePositions[currentLane], transform.position.z);

        transform.position = targetPosition; // Set the bartender's initial position
    }

    void Update()
    {
        // Only allow movement if the bartender can move (cooldown expired)
        if (canMove)
        {
            // Move Up
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentLane > 0) // Check if the bartender can move up
                {
                    currentLane--;
                    MoveToNewLane(1); // Move forward when going up
                    StartCoroutine(MovementCooldown()); // Start cooldown after movement
                }
            }

            // Move Down
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentLane < lanePositions.Length - 1) // Check if the bartender can move down
                {
                    currentLane++;
                    MoveToNewLane(-1); // Move backward when going down
                    StartCoroutine(MovementCooldown()); // Start cooldown after movement
                }
            }
        }

        // Smoothly move to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, moveSmoothTime);
    }

    void MoveToNewLane(int direction)
    {
        // Calculate new target position
        targetPosition = new Vector3(transform.position.x + horizontalShift * direction, lanePositions[currentLane], transform.position.z);
    }

    // Coroutine to add a cooldown delay after movement
    IEnumerator MovementCooldown()
    {
        canMove = false; // Disable movement
        yield return new WaitForSeconds(moveCooldown); // Wait for the set cooldown time

        // Decrease the moveCooldown by 20%, but ensure it never goes below the minimum value
        moveCooldown = Mathf.Max(moveCooldown * 0.8f, minMoveCooldown);

        canMove = true; // Re-enable movement
    }
}
