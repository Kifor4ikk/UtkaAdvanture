using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemyBrains : MonoBehaviour
{
    //Need to get position and shoot in this peace of player!
    //player body
    [SerializeField] private GameObject player;
    private Rigidbody2D playerBody;
    [SerializeField] private float brainActivatorDistance = 3;

    //Enemy params

    [SerializeField] private float damage;

    //shooting temp
    [SerializeField] private float shootingTemp;

    //CURRENT JUST FOR control temp attack
    private float currentShootingTemp;

    //accuracy
    [SerializeField] private float accuracy;

    //BULLET TO SET PARAMS
    [SerializeField] private GameObject bullet;

    //Speed for bullet
    [SerializeField] private float bulletSpeed;

    [SerializeField] private float reloadTime;
    [SerializeField] private float reloadTimeCurrent;

    [SerializeField] private float ammo;

    [SerializeField] private float ammoCurrent;

    //Enemy move speed
    [SerializeField] private float speed;
    private float speedCurrent;

    [SerializeField] private GameObject shootingPos;

    //Enemy collider
    private Collider2D collider;

    //Enemy body
    private Rigidbody2D body;


    private float currentSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerBody = player.GetComponent<Rigidbody2D>();
        bullet.GetComponent<SimpleBullet>().setDamage(damage);
        bullet.GetComponent<SimpleBullet>().setSpeed(bulletSpeed);
        if (accuracy > 180) accuracy = 180;
        if (accuracy < 0) accuracy = 0;
        ammoCurrent = ammo;
    }

    private float tempX;
    private float tempY;

    void FixedUpdate()
    {
        //Update variables
        shooting();
        playerStalkingMove();
        playerStalking();
    }

    void shooting()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(shootingPos.transform.position, shootingPos.transform.right);

        if (currentShootingTemp > 0) currentShootingTemp -= Time.fixedDeltaTime;

        if (currentShootingTemp <= 0 && ammoCurrent > 0 && hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.tag == "Player")
            {
                ammoCurrent -= 1;
                currentShootingTemp = shootingTemp;
                float rnd = ((new Random((uint) DateTime.UtcNow.Millisecond * (uint) DateTime.UtcNow.Millisecond)).NextFloat(2) - 1);
                float rotateWithSpread = (rnd * accuracy);
                float angle = 0f;
                Vector3 axis;
                //Получение оси и угла поворота
                transform.rotation.ToAngleAxis(out angle, out axis);

                Instantiate(bullet, shootingPos.transform.position,
                    Quaternion.AngleAxis((angle + rotateWithSpread), axis));
            }
        }

        if (ammoCurrent <= 0 && reloadTimeCurrent < reloadTime) reloadTimeCurrent += Time.fixedDeltaTime;

        if (reloadTimeCurrent >= reloadTime)
        {
            ammoCurrent = ammo;
            reloadTimeCurrent = 0;
        }
    }

    void playerStalking()
    {
        float rotateSimpleGunY = Mathf.Atan2((player.transform.position - transform.position).y,
            (player.transform.position - transform.position).x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateSimpleGunY);
    }

    void playerStalkingMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

    void justMoving()
    {

    }

}