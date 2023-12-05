using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    public Button butt;
    bool enab;
    bool last;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!last&&butt.on) {
            enab = !enab;
        }
        GetComponentInChildren<SpriteRenderer>().flipX = enab;
        GetComponentInChildren<SpriteRenderer>().flipY = enab;
        GetComponent<BoxCollider2D>().enabled = !enab;
        last = butt.on;
    }
}
