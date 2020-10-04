using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource onionDed;

    public void playOnionDeath()
    {
        onionDed.Play();
    }
}
