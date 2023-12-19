using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveStation : MonoBehaviour
{
    public GameController cont;
    public int spin = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        cont = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cont.candyCount >= cont.reviveCost){
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = spin;}
        if (cont.candyCount < cont.reviveCost)
        {
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
}
