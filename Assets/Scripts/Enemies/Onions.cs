using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onions : MonoBehaviour
{
    // i'm litteraly programming an onion class. what i have become.

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private Vector3 direction;
    private GameObject aPlayer;
    private bool foundPlayer = false;
    private float speed;

    public void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!foundPlayer && collision.gameObject.tag == "Player")
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position - this.transform.position);
            if (hit.collider == null)
            {
                foundPlayer = true;
                aPlayer = collision.gameObject;
            }

        }

        if (foundPlayer && collision.gameObject.tag.Equals("Onion"))
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position - this.transform.position);
            if (hit.collider == null)
            {
                Debug.Log("Alert sent");
                collision.gameObject.GetComponent<Onions>().alertOnion(this.aPlayer);
            }
            else
                Debug.Log(hit.transform.gameObject.tag);
        }
    }

    public void Update()
    {
        if (foundPlayer && aPlayer == null)
        {
            foundPlayer = false;
        }
        else if (foundPlayer)
        {
            direction = aPlayer.gameObject.transform.position - this.transform.position;
            this.transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    public void alertOnion(GameObject pPlayer)
    {
        Debug.Log("alert rouuuuuuuuuuge");
        this.aPlayer = pPlayer;
        foundPlayer = true;
    }
}
