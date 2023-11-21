using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController control;

    public int candyCount;

    public float timeSinceDash;
    public float timeSinceLine;

    public int lineCost;
    public int dashCost;

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
        timeSinceLine = timeSinceLine + Time.deltaTime;
    }

    public bool Line(){
        if (candyCount >= lineCost) {
            timeSinceLine = 0;
            candyCount-=lineCost;
            return true;
        }
        return false;
    }

    public bool Dash(){
        if (candyCount >= dashCost) {
            timeSinceDash = 0;
            candyCount-=dashCost;
            return true;
        }
        return false;
    }

}

