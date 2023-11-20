using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController control;

    public int candyCount;

    public float timeSinceDash;

    public int timeSinceLine;

    private void Awake()
    {
        //if a control already, delete
        if (control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);

            //If none, make this the control
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

    }

    void Update() {
        timeSinceDash = timeSinceDash + Time.deltaTime;
    }

    public bool Line(){
        return true;
    }

    public bool Dash(){
        return true;
    }

}

