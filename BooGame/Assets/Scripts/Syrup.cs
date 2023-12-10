using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syrup : MonoBehaviour
{
    public float lifetime;
    float timeLeft;
    SpriteRenderer s;
    public float sticky;
    
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponentInChildren<SpriteRenderer>();
        timeLeft = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        s.color = new Color(0.25f,0.125f,0f,timeLeft/lifetime);
        timeLeft-=Time.deltaTime;
        sticky = timeLeft/lifetime;
        if(timeLeft<=0){
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        timeLeft-=Time.deltaTime;
    }
}
