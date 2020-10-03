﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text timerText;
    private float timeLeft = 60f;
    private Egg egg; // keeps track of the egg that will hatch
    private bool notHatching = true;


    public GameObject[] playerPrefabs;
    private Player player;

    public GameObject[] cameras; // 2 cameras needed, 1 deactivated/inactive
    private int activeCamera = 0; // index for active camera
    
    // Start is called before the first frame update
    void Start()
    {
        int randInt = Random.Range(0, playerPrefabs.Length);
        player = Instantiate(playerPrefabs[randInt]).GetComponent<Player>();
        timeLeft = player.lifeSpan;
        cameras[activeCamera].GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }

    void Countdown()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0 && notHatching)
        {
            TimeUp();
        }

        timerText.text = Mathf.Round(timeLeft).ToString();
    }
    
    void TimeUp()
    {
        player.Die(); // destroy player

        int inactiveCamera = (activeCamera + 1) % 2;
        
        if (egg != null)    
        {
            notHatching = false;
            // camera look at egg that will hatch (blends smoothly)
            cameras[inactiveCamera].GetComponent<CinemachineVirtualCamera>().m_LookAt = egg.transform;
            cameras[activeCamera].SetActive(false);
            cameras[inactiveCamera].SetActive(true);

            cameras[inactiveCamera].GetComponent<CinemachineVirtualCamera>().m_Follow = egg.transform;
            egg.Hatch(); // egg hatching animation
            StartCoroutine(waitSpawn(egg.animationLength, inactiveCamera, egg.transform.position));

        }
        else
        {
            SceneManager.LoadScene(0); // to change
        }

        // camera follows new player

       // cameras[inactiveCamera].GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;

        //activeCamera = inactiveCamera; // switch cameras

        // reset timer (maybe wait a bit so it's not that immediate)
        //timeLeft = player.lifeSpan;
    }

    public void SetEgg(Egg e)
    {
        egg = e;
    }

    IEnumerator waitSpawn(float waitTime, int inactiveCamera, Vector3 eggPosition) {
        yield return new WaitForSeconds(waitTime);
        int randInt = Random.Range(0, playerPrefabs.Length);
        player = Instantiate(playerPrefabs[randInt], eggPosition, Quaternion.identity).GetComponent<Player>(); // create next chicken


        cameras[inactiveCamera].GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;

        activeCamera = inactiveCamera; // switch cameras

        timeLeft = player.lifeSpan;
        notHatching = true;
    }

}
