using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreText;  
    private int score = 0;  

    void Start()
    {
        
        if (scoreText == null)
        {
            Debug.LogError("Please assign the UI Text element .");
        }
        else
        {
            
            UpdateScoreText();
        }
    }

    void Update()
    {
        
    }

    void UpdateScoreText()
    {
        
        scoreText.text = "" + score;
    }

    
    public void EnemyDestroyed()
    {
        
        score += 100;

        
        UpdateScoreText();
    }
}
