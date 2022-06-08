using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : LifeTime
{

    private AudioSource audio;
    
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        audio.volume = BoosterVariables.volume;
        audio.Play();
    }
}
