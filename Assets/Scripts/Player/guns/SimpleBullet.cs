using System;
using System.Collections;
using System.Collections.Generic;
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

    //Impact animation 
    [SerializeReference] private GameObject impactAnimation;

    void Start()
    {
        speed *= BoosterVariables.gameSpeed;
        bulletBody = this.GetComponent<Rigidbody2D>();
        bulletHitBox = this.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision!");

        if (collision.gameObject.tag == "Enemy") collision.gameObject.GetComponent<LivingEntity>().takeDamage((int) damage);

        
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
}