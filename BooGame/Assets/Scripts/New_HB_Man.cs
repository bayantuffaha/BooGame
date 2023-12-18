using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class New_HB_Man : MonoBehaviour
{
    public SpriteRenderer vignette2;
    public float time = 100;
    float timeRemaining = 0;
    public AnimationCurve oVT;
    public float minTime = 0.5f;
    public float maxTime = 1.5f;
    public float minDist = 5f;
    public float maxDist = 50f;
    private bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        vignette2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0 && timeRemaining > -0.1)
        {
            if(!played)
            {
                GetComponent<AudioSource>().Play(0);
                played = true;
            }
            vignette2.enabled = true;
        }
        if (timeRemaining <= -.1 && timeRemaining > -0.2)
        {
            vignette2.enabled = false;
        }
        if (timeRemaining <= -0.2 && timeRemaining > -0.35)
        {
            vignette2.enabled = true;
        }
        if (timeRemaining <= -.35)
        {
            vignette2.enabled = false;
            timeRemaining = 1.5f;
            played = false;
        }
        timeRemaining -= Time.deltaTime;
    }

}
