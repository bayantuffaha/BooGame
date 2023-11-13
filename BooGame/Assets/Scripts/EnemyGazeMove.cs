// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class EnemyGazeMove : MonoBehaviour {

//    public float rotationSpeed = 30;
//    public float distance = 50;
//    public LineRenderer lineOfSight;
//    public Gradient redColor;
//    public Gradient greenColor;
//    public GameObject hitEffectAnim;

//    public int EnemyLives = 30;
// //    private Renderer rend;
//    // private GameController gameControllerObj;

//    void Start() {
//       Physics2D.queriesStartInColliders = false;

//     //   rend = GetComponent<Renderer();
//       // GameObject gameControllerLocation = GameObject.FindWithTag("GameController");
//       // if (gameControllerLocation != null) {
//       //    gameControllerObj = gameControllerLocation.GetComponent<GameController>();
//       // }
//    }

//    void FixedUpdate () {
//       transform.Rotate (Vector3.forward * rotationSpeed * Time.deltaTime);

//       // Raycast needs location, Direction, and length
//       RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.right, distance);
//       if (hitInfo.collider != null) {
//          Debug.DrawLine(transform.position, hitInfo.point, Color.red);
//          lineOfSight.SetPosition(1, hitInfo.point); // index 1 is the end-point of the line
//          lineOfSight.colorGradient = redColor;

//          if ((hitInfo.collider.CompareTag("Player")) || (hitInfo.collider.CompareTag("enemy"))) {
//             GameObject animEffect = Instantiate(hitEffectAnim, hitInfo.point, Quaternion.identity);
//             Destroy(animEffect, 0.5f);
//             Destroy(hitInfo.collider.gameObject);
//          }
//       } else {
//          Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
//          lineOfSight.SetPosition(1, transform.position + transform.right * distance);
//          lineOfSight.colorGradient = greenColor;
//       }
//       lineOfSight.SetPosition(0, transform.position); // index 0 is the start-point of the line
//    }

//    void OnCollisionEnter2D(Collision2D collision) {
//       if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player") {
//          // Comment out the following lines since you're not managing scoring
//          // EnemyLives -= 1;
//          // StopCoroutine("HitEnemy");
//          // StartCoroutine("HitEnemy");
//          Debug.Log("Attack player!");
//       }
//    }

//    // Comment out the HitEnemy coroutine
//    // IEnumerator HitEnemy(){
//    //    // color values are R, G, B, and alpha, each divided by 100
//    //    rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
//    //    if (EnemyLives < 1){
//    //       // Comment out the score addition, as you're not using a GameController
//    //       // gameControllerObj.AddScore (10);
//    //       Destroy(gameObject);
//    //    }
//    //    else {
//    //       yield return new WaitForSeconds(0.5f);
//    //       rend.material.color = Color.white;
//    //    }
//    // }
// }
