using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraManager_SplitScreen : MonoBehaviour
{

    public GameObject cameraMain;
    public GameObject cameraPlayer1;
    public GameObject cameraPlayer2;
    public Transform player1;
    public Transform player2;
    public float cameraDistance = 5f;

    void Start()
    {
        cameraMain.SetActive(true);
        cameraPlayer1.SetActive(true);
        cameraPlayer2.SetActive(true);
    }

    void Update()
    {
        FindPlayerCenter(); //the GameHandler acts as the target for the MainCamera

        float playerDistance = Vector3.Distance(player1.position, player2.position); //get distance
                                                                                     //if distance is within threshold, use fullscreen MainCamera
        if (playerDistance < cameraDistance)
        {
            cameraPlayer1.GetComponent<Camera>().enabled = false;
            cameraPlayer2.GetComponent<Camera>().enabled = false;
            cameraMain.GetComponent<Camera>().enabled = true;
        }
        //else, use splitscreen
        else
        {
            cameraPlayer1.GetComponent<Camera>().enabled = true;
            cameraPlayer2.GetComponent<Camera>().enabled = true;
            cameraMain.GetComponent<Camera>().enabled = false;
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