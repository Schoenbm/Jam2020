using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource onionDed;
    public AudioSource chickenDed;

    public void playOnionDeath()
    {
        onionDed.Play();
    }

    public void playChickenDed()
    {
        chickenDed.Play();
    }
}
