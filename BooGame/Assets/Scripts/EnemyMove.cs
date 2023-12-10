using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 10f;
    public Transform player;
    private bool isChasing = false;
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


    void Start()
    {
        Debug.Log("Enemy initialized.");
        if (lineOfSight != null)
        {
            lineOfSight.positionCount = 2;
        }
    }

    void Update()
    {
        if (player != null)
        {
            //increase speed
            speed += .02f * Time.deltaTime;
            speed = Mathf.Clamp(speed, minimumSpeed, maximumSpeed);

            if (CanSeePlayer())
            {
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                Patrolling();
            }

            Vector3 direction = player.position - transform.position;

            if (Physics2D.Raycast(transform.position, direction, detectionRange, obstacleLayerMask))
            {
                //Debug.Log("Obstacle detected!");
            }

            DrawLineOfSight();
        }
    }

    void ChasePlayer()
    {
        if (isChasing)
        {
            //Debug.Log("Chasing player");
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, (speed - (minimumSpeed * sticky)) * Time.deltaTime);
        }
    }

void Patrolling()
{
    // Move the enemy based on its current direction
    Vector3 movement = isPatrollingRight ? Vector3.right : Vector3.left;
    transform.Translate(movement * speed * Time.deltaTime);

    // Check if the enemy reached the patrol boundaries
    if (transform.position.x >= patrolMaxX && isPatrollingRight)
    {
        // If moving right and reached the right boundary, flip the direction
        isPatrollingRight = false;
    }
    else if (transform.position.x <= patrolMinX && !isPatrollingRight)
    {
        // If moving left and reached the left boundary, flip the direction
        isPatrollingRight = true;
    }
}



    bool CanSeePlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned!");
            return false;
        }

        Vector3 direction = player.position - transform.position;

        // Check if the player is within detection range
        if (direction.magnitude > detectionRange)
        {
            //Debug.LogWarning("Player out of detection range!");
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, obstacleLayerMask);

        if (hit.collider != null)
        {
            // Check if the hit object is the player
            if (hit.collider.CompareTag("Player"))
            {
                return true; // Player is visible, so the enemy can chase
            }
            else
            {
                Debug.LogWarning("Obstacle detected between enemy and player!");
                return false; // Obstacle is in the way, so the enemy won't chase
            }
        }
        else
        {
            // No obstacles, and the player is within detection range
            return true;
        }
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
            lineOfSight.SetPosition(1, isChasing ? player.position : transform.position);
        }
    }
}
