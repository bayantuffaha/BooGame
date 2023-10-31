using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_PatrolRandomSpace : MonoBehaviour {

       public float speed = 10f;
       private float waitTime;
       public float startWaitTime = 2f;

       public Transform moveSpot;
       public float minX;
       public float maxX;
       public float minY;
       public float maxY;
    //    public Transform playerSpawnPoint;
       public bool isAttacking = false;


       void Start(){
              waitTime = startWaitTime;
              float randomX = Random.Range(minX, maxX);
              float randomY = Random.Range(minY, maxY);
              moveSpot.position = new Vector2(randomX, randomY);
       }

       void Update(){
        // Debug.Log("updating");
              transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

              if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f){
                     if (waitTime <= 0){
                            float randomX = Random.Range(minX, maxX);
                            float randomY = Random.Range(minY, maxY);
                            moveSpot.position = new Vector2(randomX, randomY);
                            waitTime = startWaitTime;
                     } else {
                            waitTime -= Time.deltaTime;
                     }
              }
       }


       //Injure the Player on contact:
        void OnCollisionEnter2D(Collision2D collision){
            if (collision.gameObject.tag == "Player") {
                isAttacking = true;
                Debug.Log("Attacking!");
                gameObject.GetComponent<AudioSource>().Play();
                // Debug.Log(collision.gameObject.GetComponent<AudioSource>());
                Destroy(collision.gameObject);
                Debug.Log("Audio just played");
                //anim.SetBool("Attack", true);
                //  gameHandler.playerGetHit(damage);
                //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
                //StartCoroutine(HitEnemy());
            }
        }

       public void OnCollisionExit2D(Collision2D collision){
              if (collision.gameObject.tag == "Player") {
                     isAttacking = false;
                     //anim.SetBool("Attack", false);
              }
       }
}