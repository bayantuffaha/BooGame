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

    void OnColliderStay2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            on = true;
        }
    }

    void OnColliderExit2D(Collision2D col){
        on = false;
    }
}
