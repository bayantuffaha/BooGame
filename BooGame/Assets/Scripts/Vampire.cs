using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 10f;
    public Transform player1;
    public Transform player2;
    private Transform isChasing;
    public LayerMask obstacleLayerMask;
    float sticky = 0;

    public LineRenderer lineOfSight;
    public bool isAttacking = false;

    // Variables for patrolling behavior
    public float patrolMinX = 0f;
    public float patrolMaxX = 10f;
    private bool isPatrollingRight = true;

    //Variables to speed up the movement over time
    private float minimumSpeed = 3f;
    private float maximumSpeed = 6f;
    public float timeToAppear = 5f;
    public bool hasAppeared = false;


    void Start()
    {
        Debug.Log("Enemy initialized.");
        if (lineOfSight != null)
        {
            lineOfSight.positionCount = 2;
        }

        gameObject.SetActive(false);
        Invoke("Appear", timeToAppear);

        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        if (hasAppeared) {
            if (player1 != null && player2 != null)
            {
                //increase speed
                speed += .02f * Time.deltaTime;
                speed = Mathf.Clamp(speed, minimumSpeed, maximumSpeed);

                isChasing = CanSeePlayer();
                if (isChasing!=null)
                {
                    ChasePlayer();
                    if ((Vector2.Distance(transform.position, player1.position) <= 1.0f) || (Vector2.Distance(transform.position, player2.position) <= 1.0f)) {
                        GetComponent<Collider2D>().enabled = true;
                    } else {
                        GetComponent<Collider2D>().enabled = false;
                    }
                }
                else
                {
                    Patrolling();
                }

                /*Vector3 direction = player.position - transform.position;

                if (Physics2D.Raycast(transform.position, direction, detectionRange, obstacleLayerMask))
                {
                    //Debug.Log("Obstacle detected!");
                }*/

                DrawLineOfSight();
            }
        }
    }

    void Appear()
    {
    // This function is called after the specified time, making the enemy appear
    hasAppeared = true;

    // Activate the GameObject to make it visible and active
    gameObject.SetActive(true);
    }

    void ChasePlayer()
    {
        if (isChasing!=null)
        {
            //Debug.Log("Chasing player");
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = isChasing.position.x-transform.position.x<0;
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, isChasing.position, (speed - (minimumSpeed * sticky)) * Time.deltaTime);
        }
    }

    void Patrolling()
    {
        // // Move the enemy based on its current direction
        // Vector3 movement = isPatrollingRight ? Vector3.right : Vector3.left;
        // transform.Translate(movement * speed * Time.deltaTime);

        // // Check if the enemy reached the patrol boundaries
        // if (transform.position.x >= patrolMaxX && isPatrollingRight)
        // {
        //     // If moving right and reached the right boundary, flip the direction
        //     isPatrollingRight = false;
        // }
        // else if (transform.position.x <= patrolMinX && !isPatrollingRight)
        // {
        //     // If moving left and reached the left boundary, flip the direction
        //     isPatrollingRight = true;
        // }

        // Use Perlin noise to create smooth, random movement
        float xNoise = Mathf.PerlinNoise(Time.time * 0.5f, 0) * 2f - 1f; // Generate random value in x direction
        float yNoise = Mathf.PerlinNoise(0, Time.time * 0.5f) * 2f - 1f; // Generate random value in y direction
        Vector3 movement = new Vector3(xNoise, yNoise, 0f).normalized; // Normalize for consistent speed
        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = movement.x<0;
        transform.Translate(movement * speed * Time.deltaTime);
    }



    Transform CanSeePlayer()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogWarning("Player not assigned!");
            return null;
        }

        Vector3 direction1 = player1.position - transform.position;
        Vector3 direction2 = player2.position - transform.position;

        // Check if the player is within detection range
        if (direction1.magnitude > detectionRange && direction2.magnitude > detectionRange)
        {
            //Debug.LogWarning("Player out of detection range!");
            return null;
        }

        // Remove obstacle checking here and directly return the player's position
        return (player1.gameObject.GetComponent<PlayerMove_2P>().isAlive) ? player1 : player2;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            Debug.Log("Attacking!");
            gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<PlayerMove_2P>().Die();
            Debug.Log("Audio just played");
            // gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
        }
        if(collision.gameObject.CompareTag("Ooze"))
        {
            sticky = 0;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ooze"))
        {
            sticky = col.gameObject.GetComponent<Syrup>().sticky;
        }
    }

    void DrawLineOfSight()
    {
        if (lineOfSight != null)
        {
            lineOfSight.SetPosition(0, transform.position);
            lineOfSight.SetPosition(1, (isChasing!=null) ? isChasing.position : transform.position);
        }
    }
}