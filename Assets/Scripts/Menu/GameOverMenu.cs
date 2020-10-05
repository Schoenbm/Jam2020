using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverEvent;
    private bool gameHadEnded = false; // GAME OVER BIATCH

    public void GameOver() // GameOverMenu
    {
        
        if (gameHadEnded == false)
        {
            gameHadEnded = true;
            Debug.Log("GameOver");
            GameOverEvent.SetActive(true);
            gameHadEnded = false;
            
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
