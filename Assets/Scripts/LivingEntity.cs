using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    [SerializeField] private int HP;
    [SerializeField] private int HPMax = 20;

    [SerializeField] private Collider2D body;
    [SerializeField] private GameObject deathAnimation;
    [SerializeField] private AudioSource audio;

    void Start()
    {
        HP = HPMax;
        body = this.GetComponent<Collider2D>();
        audio = this.GetComponent<AudioSource>();
        audio.volume = BoosterVariables.volume;
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            if(deathAnimation != null) Instantiate(deathAnimation, this.transform.position, transform.rotation);
            if(audio != null) audio.Play();
            Destroy(this.gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        this.HP -= damage;
    }

    public int getHP()
    {
        return this.HP;
    }
    
    public int getMaxHP()
    {
        return this.HPMax;
    }
    
    
}
