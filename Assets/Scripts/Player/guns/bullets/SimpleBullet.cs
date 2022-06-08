using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifeTime = 10;

    private float lifeTimeCurrent = 0;


    //Bullet body to move it and check hit
    private Rigidbody2D bulletBody;
    private Collider2D bulletHitBox;
    
    //Who should took damage TAG
    [SerializeField] private List<String> damagableTag;
    //Impact animation 
    [SerializeReference] private GameObject impactAnimation;

    void Start()
    {
        speed *= BoosterVariables.gameSpeed;
        bulletBody = this.GetComponent<Rigidbody2D>();
        bulletHitBox = this.GetComponent<Collider2D>();
        impactAnimation.GetComponent<ImpactScript>().IsImpactInLiving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().enabled = true;
        
        if (damagableTag.Contains(collision.gameObject.tag))
        {
            if (collision.gameObject.tag == "Player")
            {
                impactAnimation.GetComponent<ImpactScript>().IsImpactInLiving = true;
                collision.gameObject.GetComponent<PlayerControl>().damagePlayer((int) damage);
            }

            if (collision.gameObject.tag == "Enemy")
            {
                impactAnimation.GetComponent<ImpactScript>().IsImpactInLiving = true;
                collision.gameObject.GetComponent<LivingEntity>().takeDamage((int) damage);
            }
        }

        if(impactAnimation != null) Instantiate(impactAnimation, this.transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        bulletBody.position = Vector2.right * speed * Time.deltaTime;
        lifeTimeCurrent += Time.deltaTime;
        if (lifeTimeCurrent > lifeTime) Destroy(gameObject);
        //Move object
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void setDamage(float dmg)
    {
        this.damage = dmg;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setLifeTime(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public GameObject ImpactAnimation
    {
        get => impactAnimation;
        set => impactAnimation = value;
    }

    public float Damage
    {
        get => damage;
    }
}