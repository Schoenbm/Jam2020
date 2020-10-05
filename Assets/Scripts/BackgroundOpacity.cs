using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundOpacity : MonoBehaviour
{
    public Image background;
    [SerializeField] private float alphaInstructions = 1;
    [SerializeField] private float alpha = 0.75f;

    public void goInstructions()
    {
        var tempColor = background.color;

        tempColor.a = alphaInstructions;
        background.color = tempColor;
    }

    public void goMenu()
    {
        var tempColor = background.color;

        
        tempColor.a = alpha;
        background.color = tempColor;
    }

}
