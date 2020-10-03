using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    public GameObject egg;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LayEgg()
    {
        // create new egg in nest and pass it to game controller
        GameObject newEgg = Instantiate(egg, transform.position, Quaternion.identity);
        gameController.SetEgg(newEgg.GetComponent<Egg>());
        Debug.Log("out");
    }
}
