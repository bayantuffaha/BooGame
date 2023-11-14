using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatManager : MonoBehaviour
{
    public SpriteRenderer vignette2;
    public float time = 100;
    float timeRemaining = 0;
    public AnimationCurve oVT;
    public float minTime = 0.5f;
    public float maxTime = 1.5f;
    public float minDist = 5f;
    public float maxDist = 50f;

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
        Debug.Log(distance);
        time = Math.Min(Math.Max(distance * (maxTime-minTime) / (maxDist-minDist), minTime), maxTime);
        timeRemaining = time;
        Debug.Log(time);
    }
}
