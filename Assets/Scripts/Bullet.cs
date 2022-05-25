using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float distance;
    [SerializeField] private float damage;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);
        //Check collision
        if (hitInfo.collider != null)
        {    
            Debug.Log("COLLISION! " + hitInfo.collider);
            //if collision enemy damage enemy
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<LivingEntity>().takeDamage((int)damage);
            }
            //after collision delete;
            Destroy(gameObject);
        }
        //Move object
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
