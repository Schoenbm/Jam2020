using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onions : MonoBehaviour
{
    // i'm litteraly programming an onion class. what i have become.


    public AudioSource alarmAudio;
   
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float rayMaxDistance = 3;
    [SerializeField] private float distanciationForce;

    private Vector3 direction;
    private GameObject aPlayer;
    private bool foundPlayer = false;
    private float speed;

    private Rigidbody2D rb2d;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public void Start()
    {
        foundPlayer = false;
        rb2d = this.gameObject.GetComponentInParent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!foundPlayer && collision.gameObject.tag == "Player")
        {
            Debug.Log("found player on trigger");
            RaycastHit2D hit;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position - this.transform.position, (collision.gameObject.transform.position - this.transform.position).magnitude);
            if (hit.collider == null)
            {
                foundPlayer = true;
                aPlayer = collision.gameObject;
                alarmAudio.Play();
            }
            else
            {
                Debug.Log("problem : object in between = " + hit.collider.gameObject.name);
                Debug.DrawLine(this.transform.position, collision.gameObject.transform.position);
            }
        }

        if (collision.gameObject.tag.Equals("Onion"))
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(this.transform.position, collision.gameObject.transform.position, maxDistance);
            if (hit.collider == null)
            {
                if(foundPlayer)
                    collision.gameObject.GetComponent<OnionListener>().listenOnion(this.aPlayer);

                direction = this.transform.position - collision.transform.position;

                if (direction.magnitude < maxDistance)
                    rb2d.AddForce(direction.normalized * distanciationForce);
            }
        }
    }

    public void Update()
    {
        Animate();


        if (foundPlayer && aPlayer == null)
        {
            foundPlayer = false;
        }
        else if (foundPlayer)
        {
            direction = aPlayer.gameObject.transform.position - this.transform.position;

            Vector3 vel = rb2d.velocity;

            spriteRenderer.flipX = vel.x < 0;

            if (vel.magnitude < maxSpeed)
                rb2d.AddForce(direction.normalized * speed);
        }
    }

    public void alertOnion(GameObject pPlayer)
    {
        this.aPlayer = pPlayer;
        foundPlayer = true;
    }

    private void Animate()
    {
        bool animIsWalking = animator.GetBool("isWalking");

        if(animIsWalking != foundPlayer)
        {
            animator.SetBool("isWalking", foundPlayer);
        }
    }
}
