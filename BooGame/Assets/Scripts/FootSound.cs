using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FootSound : MonoBehaviour
{

    public AudioClip soundEffect;

    private float hori, vert;
    private float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        hori = gameObject.transform.position.x;
        vert = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (hori - gameObject.transform.position.x) * (hori - gameObject.transform.position.x) + (vert - gameObject.transform.position.y) * (vert - gameObject.transform.position.y);

        if (distance > 10)
        {
            AudioManager.instance.playClip(soundEffect);
            hori = gameObject.transform.position.x;
            vert = gameObject.transform.position.y;
        }
    }
}
