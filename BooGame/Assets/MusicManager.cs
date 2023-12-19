using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip baseLayerClip;
    public AudioClip drumLayerClip;

    private AudioSource baseLayerAudioSource;
    private AudioSource drumLayerAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Create AudioSources and set their clips
        baseLayerAudioSource = gameObject.AddComponent<AudioSource>();
        baseLayerAudioSource.clip = baseLayerClip;
        baseLayerAudioSource.loop = true;
        baseLayerAudioSource.Play();

        drumLayerAudioSource = gameObject.AddComponent<AudioSource>();
        drumLayerAudioSource.clip = drumLayerClip;
        drumLayerAudioSource.loop = true;
        drumLayerAudioSource.volume = 0f; // Start with drum layer muted
        drumLayerAudioSource.Play();
    }

    // Call this method when a chase is happening
    public void StartChaseMusic()
    {
        // Increase the volume of the drum layer
        StartCoroutine(FadeInDrumLayer());
    }

    // Call this method when the chase ends
    public void StopChaseMusic()
    {
        // Decrease the volume of the drum layer
        StartCoroutine(FadeOutDrumLayer());
    }

    private System.Collections.IEnumerator FadeInDrumLayer()
    {
        float targetVolume = 2.0f;
        float fadeDuration = 2.0f; // Adjust as needed

        while (drumLayerAudioSource.volume < targetVolume)
        {
            drumLayerAudioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    private System.Collections.IEnumerator FadeOutDrumLayer()
    {
        float targetVolume = 0.0f;
        float fadeDuration = 2.0f; // Adjust as needed

        while (drumLayerAudioSource.volume > targetVolume)
        {
            drumLayerAudioSource.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}