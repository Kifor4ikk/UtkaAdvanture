using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage;
    
    [SerializeField] private Rigidbody2D bulletBody;
    [SerializeField] private Collider2D bulletHitBox;

    [SerializeReference] private GameObject impactAnimation;
    
    private float livingTime;

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") collision.gameObject.GetComponent<LivingEntity>().takeDamage((int)damage);
        
        Instantiate(impactAnimation, this.transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void Update()
    {
        bulletBody.position = Vector2.right * speed * Time.deltaTime;
        livingTime += Time.deltaTime;
        if (livingTime > lifetime) Destroy(gameObject);
        //Move object
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
