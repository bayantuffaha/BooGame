using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour
{

    public static NewGameController control;

    public double candyCount;

    public float timeSinceDash;
    public float timeSinceLine;

    public float dashCooldown;
    public float lineCooldown;

    public int lineCost;
    public int dashCost;
    // public GameObject player1;
    // public GameObject player2;

    public Image ZombieImage; // Reference to the Image component
    public AudioSource ZombieSound; // Reference to the AudioSource component

    private void Awake()
    {
        //if a control already, delete
        if (control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);

            //If none, make this the control
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        timeSinceDash = timeSinceDash + Time.deltaTime;
        timeSinceLine = timeSinceLine + Time.deltaTime;
    }

    void Start()
    {
        ZombieImage.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log("starting the game!");
        SceneManager.LoadScene("Build Halloween");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    public bool Line()
    {
        if (candyCount >= lineCost && (timeSinceLine > lineCooldown || timeSinceLine < 0))
        {
            timeSinceLine = -0.04f;
            candyCount -= lineCost;
            return true;
        }
        return false;
    }

    public bool Dash()
    {
        if (candyCount >= dashCost && timeSinceDash > dashCooldown)
        {
            timeSinceDash = 0;
            candyCount -= dashCost;
            return true;
        }
        return false;
    }


    public void ZombieScare()
    {
        // This method shows the jump scare image and plays the sound effect

        // Set the alpha value of the image to one
        ZombieImage.gameObject.SetActive(true);

        // Play the sound effect
        ZombieSound.Play();

        // The delay before the fade in seconds
        float fadeDelay = 0.8f;

        // Invoke the Coroutine that fades out the image after the delay
        Invoke("StartFadeOutImage", fadeDelay);
    }

    // This method starts the Coroutine that fades out the image
    void StartFadeOutImage()
    {
        StartCoroutine(FadeOutImage());
    }

    // This Coroutine fades out the image over a given duration
    IEnumerator FadeOutImage()
    {
        // The duration of the fade in seconds
        float fadeDuration = 1.9f;

        // The initial alpha value of the image
        float startAlpha = 1f;

        // The final alpha value of the image
        float endAlpha = 0f;

        // The elapsed time since the start of the fade
        float elapsedTime = 0f;

        // The current alpha value of the image
        float currentAlpha;

        // Loop until the fade is complete
        while (elapsedTime < fadeDuration)
        {
            // Increase the elapsed time by the time between frames
            elapsedTime += Time.deltaTime;

            // Calculate the current alpha value using linear interpolation
            currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            // Set the alpha value of the image
            ZombieImage.color = new Color(ZombieImage.color.r, ZombieImage.color.g, ZombieImage.color.b, currentAlpha);

            // Wait for the next frame
            yield return null;
        }

        // Set the alpha value of the image to the final value
        ZombieImage.color = new Color(ZombieImage.color.r, ZombieImage.color.g, ZombieImage.color.b, endAlpha);

        // Deactivate the image
        ZombieImage.gameObject.SetActive(false);
    }
}

