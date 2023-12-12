using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacking : MonoBehaviour
{
    public GameObject otherPlayer;
    public bool isP1;
    string stackButton;

    private bool stacked;

    // Start is called before the first frame update
    //sets the button depending on which player is activating.
    void Start()
    {
        if (isP1)
        {
            stackButton = "Stack_p1";
        }
        else
        {
            stackButton = "Stack_p2";
        }

        stacked = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetButtonDown(stackButton) && !stacked)
        {
            otherPlayer.transform.position = transform.position;
            otherPlayer.transform.parent = transform;
            otherPlayer.GetComponentInChildren<SpriteRenderer>().enabled = false;
            otherPlayer.GetComponent<PlayerMove_2P>().enabled = false;

            //switch sprite to a stacked sprite
        }
        else if (Input.GetButtonDown(stackButton) && stacked)
        {
            //need to detach child from this, reactivate sprite and movement
            //otherPlayer.transform.parent = transform;

            otherPlayer.GetComponentInChildren<SpriteRenderer>().enabled = true;
            otherPlayer.GetComponent<PlayerMove_2P>().enabled = true;

            //switch sprite to a normal sprites
        }
    }
}
