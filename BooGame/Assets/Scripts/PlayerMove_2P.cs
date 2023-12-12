using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement = UnityEngine.SceneManagement;

public class PlayerMove_2P : MonoBehaviour
{
    public bool isFacingRight = true;
    public bool isFacingDown = true;
    public bool isDash = false;
    public Sprite[] frontIdle;
    public Sprite[] backIdle;
    public Sprite[] frontRun;
    public Sprite[] backRun;
    public int uPF;
    int animFrame = 0;
    SpriteRenderer s;
    public float speed = 5;
    //public AnimationCurve acc;
    public float accTime;
    int timeSinceIdle = 0;
    int timeSinceMove = 0;
    public float dashDuration = 0.1f;
    //public float lineDuration;
    public float dashSpeed;
    public AnimationCurve dashCurve;
    Vector2 dashDirection;
    public PlayerMove_2P otherP;
    public GameController cont;
    //public LineRenderer line;
    //public bool isLine;
    float sticky;

    public GameObject holding = null;
    public float holdRange = 5;

    public int playersAlive = 2;
    public bool isAlive = true;
    public bool isP1 = true;
    private string theHorizontal;
    private string theVertical;
    private string theDash;
    private string theLine;
    public int candiesToCollect = 20;
    private double candiesAtDeath;
    

    // Start is called before the first frame update
    void Start()
    {
        if (isP1) {
            theHorizontal = "p1_Horiz";
            theVertical = "p1_Vert";
            theDash = "Fire2";
            theLine = "Fire3";
        } else
        {
            theHorizontal = "p2_Horiz";
            theVertical = "p2_Vert";
            theDash = "Fire1";
            theLine = "Fire3";
        }

        s = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("num alive: " + playersAlive);
        //dash
        if (Input.GetButtonDown(theDash) && !(otherP.holding == gameObject) && cont.Dash()) {
            isDash = true;
            dashDirection = new Vector2(Input.GetAxis(theHorizontal), Input.GetAxis(theVertical));
        }
            
        if (isDash) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(dashSpeed*dashDirection.x*dashCurve.Evaluate(cont.timeSinceDash/dashDuration), dashSpeed*dashDirection.y*dashCurve.Evaluate(cont.timeSinceDash/dashDuration));
            if (cont.timeSinceDash >= dashDuration) {
                isDash = false;
            }
            timeSinceMove++;
        }



        /*line/revive
        if (Input.GetButtonDown(theLine) && cont.Line()) {
            isLine = true;
        }

        if (isLine) {
            if(isP1){
                line.SetPosition(0, otherP.transform.position);
                line.SetPosition(1, gameObject.transform.position);
            } else {
                line.SetPosition(1, otherP.transform.position);
                line.SetPosition(0, gameObject.transform.position);
            }
            if (cont.timeSinceLine >= lineDuration){
                isLine = false;
            }
        } else {
            line.SetPosition(0, new Vector3(0,0,1));
            line.SetPosition(1, new Vector3(0,0,1));
        }*/



        if (!isDash) {
            isFacingRight = Input.GetAxis(theHorizontal)>0 || (!(Input.GetAxis(theHorizontal)<0) && isFacingRight);
            isFacingDown = Input.GetAxis(theVertical)<0 || (!(Input.GetAxis(theVertical)>0) && isFacingDown);
            s.flipX = !isFacingRight; //!((isFacingRight && isFacingDown) || (!isFacingRight && !isFacingDown));
            if (Time.frameCount % 10 == 0) {
                if (Input.GetAxis(theVertical)==0 && Input.GetAxis(theHorizontal)==0) {
                    if (isFacingDown) {
                        s.sprite = frontIdle[animFrame];
                    } else {
                        s.sprite = backIdle[animFrame];
                    }
                    timeSinceMove++;
                    timeSinceIdle = 0;
                } else {
                    if (timeSinceIdle % uPF > uPF/2) {
                        if (isFacingDown) {
                            s.sprite = frontRun[animFrame];
                        } else {
                            s.sprite = backRun[animFrame];
                        }
                    } else {
                        if (isFacingDown) {
                            s.sprite = frontIdle[animFrame];
                        } else {
                            s.sprite = backIdle[animFrame];
                        }
                    }
                    timeSinceIdle++;
                    timeSinceMove = 0;
                }
                animFrame = (animFrame+1)%4;
            }

            if(otherP.holding != gameObject){
                if(Input.GetKeyDown("u") && isP1){
                    Hold();
                } else if (Input.GetKeyDown("i") && !isP1){
                    Hold();
                }
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(gameObject.GetComponent<Rigidbody2D>().velocity, new Vector2(Input.GetAxis(theHorizontal)*speed*(1-sticky), Input.GetAxis(theVertical)*speed*(1-sticky)), accTime);
            }
        }

        if(holding != null){
            holding.transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y+2f);
            holding.transform.parent = gameObject.transform;
        }

        CheckRevivalCondition();
    }

    void CheckRevivalCondition()
    {
        if (playersAlive == 1)
        {
            if (GameController.control.candyCount - candiesAtDeath >= candiesToCollect)
            {
                RevivePlayer();
            }
        }
    }

    void RevivePlayer()
    {
        isAlive = true;
        speed = 5;
        dashDuration = 0.1f;
        s.color = new Color(1f, 1f, 1f, 1f);
        s.enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;

        playersAlive++;
        gameObject.transform.SetParent(null);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
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

    public void Die() {
        // ZombieJumpScare ZJumpScare = GetComponent<ZombieJumpScare>();

        // ZJumpScare.ShowJumpScare();

        cont.ZombieScare();

        gameObject.transform.SetParent(otherP.transform);
        gameObject.transform.position = otherP.transform.position;
        isAlive = false;
        speed = 0;
        dashDuration = 0;
        s.color = new Color(0f,0f,0f,0f);
        s.enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        candiesAtDeath = GameController.control.candyCount;

        // Decrement the playersAlive variable by one
        playersAlive--;
        // If no players are alive, switch to the loss scene
        /*if (playersAlive <= 0 && GameController.control.candyCount >= 25)
        {
            // Start a coroutine to load the EndWin scene after some seconds
            StartCoroutine(LoadSceneWithDelay("EndWin", 3f));
        }
        else */if (playersAlive <= 0 && GameController.control.candyCount <= 25)
        {
            // Start a coroutine to load the EndLose scene after some seconds
            StartCoroutine(LoadSceneWithDelay("EndLose", 3f));
        }

        // Define a coroutine that takes a scene name and a delay time as parameters
        IEnumerator LoadSceneWithDelay(string sceneName, float delayTime)
        {
            // Wait for the specified amount of time
            yield return new WaitForSeconds(delayTime);
            // Load the scene
            SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    public static int BooltoInt(bool b){
        if(b){return 1;}else{return 0;}
    }

    public void Hold()
    {
        if(holding == null){
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                if(go==gameObject){continue;}
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            if(distance < holdRange){
                holding = closest;
            } else {return;}
            holding.transform.parent = gameObject.transform;
            holding.transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y+2f);
            holding.GetComponent<Rigidbody2D>().velocity = (new Vector2(0f,0f));
        } else {
            holding.transform.localPosition = new Vector2(0f,0f);
            holding.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis(theHorizontal) * 100f + 10f * (BooltoInt(isFacingRight) - 0.5f), Input.GetAxis(theVertical) * 100f + 10f * (BooltoInt(!isFacingDown) - 0.5f));
            holding.transform.parent = null;
            holding = null;
        }
    }
}
