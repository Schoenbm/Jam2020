using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionHitBox : MonoBehaviour
{

    //TODO : Kill current chicken if hits chicken
    //a destroy parent function if hit by attack

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            Destroy(transform.parent.gameObject);
        }
        else if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
