using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    // public Tween tw;
    [SerializeField] GameObject menuUI;
    bool GameIsPaused;

    void Start() {Resume();}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        menuUI.SetActive(true);
        // tw.ButtonPop();
        GameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        menuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
