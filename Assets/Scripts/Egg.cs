using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float animationLength =2f;


    public void Hatch()
    {
        //TODO: play animation

        // destroy egg shell after animation
        
        Destroy(gameObject,  animationLength);
    }
}
