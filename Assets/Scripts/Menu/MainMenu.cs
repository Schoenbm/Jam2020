using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Instructions;

    void Start()
    {

    }

    public void Play()
    {
        Debug.Log("Let's go");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void QuitButton()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}

