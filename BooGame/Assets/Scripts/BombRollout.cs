using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRollout : MonoBehaviour
{

    public GameObject Garlic;
    public Transform Thrower;
    public bool isP1;
    string BombButton;

    // Start is called before the first frame update
    void Start()
    {
        if (isP1)
            BombButton = "Bomb 1";
        else
            BombButton = "Bomb 2";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown(BombButton))
        {
            Debug.Log("button ressed");
            Instantiate(Garlic, Thrower);
        }
        if(Input.GetButtonDown("m"))
        {
            Debug.Log("m was pressed");
        }
    }

}
