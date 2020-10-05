using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource onionDed;
    public AudioSource chickenDed;
    public AudioSource winMelody;
    public AudioSource gameOverMelody;
    public AudioSource mainTheme;

    public void playOnionDeath()
    {
        onionDed.Play();
    }

    public void playChickenDed()
    {
        chickenDed.Play();
    }

    public void playWin()
    {
        mainTheme.Stop();
        winMelody.Play();
    }

    public void playLoose()
    {
        mainTheme.Stop();
        gameOverMelody.Play();
    }
}
