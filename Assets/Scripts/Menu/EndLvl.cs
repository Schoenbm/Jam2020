using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLvl : MonoBehaviour
{
    public TextMeshProUGUI eggText;
    public GameController controller;
    public AudioManager audioManager;
    public GameObject winUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            eggText.text = "you saved " + controller.getScore() + " eggs !";
            winUI.SetActive(true);
            audioManager.playWin();
            collision.gameObject.GetComponent<Player>().setBlocked(true);
            Destroy(this);
        }
        Debug.Log(gameObject.tag);
    }
}
