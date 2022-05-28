using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleBullet : MonoBehaviour
{
    
    //Living time of bullet
    [SerializeField] private float lifetime;
    private float livingTime;
    //Simple params
    [SerializeField] private float speed;
    private float damage = 10;
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
        if (collision.gameObject.tag != "player")
        {
            if (collision.gameObject.tag == "Enemy")
                collision.gameObject.GetComponent<LivingEntity>().takeDamage((int) damage);
            Instantiate(impactAnimation, this.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        bulletBody.position = Vector2.right * speed * Time.deltaTime;
        livingTime += Time.deltaTime;
        if (livingTime > lifetime) Destroy(gameObject);
        //Move object
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
