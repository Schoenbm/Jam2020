using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionHitBox : MonoBehaviour
{
    public AudioSource deathAudio;
    public BoxCollider2D hitbox;
    public SpriteRenderer onionSprite;
    public Rigidbody2D rb2d;

    //TODO : Kill current chicken if hits chicken
    //a destroy parent function if hit by attack

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack") 
        {
            deathAudio.Play();
            onionSprite.enabled = false;
            hitbox.enabled = false;
            rb2d.Sleep();
            Destroy(transform.parent.transform.parent.gameObject, 2f);
        }
        else if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
