using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameHandler : MonoBehaviour
{
    // public GameObject pointsText;
    // private int pointsNum = 0;
    private GameObject player;
    public GameController cont;

    // public bool isDefending = false;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        cont = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        // sceneName = SceneManager.GetActiveScene().name;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        Debug.Log("starting the game!");
        cont.candyCount = 2000;
        SceneManager.LoadScene(0);
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
        cont.candyCount = cont.candyCountAtStart;
        cont.bombCount = cont.bombCountAtStart;
        cont.speedCount = cont.speedCountAtStart;
        cont.bottleCount = cont.bottleCountAtStart;
        // GameHandler_Pausemenu.GameisPaused = false;
            SceneManager.LoadScene(cont.level);
            // Please also reset all static variables here, for new games!
        // playerHealth = StartPlayerHealth;
    }

    public void RestartGameWin() {
        Time.timeScale = 1f;
        // GameHandler_Pausemenu.GameisPaused = false;
        cont.candyCount = 0;
        cont.bombCount = 0;
        cont.speedCount = 0;
        cont.bottleCount = 0;
        SceneManager.LoadScene(7);
            // Please also reset all static variables here, for new games!
        // playerHealth = StartPlayerHealth;
    }

//   // Replay the Level where you died
//     public void ReplayLastLevel() {
//         Time.timeScale = 1f;
//         GameHandler_PauseMenu.GameisPaused = false;
//         SceneManager.LoadScene("lastLevelDied");
//          // Reset all static variables here, for new games:
//         playerHealth = StartPlayerHealth;
//     }
}
