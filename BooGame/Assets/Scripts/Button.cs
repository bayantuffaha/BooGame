using System;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool on;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().flipX = on;
        GetComponentInChildren<SpriteRenderer>().flipY = on;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player"){
            on = true;
        }
        on = true;
    }

    void OnTriggerExit2D(Collider2D col){
        on = false;
    }
}
