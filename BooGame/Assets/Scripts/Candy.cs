using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    // The reference to the coroutine
    private Coroutine pickupCoroutine;
    private AudioSource audioSource;
    private bool isCollected; // Add this variable

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isCollected = false; // Set it to false by default
    }

    // This function is called when another collider enters this collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Check if the candy is not collected yet
            if (!isCollected)
            {
                // Start the pickup coroutine
                pickupCoroutine = StartCoroutine(Pickup());
                // Set the candy to be collected
                isCollected = true;
            }
        }
    }

    // This function is a coroutine that handles the pickup logic
    IEnumerator Pickup()
    {
        // Increase count
        GameController.control.candyCount++;
        audioSource.Play(0);
        //yield return new WaitForSeconds(0f);
        gameObject.transform.position = new Vector3(150f, 150f, 150f);
        yield return new WaitForSeconds(1.003f);
        // Destroy this item
        Destroy(gameObject);
    }
}