using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacking : MonoBehaviour
{
    public GameObject otherPlayer;
    public bool isP1;
    string stackButton;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("button was pressed K");
        }
        
        if(Input.GetButtonDown(stackButton))
        {
            otherPlayer.transform.position = transform.position;
            otherPlayer.transform.parent = transform;
            otherPlayer.GetComponentInChildren<SpriteRenderer>().enabled = false;
            // otherPlayer.GetComponentInChildren<PlayerMove_2P>.enabled = false;

        }
    }
}
