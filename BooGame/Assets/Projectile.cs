using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float lifespan;
    float time;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = (direction-(Vector2)gameObject.transform.position);
        if(direction.x>0){
            GetComponentInChildren<SpriteRenderer>().transform.Rotate(0,0,-1*Vector3.Angle(Vector2.up, direction));
        } else {
            GetComponentInChildren<SpriteRenderer>().transform.Rotate(0,0,Vector3.Angle(Vector2.up, direction));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //direction.Normalize();
        gameObject.transform.Translate(direction*speed);
        time+=Time.deltaTime;
        if(time>=lifespan){
            Esplode();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Zombie") || col.gameObject.CompareTag("Ooze")){
            return;
        }
        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMove_2P>().isAlive && !col.gameObject.GetComponent<PlayerMove_2P>().isDash)
        {
            Debug.Log("Attacking!");
            col.gameObject.GetComponent<PlayerMove_2P>().Die();
        }
        Esplode();
    }

    public void Esplode()
    {
        Destroy(gameObject);
    }
}
