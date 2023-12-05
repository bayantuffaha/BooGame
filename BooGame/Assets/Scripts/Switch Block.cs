using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    public Button butt;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().flipX = butt.on;
        GetComponentInChildren<SpriteRenderer>().flipY = butt.on;
        GetComponent<BoxCollider2D>().enabled = !butt.on;
    }
}
