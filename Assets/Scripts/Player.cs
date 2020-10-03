using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float lifeSpan = 40f;

    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = false;
    private int collectedEggs = 0;

    private bool canLayEgg = true;
    private bool hasEgg = true;
    private bool isLayingEgg = false;
    private Nest lastNest; // last nest the player interacted with

    [SerializeField] private float cdAttack = 0;
    private float actualCd;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        // calculate direction and speed
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(h, v, 0).normalized;

        float moveSpeed = Input.GetAxisRaw("Sprint") != 0 ? runSpeed : walkSpeed; // sprint is 'Shift' or 'Xbox B'

        transform.position += movement * moveSpeed * Time.deltaTime;

        // flip if necessary
        if (isFacingRight && h < 0)
        {
            isFacingRight = false;
        }
        else if (!isFacingRight && h > 0)
        {
            isFacingRight = true;
        }

        spriteRenderer.flipX = isFacingRight;
    }

    public void Die()
    {
        // TODO: play animation

        float animationLength = 0f; // TODO: replace 0 with animation length
        Destroy(gameObject, animationLength); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nest")
        {
            canLayEgg = true;
            lastNest = collision.gameObject.GetComponent<Nest>();
        }
        else if(collision.gameObject.tag == "Egg")
        {
            collectedEggs++;
            Debug.Log("collected : " + collectedEggs);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // lay the egg if button is pressed
        if(collision.gameObject.tag == "Nest")
        {
            if (Input.GetAxisRaw("Fire2") != 0) // 'Fire2' is 'B' in Xbox controller and left mouse or 'E' in keyboard
            {
                if (!isLayingEgg && canLayEgg && hasEgg)
                {
                    isLayingEgg = true;
                    hasEgg = false;
                    lastNest.LayEgg();
                }
            }
            else
            {
                isLayingEgg = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nest")
        {
            canLayEgg = false;
        }
    }

    public int getEggCount()
    {
        return collectedEggs;
    }

    private void Attack()
    {
        if (Input.GetAxisRaw("Fire1") != 0 && actualCd <= 0) // Fire 1 is "X" on xbox, "Q" on keyboard, left clic on mouse.
        {
            Debug.Log("pew pew");
            //TODO ATTACK
            actualCd = cdAttack;
        }
        if (actualCd > 0)
            actualCd -= Time.deltaTime;
    }
}
