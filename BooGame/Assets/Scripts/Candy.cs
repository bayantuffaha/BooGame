using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    // The reference to the coroutine
    private Coroutine pickupCoroutine;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();
        yield return new WaitForSeconds(0f);
        // Increase count
        // GameController.control.candyCount++;
        // Destroy this item
        Destroy(gameObject);
    }
}

