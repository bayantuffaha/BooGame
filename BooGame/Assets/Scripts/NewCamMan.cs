using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class CameraManager_SplotScroon : MonoBehaviour
{

    public GameObject cameraMain;
    public GameObject cameraPlayer1;
    public GameObject cameraPlayer2;
    public Transform player1;
    public Transform player2;
    public Vector2 cameraDistance = new Vector2(8f,4.5f);
    public Vector2 playerDistance;

    void Start()
    {
        cameraMain.SetActive(true);
        cameraPlayer1.SetActive(true);
        cameraPlayer2.SetActive(true);
    }

    void Update()
    {
        FindPlayerCenter(); //the GameHandler acts as the target for the MainCamera
        
        playerDistance = new Vector2((float)Math.Abs(player1.position.x - player2.position.x), (float)Math.Abs(player1.position.y - player2.position.y)); //get distance
                                                                                     //if distance is within threshold, use fullscreen MainCamera
        if (playerDistance.x < cameraDistance.x && playerDistance.y < cameraDistance.y)
        {
            cameraMain.SetActive(true);
            cameraPlayer1.GetComponent<Camera>().enabled = false;
            cameraPlayer2.GetComponent<Camera>().enabled = false;
            cameraMain.GetComponent<Camera>().enabled = true;
            cameraPlayer1.SetActive(false);
            cameraPlayer2.SetActive(false);
        }
        //else, use splitscreen
        else
        {
            cameraPlayer1.SetActive(true);
            cameraPlayer2.SetActive(true);
            cameraPlayer1.GetComponent<Camera>().enabled = true;
            cameraPlayer2.GetComponent<Camera>().enabled = true;
            cameraMain.GetComponent<Camera>().enabled = false;
            cameraMain.SetActive(false);
        }
    }

    // set this object's position to the center of the two players
    void FindPlayerCenter()
    {
        Vector3 pos;
        pos.x = player1.position.x + (player2.position.x - player1.position.x) / 2;
        pos.y = player1.position.y + (player2.position.y - player1.position.y) / 2;
        pos.z = player1.position.z + (player2.position.z - player1.position.z) / 2;
        transform.position = new Vector3(pos.x, pos.y, -10);
    }
}