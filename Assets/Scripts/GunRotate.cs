using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Random = System.Random;

public class GunRotate : MonoBehaviour
{
    
    //player and if need fix rotation by var offset
    [SerializeField] private Rigidbody2D player;
    
    //gun image
    [SerializeField] private SpriteRenderer image;
        //need to set layer
    [SerializeField] private SpriteRenderer playerSprite;
    
    //bulet object and bullet start shooting pos
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private Transform bulletPosFix;
    private Transform currentBulletPos;
    [SerializeField] private AudioSource bulletSound;
    
    //animation shooting
    [SerializeField] private GameObject shootingImage;
    
    //to transform image
    private Transform playerTransform;
    private float correctY = 0.1F;
    private float correctX = 0.07F;

    [SerializeField] private float ShootingTemp = 0.3F;
    private float ShootingTempCurrent = 0.0F;
    [SerializeField] private int clipSize = 30;
    void Start()
    {
        image.sortingOrder = 2;
    }
    
    void Update()
    {
        
        // layer switch bone
        if (playerSprite.sprite.name.Contains("char_back")) 
        {
            image.sortingOrder = 0;
            if(playerSprite.sprite.name == ("char_back")) image.transform.position = transform.position = new Vector3(player.position.x, player.position.y);
            else if(playerSprite.sprite.name.Contains("char_back_right")) image.transform.position = transform.position = new Vector3(player.position.x + 0.05f, player.position.y - 0.05f);
            else if(playerSprite.sprite.name.Contains("char_back_left")) image.transform.position = transform.position = new Vector3(player.position.x - 0.05f, player.position.y - 0.05f);
        }
        else
        {
            image.sortingOrder = 2;
            image.transform.position = transform.position = new Vector3(player.position.x, player.position.y-0.25f);
        }

        Vector3 rotate = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateY = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f ,rotateY);

        if (Quaternion.Euler(0f, 0f, rotateY).z > -0.7 && Quaternion.Euler(0f, 0f, rotateY).z < 0.70)
        {
            image.flipY = false;
            currentBulletPos = bulletPos;
        }
        else
        {
            image.flipY = true;
            currentBulletPos = bulletPosFix;
        }
        
        
        //Shooting
        if (Input.GetMouseButton(0) && ShootingTempCurrent <= 0)
        {
            //create bullet with pos and our gun rotation
            Instantiate(bullet, currentBulletPos.position, transform.rotation);
            Instantiate(shootingImage, currentBulletPos.position, transform.rotation);
            bulletSound.Play();
            ShootingTempCurrent = ShootingTemp;
        } else if (ShootingTempCurrent > 0) ShootingTempCurrent -= Time.deltaTime;

    }
}
