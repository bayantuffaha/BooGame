using UnityEngine;

public class WerewolfMove : MonoBehaviour
{
    float speed = 0f;
    public float detectionRange = 10f;
    public Transform player1;
    public Transform player2;
    private Transform isChasing;
    //LayerMask obstacleLayerMask;
    float sticky = 0;
    public SpriteRenderer walk;
    public SpriteRenderer run;

    public LineRenderer lineOfSight;
    public bool isAttacking = false;

    // Variables for patrolling behavior
    public float patrolMinX = 0f;
    public float patrolMaxX = 10f;
    private bool isPatrollingRight = true;

    //Variables to speed up the movement over time
    public float minimumSpeed = 3f;
    public float maximumSpeed = 6f;

    public float dashDuration;
    public float dashSpeedInc;
    public AnimationCurve dashCurve;
    Vector2 dashDirection;
    float timeSinceDash;


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
        timeSinceDash+=Time.deltaTime;
        if(isChasing!=null || timeSinceDash<=dashDuration){
            run.enabled = true;
            walk.enabled = false;
        } else {
            walk.enabled = true;
            run.enabled = false;
        }
        if (player1 != null && player2 != null && timeSinceDash>dashDuration)
        {
            //increase speed
            speed += .02f * Time.deltaTime;
            speed = Mathf.Clamp(speed, minimumSpeed, maximumSpeed);

            isChasing = CanSeePlayer();
            if (isChasing!=null)
            {
                timeSinceDash = 0;
                dashDirection = 100*(isChasing.position-transform.position)+transform.position;
                //dashDirection.Normalize();
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
        } else {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (isChasing!=null)
        {
            run.flipX = dashDirection.x-transform.position.x<0;
            transform.position = Vector2.MoveTowards(transform.position, dashDirection, (speed*dashCurve.Evaluate(timeSinceDash/dashDuration)*dashSpeedInc/(1 - sticky)) * Time.deltaTime);
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
    walk.flipX = xNoise<0;
    float yNoise = Mathf.PerlinNoise(0, Time.time * 0.5f) * 2f - 1f; // Generate random value in y direction
    Vector3 movement = new Vector3(xNoise, yNoise, 0f).normalized; // Normalize for consistent speed

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

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + 2*direction1.normalized, direction1, detectionRange/*, obstacleLayerMask*/);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + 2*direction2.normalized, direction2, detectionRange/*, obstacleLayerMask*/);

        //Debug.LogWarning(hit1.collider.gameObject.name);
        //Debug.LogWarning(hit2.collider.gameObject.name);
        if (hit1.collider != null && hit2.collider != null && hit1.collider.CompareTag("Player") && hit2.collider.CompareTag("Player")) {
            if (direction1.magnitude < direction2.magnitude && direction2.magnitude <= detectionRange && player1.gameObject.GetComponent<PlayerMove_2P>().isAlive) {
                return player1;
            } else if (direction1.magnitude > direction2.magnitude && direction1.magnitude <= detectionRange && player2.gameObject.GetComponent<PlayerMove_2P>().isAlive) {
                return player2;
            } else {
                return null;
            }
        } else if (hit1.collider != null && (hit2.collider == null || hit1.collider.CompareTag("Player") && !hit2.collider.CompareTag("Player") && direction1.magnitude <= detectionRange) && player1.gameObject.GetComponent<PlayerMove_2P>().isAlive) {
            return player1;
        } else if (hit2.collider != null && (hit1.collider == null || !hit1.collider.CompareTag("Player") && hit2.collider.CompareTag("Player") && direction2.magnitude <= detectionRange) && player2.gameObject.GetComponent<PlayerMove_2P>().isAlive) {
            return player2;
        } else {
            // Colliders are messed up
            return null;
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
            lineOfSight.SetPosition(1, (isChasing!=null) ? isChasing.position : transform.position);
        }
    }
}
