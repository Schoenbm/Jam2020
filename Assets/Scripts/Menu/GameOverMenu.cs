using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameController gameController; 
    public GameObject GameOverEvent;
    public GameObject GameVictoryEvent;
    private bool gameHadEnded = false; // GAME OVER BIATCH
    public Text ScoreEggs;

    public void GameOver() // GAME OVER
    {
        
        if (gameHadEnded == false)
        {
            gameHadEnded = true;
            Debug.Log("GameOver");
            GameOverEvent.SetActive(true);
            gameHadEnded = false;
            
        }
    }

    public void GameVictory() // GAME VICTORY
    {
        if(gameHadEnded == false)
        {
            gameHadEnded = true;
            GameVictoryEvent.SetActive(true);
            ScoreEggs.text = gameController.getScore().ToString();
            gameHadEnded = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
