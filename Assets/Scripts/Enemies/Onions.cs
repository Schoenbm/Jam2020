using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onions : MonoBehaviour
{
    // i'm litteraly programming an onion class. what i have become.

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float distanciationForce;

    private Vector3 direction;
    private GameObject aPlayer;
    private bool foundPlayer = false;
    private float speed;

    private Rigidbody2D rb2d;

    public void Start()
    {
        rb2d = this.gameObject.GetComponentInParent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!foundPlayer && collision.gameObject.tag == "Player")
        {
            Debug.Log("found player on trigger");
            RaycastHit2D hit;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position - this.transform.position);
            if (hit.collider == null)
            {
                foundPlayer = true;
                aPlayer = collision.gameObject;
            }
            else
            {
                Debug.Log("problem : object in between");
            }
        }

        if (collision.gameObject.tag.Equals("Onion"))
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position - this.transform.position);
            if (hit.collider == null)
            {
                Debug.Log("Alert sent");
                if(foundPlayer)
                    collision.gameObject.GetComponent<Onions>().alertOnion(this.aPlayer);

                direction = this.transform.position - collision.transform.position;

                if (direction.magnitude < maxDistance)
                    rb2d.AddForce(direction.normalized * distanciationForce);
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
            Debug.Log("found player");
            direction = aPlayer.gameObject.transform.position - this.transform.position;
            if (rb2d.velocity.magnitude < maxSpeed)
                rb2d.AddForce(direction.normalized * speed);
            //this.transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    public void alertOnion(GameObject pPlayer)
    {
        this.aPlayer = pPlayer;
        foundPlayer = true;
    }
}
