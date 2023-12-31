using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    public Button butt;
    bool current = false;
    bool last = false;
    bool state = false;
    public SpriteRenderer cone;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(butt==null){return;}
        last = current;
        current = butt.on;
        if (last && !current) {
            state = !state;
        }
        cone.enabled = !state;
        GetComponent<BoxCollider2D>().enabled = !state;
    }
}
