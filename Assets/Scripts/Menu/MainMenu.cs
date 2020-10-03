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
        MenuButton();
    }

    public void Play()
    {
        Debug.Log("Let's go");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void InstructionsButton()
    {
        Menu.SetActive(false);
        Instructions.SetActive(true);
    }

    public void MenuButton()
    {
        // Show Main Menu
        Menu.SetActive(true);
        Instructions.SetActive(false);
    }

    public void QuitButton()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}

