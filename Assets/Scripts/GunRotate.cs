using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    
    //player and if need fix rotation by var offset
    [SerializeField] private float offset;
    [SerializeField] private Rigidbody2D player;
    
    //gun image
    [SerializeField] private SpriteRenderer image;
    
    //bulet object and bullet start shooting pos
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    //to transform image
    private Transform playerTransform;
    private float correct = 0.07F;

    [SerializeField] private float ShootingTemp = 0.3F;
    private float ShootingTempCurrent = 0.0F;
    [SerializeField] private int clipSize = 30;
    void Start()
    {
        image.sortingOrder = 2;
    }
    
    void Update()
    {
        //Rotation
        playerTransform = player.transform;
        Vector3 rotate = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateY = Mathf.Atan2(rotate.y,rotate.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f ,rotateY + offset );
        //Layer order and fixed up down position
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y-0.07F, playerTransform.position.z);
        if (
            (Quaternion.Euler(0f, 0f, rotateY + offset).z < 0.96 && Quaternion.Euler(0f, 0f, rotateY + offset).z > 0.20) &&
            (Quaternion.Euler(0f, 0f, rotateY + offset).w < 0.96 && Quaternion.Euler(0f, 0f, rotateY + offset).w > 0.20)
        ) { image.sortingOrder = 0; correct = 0.03F; }
        else { image.sortingOrder = 2; correct = 0.07f; }
        //Layers
        if (Quaternion.Euler(0f, 0f, rotateY + offset).z > -0.7 && Quaternion.Euler(0f, 0f, rotateY + offset).z < 0.70) image.flipY = false;
        else image.flipY = true;
        //Just move with player
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - correct,
            playerTransform.position.z);
        //Shooting
        if (Input.GetMouseButton(0) && ShootingTempCurrent <= 0)
        {
            //create bullet with pos and our gun rotation
            Instantiate(bullet, bulletPos.position, transform.rotation);
            ShootingTempCurrent = ShootingTemp;
        } else if (ShootingTempCurrent > 0) ShootingTempCurrent -= Time.deltaTime;

    }
}
