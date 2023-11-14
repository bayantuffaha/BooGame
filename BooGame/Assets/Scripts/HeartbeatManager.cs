using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatManager : MonoBehaviour
{
    public SpriteRenderer vignette2;
    public float time = 100;
    float timeRemaining = 0;
    public AnimationCurve oVT;
    public float minTime = 10;
    public float maxTime = 400;
    public float minDist = 1;
    public float maxDist = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vignette2.color = new Color(1f,1f,1f,oVT.Evaluate(timeRemaining/time));
        if (timeRemaining <= 0) {
            Beat();
        }
        timeRemaining-=Time.deltaTime;
    }

    void Beat() {
        GetComponent<AudioSource>().Play(0);
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        distance = 
        time = Math.Min(Math.Max(distance * (maxTime-minTime) / (maxDist-minDist), minTime), maxTime);
        timeRemaining = time;
    }
}
