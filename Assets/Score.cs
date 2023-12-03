using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text scoreText;

    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
           this.score = value; 
           UpdateScore();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void UpdateScore()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreText.text = scoreStr;
    }
}
