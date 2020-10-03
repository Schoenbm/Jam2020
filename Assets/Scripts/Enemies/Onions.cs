using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onions : MonoBehaviour
{
    // i'm litteraly programming an onion class. what i have become.

    [SerializeField] private float speed = 10;
    private Vector3 direction;
    private GameObject aPlayer;
    private bool foundPlayer = false;
    private bool playerOnTrigger = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!foundPlayer && collision.gameObject.tag == "Player")
        {
            RaycastHit2D hit;
            playerOnTrigger = true;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position);
            if (hit.collider == null)
            {
                Debug.Log("nothing between");
                foundPlayer = true;
                aPlayer = collision.gameObject;
            }
            else
                Debug.Log(hit.transform.gameObject.tag);

        }
    }

    public void Update()
    {
        if (foundPlayer)
        {
            direction = aPlayer.gameObject.transform.position - this.transform.position;
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }
}
