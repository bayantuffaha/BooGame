using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float lifespan;
    float time;
    public Syrup SYRUP;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().transform.Rotate(0,0,180*speed*Time.deltaTime);
        gameObject.transform.Translate(new Vector2(speed*direction.x*Time.deltaTime,speed*direction.y*Time.deltaTime));
        time+=Time.deltaTime;
        if(time>=lifespan){
            Esplode();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Esplode();
    }

    public void Esplode()
    {
        Syrup syrup = Instantiate(SYRUP,null);
        syrup.transform.position = (gameObject.transform.position);
        Destroy(gameObject);
    }
}
