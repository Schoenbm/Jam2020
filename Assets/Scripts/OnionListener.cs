using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionListener : MonoBehaviour
{
    private Onions onion;
    // Onion listener listen to the alarm and inform the onion script
    void Start()
    {
        onion = GetComponentInChildren<Onions>(); 
    }

    public void listenOnion(GameObject player)
    {
        onion.alertOnion(player);
    }
}
