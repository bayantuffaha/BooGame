using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text bomb;
    public Text bottle;

    string score = "0"; // Change the type and the initial value
    string bombScore = "0";
    string bottleScore = "0";

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score;
        bomb.text = bombScore;
        bottle.text = bottleScore;
    }

    // Update is called once per frame
    void Update()
    {
        // Assign the score to the global count as a string
        score = (GameController.control.candyCount).ToString();
        bombScore = (GameController.control.bombCount).ToString();
        bottleScore = (GameController.control.bottleCount).ToString();
        scoreText.text = score; // Update the text
        bomb.text = bombScore;
        bottle.text = bottleScore;
    }
}
