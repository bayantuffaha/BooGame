using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform gas;
    public AnimationCurve levels;
    public float max;
    public float min;
    public float time;
    float timeSinceInit = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float size = levels.Evaluate(timeSinceInit/time)*(max-min)+min;
        gas.localScale = new Vector3(size/2f,size/2f,1f);
        if(timeSinceInit>=time){Destroy(gameObject);}
        timeSinceInit+=Time.deltaTime;
    }
}
