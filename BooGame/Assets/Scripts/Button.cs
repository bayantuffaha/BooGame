using System;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool on;
    public GameObject up;
    public GameObject down;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        up.SetActive(!on);
        down.SetActive(on);
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Player")){
            on = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        on = false;
    }
}
