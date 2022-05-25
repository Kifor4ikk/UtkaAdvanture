using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    [SerializeField] private int HP = 20;

    [SerializeField] private Collider2D body;

    void FixedUpdate()
    {
        if (HP <= 0) Destroy(gameObject);
        if (body.isTrigger) ;
    }

    public void takeDamage(int damage)
    {
        this.HP -= damage;
    }
    
}
