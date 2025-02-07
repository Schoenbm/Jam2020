﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float lifeSpan = 40f;
    
    private float duringAttack = 0f;
    private float attackAnimationLength;

    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = false;
    private int collectedEggs = 0;

    private bool canLayEgg = false;
    private bool hasEgg = false;
    private bool layedEgg = false;
    private Nest lastNest; // last nest the player interacted with

    public  BoxCollider2D hitBoxAttack;
    private GameController gameController; // set by the gameController


    [SerializeField] private float cdAttack = 0;
    public AudioSource[] source;

    private float actualCd;

    private Animator animator;
    private bool blocked;

    // Start is called before the first frame update
    void Start()
    {
        blocked = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hitBoxAttack.enabled =false;

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        attackAnimationLength = clips[2].length * 2; // clips[1] is egg crack

    }

    // Update is called once per frame
    void Update()
    {
        if( !blocked)
        {
            Animate();
            Move();
            Attack();
        }
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
        if (!blocked)
        {
            this.gameController.manageDeath(collectedEggs);

            source[2].Play();
            blocked = true;
            spriteRenderer.enabled = false;
            Destroy(gameObject, 1f);
        }
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
            source[4].Play();
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
                if (!layedEgg && canLayEgg && hasEgg)
                {
                    source[0].Play();
                    layedEgg = true;
                    lastNest.LayEgg();
                }
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
        if(!blocked)
            return collectedEggs;
        return 0;
    }

    public void Attack()
    {
        if (Input.GetAxisRaw("Fire1") != 0 && actualCd <= 0) // Fire 1 is "X" on xbox, "Q" on keyboard, left clic on mouse.
        {
            source[1].Play();
            hitBoxAttack.enabled =true;
            Debug.Log("pew pew");
            //TODO ATTACK
            actualCd = cdAttack;
            duringAttack = attackAnimationLength;


            animator.SetBool("isAttacking", true);
            
        }
        if (actualCd > 0)
            actualCd -= Time.deltaTime;
        if (duringAttack > 0)
            duringAttack -= Time.deltaTime;
        else if (hitBoxAttack.enabled)
        {
            animator.SetBool("isAttacking", false);
            hitBoxAttack.enabled = false;
        }
    }


    private void Animate()
    {
        bool isWalking = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;
        bool animIsWalking = animator.GetBool("isWalking");

        if (isWalking != animIsWalking)
        {
            source[3].Play();
            animator.SetBool("isWalking", isWalking);
        } 
        else if (source[3].isPlaying && !isWalking)
        {
            source[3].Stop();
        }
    }

    public void setGameController(GameController pGC)
    {
        this.gameController = pGC;
    }

    public void setBlocked(bool bol)
    {
        blocked = bol;
    }
}
