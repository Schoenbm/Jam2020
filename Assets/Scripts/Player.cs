using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float lifeSpan = 40f;
    
    private float duringAttack = 0f;

    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = false;
    private int collectedEggs = 0;

    private bool canLayEgg = false;
    private bool hasEgg = false;
    private bool isLayingEgg = false;
    private Nest lastNest; // last nest the player interacted with

    public  BoxCollider2D hitBoxAttack;
    private GameController gameController; // set by the gameController


    [SerializeField] private float cdAttack = 0;
    private float actualCd;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hitBoxAttack.enabled =false;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
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
        if ( h < 0)
        {
            this.transform.localScale = new Vector3(1,1,1);
        }
        else if (h > 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    public void Die()
    {
        this.gameController.manageDeath(collectedEggs);

        Destroy(gameObject); 
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
            hasEgg = true;
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

    public void Attack()
    {
        if (Input.GetAxisRaw("Fire1") != 0 && actualCd <= 0) // Fire 1 is "X" on xbox, "Q" on keyboard, left clic on mouse.
        {
            hitBoxAttack.enabled =true;
            Debug.Log("pew pew");
            //TODO ATTACK
            actualCd = cdAttack;
            duringAttack = 0.3f;
        }
        if (actualCd > 0)
            actualCd -= Time.deltaTime;
        if (duringAttack > 0)
            duringAttack -= Time.deltaTime;
        else if (hitBoxAttack.enabled)
            hitBoxAttack.enabled = false;
    }


    private void Animate()
    {
        bool isWalking = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;
        bool animIsWalking = animator.GetBool("isWalking");

        if (isWalking != animIsWalking)
        {
            animator.SetBool("isWalking", isWalking);
        }
    }

    public void setGameController(GameController pGC)
    {
        this.gameController = pGC;
    }
}
