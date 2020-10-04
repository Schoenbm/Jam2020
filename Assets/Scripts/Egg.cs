using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float animationLength =2f;
    // Start is called before the first frame update
    void Start()
    {
        animationLength = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hatch()
    {
        //TODO: play animation

        // destroy egg shell after animation
        Destroy(gameObject, animationLength);
    }
}
