using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destroy : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer is found
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on " + gameObject.name);
        }
    }

    // Trigger event when another collider enters the trigger collider of this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered belongs to a specific object (optional)
        // You can replace "SpecificCollider" with any tag of your choice.
        if (other.CompareTag("Wall"))
        {
            // Make the sprite invisible
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}