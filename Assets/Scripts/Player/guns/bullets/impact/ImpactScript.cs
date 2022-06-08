using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : LifeTime
{
    
    [SerializeField] private AudioClip bodyImpact;
    [SerializeField] private AudioClip wallImpact;
    private AudioSource audio;
    [SerializeField] private bool isImpactInLiving;
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        audio.volume = BoosterVariables.volume;
        audio.clip = wallImpact;
        if(isImpactInLiving) audio.clip = bodyImpact;
        audio.Play();
    }
    
    public bool IsImpactInLiving
    {
        set => isImpactInLiving = value;
    }
}
