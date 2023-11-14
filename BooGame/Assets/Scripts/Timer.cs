using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
       public GameObject TimerText;
       private int gameTime = 60;
       private float timer = 0f;

       void Start () {
           UpdateTime();
       }
       void FixedUpdate(){
           timer += 0.03f;
            if (timer >= 1f){
                gameTime -= 1;
                timer = 0;
                UpdateTime();
            }
            if (gameTime <= 0){
                gameTime = 0;
                // GameObject.FindWithTag("GameController").GetComponent<EndScene>().setBool(true);s
            }   
      }

      public void UpdateTime(){
            Text timeTextTemp = TimerText.GetComponent<Text>();
            timeTextTemp.text = "Timer:" + gameTime;
      }
}
