using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController control;

    public double candyCount;

    public float timeSinceDash;
    public float timeSinceLine;

    public float dashCooldown;
    public float lineCooldown;

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

    public void StartGame() {
        Debug.Log("starting the game!");
        SceneManager.LoadScene("Build Halloween");
    }  

    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    public bool Line(){
        if (candyCount >= lineCost && (timeSinceLine > lineCooldown || timeSinceLine < 0)) {
            timeSinceLine = -0.04f;
            candyCount-=lineCost;
            return true;
        }
        return false;
    }

    public bool Dash(){
        if (candyCount >= dashCost && timeSinceDash > dashCooldown) {
            timeSinceDash = 0;
            candyCount-=dashCost;
            return true;
        }
        return false;
    }

}

