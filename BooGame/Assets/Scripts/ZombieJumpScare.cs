using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieJumpScare : MonoBehaviour
{
    public SpriteRenderer ZombieImage; // Reference to the Image component
    public AudioSource ZombieSound; // Reference to the AudioSource component

    void Start()
    {
        ZombieImage.gameObject.SetActive(false);

        ZombieImage.color = new Color(1f, 1f, 1f, 0f); // Set the alpha value of the image to zero
    }

    // This method shows the jump scare image and plays the sound effect
    public void ShowJumpScare()
    {
        // Set the alpha value of the image to one
        ZombieImage.gameObject.SetActive(true);
        ZombieImage.color = new Color(1f, 1f, 1f, 1f);

        // Play the sound effect
        ZombieSound.Play();
    }
}
