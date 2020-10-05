using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float animationLength =2f;
    public AudioSource eggSe;

    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        animationLength = clips[1].length; // clips[1] is egg crack
    }

    public void Hatch()
    {
        //TODO: play animation
        animator.SetBool("isCracking", true);

        // destroy egg shell after animation
        eggSe.Play();
        Destroy(gameObject,  animationLength);
    }
}
