﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hatch()
    {
        //TODO: play animation

        // destroy egg shell after animation
        float animationLength = 0f; // TODO: replace 0 with animation length
        Destroy(gameObject, animationLength);
    }
}