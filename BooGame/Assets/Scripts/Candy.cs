using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shid : MonoBehaviour
{
    // The reference to the coroutine
    private Coroutine pickupCoroutine;

    void Awake()
    {
    }

    // This function is called when another collider enters this collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Start the pickup coroutine
            pickupCoroutine = StartCoroutine(Pickup());
        }
    }

    // This function is called when another collider exits this collider
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the other collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Stop the pickup coroutine if it is running
            if (pickupCoroutine != null)
            {
                StopCoroutine(pickupCoroutine);
                pickupCoroutine = null;
            }
        }
    }

    // This function is a coroutine that handles the pickup logic
    IEnumerator Pickup()
    {
        yield return new WaitForSeconds(0f);
        // Increase count
        gameControl.control.candy++;
        // Destroy this item
        Destroy(gameObject);
    }
}

