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
    public AnimationCurve acc;
    public float accTime;
    int timeSinceIdle = 0;
    int timeSinceMove = 0;
    public float dashDuration;
    public float lineDuration;
    public float dashSpeed;
    public AnimationCurve dashCurve;
    Vector2 dashDirection;
    public Transform otherP;
    public GameController cont;
    public LineRenderer line;
    public bool isLine;

    public int playersAlive = 2;

    public bool isP1 = true;
    private string theHorizontal;
    private string theVertical;
    private string theDash;
    private string theLine;

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
        //dash
        if (Input.GetButtonDown(theDash) && cont.Dash()) {
            isDash = true;
            dashDirection = new Vector2(Input.GetAxis(theHorizontal), Input.GetAxis(theVertical));
        }
            
        if (isDash) {
            gameObject.transform.Translate(new Vector3(dashSpeed*dashDirection.x*Time.deltaTime*dashCurve.Evaluate(cont.timeSinceDash/dashDuration), dashSpeed*dashDirection.y*Time.deltaTime*dashCurve.Evaluate(cont.timeSinceDash/dashDuration)));
            if (cont.timeSinceDash >= dashDuration) {
                isDash = false;
            }
            timeSinceMove++;
        }



        //line/revive
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
        }



        if (!isDash) {
            isFacingRight = Input.GetAxis(theHorizontal)>0 || (!(Input.GetAxis(theHorizontal)<0) && isFacingRight);
            isFacingDown = Input.GetAxis(theVertical)<0 || (!(Input.GetAxis(theVertical)>0) && isFacingDown);
            s.flipX = !((isFacingRight && isFacingDown) || (!isFacingRight && !isFacingDown));
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
            if (timeSinceIdle < accTime && timeSinceIdle > 0) {
                gameObject.transform.Translate(new Vector3(Input.GetAxis(theHorizontal)*speed*Time.deltaTime*acc.Evaluate(timeSinceIdle/accTime), Input.GetAxis(theVertical)*speed*Time.deltaTime*acc.Evaluate(timeSinceIdle/accTime)));
            } else {
                gameObject.transform.Translate(new Vector3(Input.GetAxis(theHorizontal)*speed*Time.deltaTime, Input.GetAxis(theVertical)*speed*Time.deltaTime));
            }
        }
    }

    public void Die() {
        // ZombieJumpScare ZJumpScare = GetComponent<ZombieJumpScare>();

        // ZJumpScare.ShowJumpScare();

        cont.ZombieScare();

        gameObject.transform.SetParent(otherP);
        gameObject.transform.position = otherP.transform.position;
        speed = 0;
        dashDuration = 0;
        s.color = new Color(0f,0f,0f,0f);
        s.enabled = false;

        // Decrement the playersAlive variable by one
        playersAlive--;
        // If no players are alive, switch to the loss scene
        if (playersAlive <= 0 && GameController.control.candyCount >= 25)
        {
            SceneManagement.SceneManager.LoadScene("EndWin");
        }
        else if (playersAlive <= 0 && GameController.control.candyCount <= 25)
        {
            SceneManagement.SceneManager.LoadScene("EndLose");
        }
    }

}
