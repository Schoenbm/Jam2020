using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text timerText;
    public Text eggText;

    [SerializeField] private int totalEggs = 0;
    private int totalEggsCollected;

    private float timeLeft = 60f;
    private Egg egg; // keeps track of the egg that will hatch
    private bool notHatching = true;


    public GameObject[] playerPrefabs; //all chickens prefab
    private Player player;

    public GameObject[] cameras; // 2 cameras needed, 1 deactivated/inactive
    private int activeCamera = 0; // index for active camera
    
    // Start is called before the first frame update
    void Start()
    {
        int randInt = Random.Range(0, playerPrefabs.Length);
        player = Instantiate(playerPrefabs[randInt]).GetComponent<Player>();
        player.setGameController(this);
        timeLeft = player.lifeSpan;
        cameras[activeCamera].GetComponent<CinemachineVirtualCamera>().m_Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
        collectibleEggRatio();
    }

    void Countdown()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0 && notHatching)
        {
            player.Die();
        }

        timerText.text = Mathf.Round(timeLeft > 0 ? timeLeft : 0).ToString();

    }
    

    public void manageDeath(int eggCount)
    {
        totalEggsCollected += eggCount;
        int inactiveCamera = (activeCamera + 1) % 2;

        if (egg != null)
        {
            notHatching = false;
            // camera look at egg that will hatch (blends smoothly)
            cameras[inactiveCamera].GetComponent<CinemachineVirtualCamera>().Follow = egg.transform;
            cameras[activeCamera].SetActive(false);
            cameras[inactiveCamera].SetActive(true);

            egg.Hatch(); // egg hatching animation
            StartCoroutine(waitSpawn(egg.animationLength, inactiveCamera, egg.transform.position));

        }
        else
        {
            SceneManager.LoadScene(0); // to change
        }

    }


    public void SetEgg(Egg e)
    {
        egg = e;
    }

    IEnumerator waitSpawn(float waitTime, int inactiveCamera, Vector3 eggPosition) {
        yield return new WaitForSeconds(waitTime);
        int randInt = Random.Range(0, playerPrefabs.Length);
        player = Instantiate(playerPrefabs[randInt], eggPosition, Quaternion.identity).GetComponent<Player>(); // create next chicken
        
        // there is a slight delay which makes the camera move weird for a second
        cameras[inactiveCamera].GetComponent<CinemachineVirtualCamera>().Follow = player.transform; // camera look at the player back
        player.setGameController(this);


        activeCamera = inactiveCamera; // switch cameras

        timeLeft = player.lifeSpan;
        notHatching = true;
    }

    private void collectibleEggRatio()
    {
        int eggsCollectedRun = player.getEggCount();
        eggText.text = (eggsCollectedRun + totalEggsCollected).ToString(    ) + " / " + totalEggs; 
    }
}
