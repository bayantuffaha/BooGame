using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;

    string score = "0"; // Change the type and the initial value

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score;
    }

    // Update is called once per frame
    void Update()
    {
        // Assign the score to the global count as a string
        score = (GameController.control.candyCount).ToString();
        scoreText.text = score; // Update the text
    }
}
