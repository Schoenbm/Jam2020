using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionHitBox : MonoBehaviour
{
    public BoxCollider2D hitbox;
    public SpriteRenderer onionSprite;
    public Rigidbody2D rb2d;
    public bool ded = false;

    //TODO : Kill current chicken if hits chicken
    //a destroy parent function if hit by attack

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack") 
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().playOnionDeath();
            Destroy(transform.parent.transform.parent.gameObject);
        }
        else if(collision.gameObject.tag == "Player" && !ded )
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
