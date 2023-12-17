using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioSource audioSource;

    public float minPitch = 0.25f;
    public float maxPitch = 1f;
    public float minVolume = 0.25f;
    public float maxVolume = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void playClip(AudioClip clip)
    {
        randomizeSound();
        audioSource.PlayOneShot(clip);
    }

    private void randomizeSound()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.volume = Random.Range(minVolume, maxVolume);
    }
}
