using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement = UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove_2P : MonoBehaviour
{
    public bool isFacingRight = true;
    public bool isFacingDown = true;
    public bool isDash = false;
    public SpriteRenderer[] anims;
    //public int uPF;
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
    string theHorizontal;
    string theVertical;
    string theDash;
    string theBottle;
    string theStack;
    //private string theLine;
    public int candiesToCollect = 20;
    // private double candiesAtDeath;
    public GameObject bottlePrefab;

    /*public UnityEngine.UI.Button reviveButton;
    public Transform revivalStation;*/
    

    // Start is called before the first frame update
    void Start()
    {
        if (isP1) {
            theHorizontal = "p1_Horiz";
            theVertical = "p1_Vert";
            theDash = "p1_Dash";
            theBottle = "p1_Bottle";
            theStack = "p1_Stack";
            //theLine = "Fire3";
        } else
        {
            theHorizontal = "p2_Horiz";
            theVertical = "p2_Vert";
            theDash = "p2_Dash";
            theBottle = "p2_Bottle";
            theStack = "p2_Stack";
            //theLine = "Fire3";
        }

        s = GetComponentInChildren<SpriteRenderer>();

        /*reviveButton.gameObject.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(holding != null){
            holding.transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y+2f);
            holding.transform.parent = gameObject.transform;
        }

        //CheckRevivalCondition();

        foreach(SpriteRenderer i in anims){
            i.enabled = false;
            i.flipX = !isFacingRight;
        }
        
        if(!isAlive){
            anims[8].enabled = true;
            return;
        }

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

        if(Input.GetButtonDown(theBottle) && cont.Bottle()){Bottle();}



        if (!isDash) {
            isFacingRight = Input.GetAxis(theHorizontal)>0 || (!(Input.GetAxis(theHorizontal)<0) && isFacingRight);
            isFacingDown = Input.GetAxis(theVertical)<0 || (!(Input.GetAxis(theVertical)>0) && isFacingDown);
            
            if (holding == null){
                if (Input.GetAxis(theVertical)==0 && Input.GetAxis(theHorizontal)==0) {
                    if (isFacingDown) {
                        anims[0].enabled = true;
                    } else {
                        anims[1].enabled = true;
                    }
                    timeSinceMove++;
                    timeSinceIdle = 0;
                } else {
                    if (isFacingDown) {
                        anims[2].enabled = true;
                    } else {
                        anims[3].enabled = true;
                    }
                    timeSinceIdle++;
                    timeSinceMove = 0;
                }
            } else {
                if (Input.GetAxis(theVertical)==0 && Input.GetAxis(theHorizontal)==0) {
                    if (isFacingDown) {
                        anims[4].enabled = true;
                    } else {
                        anims[5].enabled = true;
                        anims[5].flipX = isFacingRight;
                    }
                    timeSinceMove++;
                    timeSinceIdle = 0;
                } else {
                    if (isFacingDown) {
                        anims[6].enabled = true;
                    } else {
                        anims[7].enabled = true;
                        anims[7].flipX = isFacingRight;
                    }
                    timeSinceIdle++;
                    timeSinceMove = 0;
                }
            }
            
            if(otherP.holding != gameObject){
                if(Input.GetButtonDown(theStack)){Hold();}
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(gameObject.GetComponent<Rigidbody2D>().velocity, new Vector2(Input.GetAxis(theHorizontal)*speed*(1-sticky), Input.GetAxis(theVertical)*speed*(1-sticky)), accTime);
            }
        }

        // if(IsAtRevivalStation()) {
        //     Debug.Log("at revival station"); 
        // } else {
        //     Debug.Log("NOT at revival station");
        // }

        // if (!isAlive && !otherP.isAlive) {
        //     playersAlive = 0;
        // } else if ((!isAlive && otherP.isAlive) || (isAlive && !otherP.isAlive)) {
        //     playersAlive = 1;
        // } else if (isAlive && otherP.isAlive) {
        //     playersAlive = 2;
        // }

    }

    /*void CheckRevivalCondition()
    {
        // if (playersAlive == 1)
        // {
        //     if (GameController.control.candyCount - candiesAtDeath >= candiesToCollect)
        //     {
        //         reviveButton.gameObject.SetActive(true);
        //         RevivePlayer();
        //     }
        // }

        bool canRevive = (GameController.control.candyCount >= candiesToCollect && IsAtRevivalStation() && otherP.IsAtRevivalStation());
        // Debug.Log(otherP.isAlive);
        // reviveButton.gameObject.SetActive(canRevive);

        if (playersAlive == 1 && canRevive) {
        //     // RevivePlayer();
            // Debug.Log("time to turn on button");
            reviveButton.gameObject.SetActive(canRevive);
        } else {
            // Debug.Log("not button time");
        }
    }

    public void RevivePlayerOnClick()
    {
        if (GameController.control.candyCount >= candiesToCollect)
        {
            GameController.control.candyCount -= candiesToCollect;

            // Update UI or game elements for the deducted candies

            RevivePlayer();

            reviveButton.gameObject.SetActive(false);
        }
    }*/

    void RevivePlayer()
    {
        isAlive = true;
        // speed = 5;
        // dashDuration = 0.1f;
        // s.color = new Color(1f, 1f, 1f, 1f);
        // s.enabled = true;
        // gameObject.GetComponent<Collider2D>().enabled = true;

        playersAlive = 2;
        otherP.playersAlive = 2;
        // gameObject.transform.SetParent(null);
        // Debug.Log("REVIVED HOMIE");
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Zombie") && isAlive)
        {
            Die();
        }
        if(col.gameObject.CompareTag("Revive") && !isAlive && cont.Revive())
        {
            RevivePlayer();
        }
    }

    public void Die() {
        // ZombieJumpScare ZJumpScare = GetComponent<ZombieJumpScare>();

        // ZJumpScare.ShowJumpScare();

        //cont.ZombieScare();                    Remmber to turn me on when you fix the gamecontroller

        isAlive = false;
        // candiesAtDeath = GameController.control.candyCount;

        // Decrement the playersAlive variable by one
        if (otherP.isAlive) {
            playersAlive = 1;
            otherP.playersAlive = 1;
        } else {
            playersAlive = 0;
            otherP.playersAlive = 0;
        }


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

    public void Bottle(){
        GameObject b = Instantiate(bottlePrefab);
        b.GetComponent<Bottle>().direction = new Vector2(Input.GetAxis(theHorizontal) * 100 + (BooltoInt(isFacingRight) - 0.5f), Input.GetAxis(theVertical) * 100 + (BooltoInt(!isFacingDown) - 0.5f));
        b.GetComponent<Bottle>().direction.Normalize();
        b.transform.position = (Vector2)gameObject.transform.position + b.GetComponent<Bottle>().direction * 2;
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
            holding.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis(theHorizontal) * 100f + 30f * (BooltoInt(isFacingRight) - 0.5f), Input.GetAxis(theVertical) * 100f + 30f * (BooltoInt(!isFacingDown) - 0.5f));
            holding.transform.parent = null;
            holding = null;
        }
    }
}
