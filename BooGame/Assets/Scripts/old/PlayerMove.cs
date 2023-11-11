// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMove : MonoBehaviour
// {
//     public bool isFacingRight = true;
//     public bool isFacingDown = true;
//     public bool isDash = false;
//     public Sprite[] frontIdle;
//     public Sprite[] backIdle;
//     public Sprite[] frontRun;
//     public Sprite[] backRun;
//     public int uPF;
//     int animFrame = 0;
//     SpriteRenderer s;
//     public float speed;
//     public AnimationCurve acc;
//     public float accTime;
//     public int timeSinceIdle = 0;
//     public int timeSinceMove = 0;
//     public int dashCooldown;
//     int timeSinceDash = 0;
//     public int dashDuration;
//     public float dashSpeed;
//     public AnimationCurve dashCurve;
//     Vector2 dashDirection;

    
//     // Start is called before the first frame update
//     void Start()
//     {
//         s = GetComponent<SpriteRenderer>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown("space") && timeSinceDash - dashDuration > dashCooldown) {
//             isDash = true;
//             timeSinceDash = 0;
//             dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//         }
//         timeSinceDash++;
//         if (isDash) {
//             timeSinceDash--;
//             gameObject.transform.Translate(new Vector3(dashSpeed*dashDirection.x*Time.deltaTime*dashCurve.Evaluate(timeSinceDash/dashDuration), dashSpeed*dashDirection.y*Time.deltaTime*dashCurve.Evaluate(timeSinceDash/dashDuration)));
//             if (timeSinceDash/dashDuration == 1) {
//                 isDash = false;
//             }
//             timeSinceDash++;
//             return;
//         }
//         isFacingRight = Input.GetAxis("Horizontal")>0 || (!(Input.GetAxis("Horizontal")<0) && isFacingRight);
//         isFacingDown = Input.GetAxis("Vertical")<0 || (!(Input.GetAxis("Vertical")>0) && isFacingDown);
//         s.flipX = !((isFacingRight && isFacingDown) || (!isFacingRight && !isFacingDown));
//         if (Time.frameCount % 10 == 0) {
//             if (Input.GetAxis("Vertical")==0 && Input.GetAxis("Horizontal")==0) {
//                 if (isFacingDown) {
//                     s.sprite = frontIdle[animFrame];
//                 } else {
//                     s.sprite = backIdle[animFrame];
//                 }
//                 timeSinceMove++;
//                 timeSinceIdle = 0;
//             } else {
//                 if (timeSinceIdle % uPF > uPF/2) {
//                     if (isFacingDown) {
//                         s.sprite = frontRun[animFrame];
//                     } else {
//                         s.sprite = backRun[animFrame];
//                     }
//                 } else {
//                     if (isFacingDown) {
//                         s.sprite = frontIdle[animFrame];
//                     } else {
//                         s.sprite = backIdle[animFrame];
//                     }
//                 }
//                 timeSinceIdle++;
//                 timeSinceMove = 0;
//             }
//             animFrame = (animFrame+1)%4;
//         }
//         if (timeSinceIdle < accTime && timeSinceIdle > 0) {
//             gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime*acc.Evaluate(timeSinceIdle/accTime), Input.GetAxis("Vertical")*speed*Time.deltaTime*acc.Evaluate(timeSinceIdle/accTime)));
//         } else {
//             gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime, Input.GetAxis("Vertical")*speed*Time.deltaTime));
//         }
//     }

// }
