using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SimpleGun : MonoBehaviour
{
    //Shooting Settings
    //shooting temp for attack speed control
    [SerializeField] public float shootingTemp;

    public float shootingTempCurrent;

    //special attack 
    [SerializeField] public float specialShootingTemp;

    public float specialShootingTempCurrent;

    //ammo
    [SerializeField] public int ammo;

    public int ammoCurrent;

    //Reload
    [SerializeField] public float reloadTime;

    public float reloadTimeCurrent;

    //Bullet 
    [SerializeField] public GameObject bullet;
    [SerializeField] public GameObject specialBullet;

    //Textures
    //Gun image    
    private SpriteRenderer gunImage;

    //Animator
    private Animator gunAnimation;

    //Shooting poing before flip and after;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private GameObject shootingPointFlip;

    [NonSerialized] public GameObject currentShootingPoint;

    //Shooting animation(when u click shoot spawn particle before trunk(ствол)
    [SerializeField] public GameObject shootingAnimation;

    //Player texture for check when we should change shooting point, layer, move up or down and etc;
    [SerializeField] private SpriteRenderer playerSprite;

    [SerializeField] private Animator playerFace;

    //player body to fix 
    [SerializeField] private Rigidbody2D player;

    //Audio
    [SerializeField] public AudioSource shootingSound;
    [SerializeField] public AudioSource outOfAmmoSound;
    [SerializeField] private string entityName;

    public bool isUsedByPlayer = true;

    void Start()
    {
        shootingTemp /= BoosterVariables.gameSpeed;
        specialShootingTemp /= BoosterVariables.gameSpeed;
        reloadTime /= BoosterVariables.gameSpeed;

        gunImage = this.GetComponent<SpriteRenderer>();
        gunAnimation = this.GetComponent<Animator>();

        outOfAmmoSound.volume = BoosterVariables.volume;
        shootingSound.volume = BoosterVariables.volume;
    }

    void FixedUpdate()
    {
        if (isUsedByPlayer)
        {
            //Animator
            gunAnimation.SetFloat("Shooting", shootingTempCurrent);
            gunAnimation.SetFloat("Reload", reloadTimeCurrent);
            playerFace.SetFloat("Shooting", shootingTempCurrent);
            //Shooting cooldown
            if (shootingTempCurrent > 0) shootingTempCurrent -= Time.deltaTime;
            if (specialShootingTempCurrent > 0) specialShootingTempCurrent -= Time.deltaTime;
            if (reloadTimeCurrent > 0) reloadTimeCurrent -= Time.deltaTime;
            
            //ANIMATION
            if (playerSprite.sprite.name.Contains(entityName + "_back"))
            {
                Debug.Log("Test ->");
                gunImage.sortingOrder = 0;
                if (playerSprite.sprite.name == (entityName + "_back"))
                    gunImage.transform.position =
                        transform.position = new Vector3(player.position.x, player.position.y);
                else if (playerSprite.sprite.name == (entityName + "_back_right"))
                    gunImage.transform.position = transform.position =
                        new Vector3(player.position.x + 0.05f, player.position.y - 0.05f);
                else if (playerSprite.sprite.name == (entityName + "_back_left"))
                    gunImage.transform.position = transform.position =
                        new Vector3(player.position.x - 0.05f, player.position.y - 0.05f);
            }
            else
            {
                Debug.Log("Test -> 2");
                gunImage.sortingOrder = 2;
                gunImage.transform.position = new Vector3(player.position.x, player.position.y);
            }

            Vector3 rotate = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateY = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateY);
            if (Quaternion.Euler(0f, 0f, rotateY).z > -0.7 && Quaternion.Euler(0f, 0f, rotateY).z < 0.70)
            {
                gunImage.flipY = false;
                currentShootingPoint = shootingPoint;
            }
            else
            {
                gunImage.flipY = true;
                currentShootingPoint = shootingPointFlip;
            }
        }

        //Debug.Log("-> " + transform.position);
    }

    public void reload()
    {
        if (ammoCurrent < ammo)
        {
            reloadTimeCurrent = reloadTime;
            ammoCurrent = ammo;
        }
    }

    public void shoot()
    {
        if (ammoCurrent > 0 && reloadTimeCurrent <= 0)
        {
            if (shootingTempCurrent <= 0)
            {
                ammoCurrent -= 1;
                shootingTempCurrent = shootingTemp;
                Instantiate(bullet, currentShootingPoint.transform.position, transform.rotation);
                Instantiate(shootingAnimation, currentShootingPoint.transform.position, transform.rotation);
                shootingSound.Play();
            }
        }
        else outOfAmmoSound.Play();
    }

    public void specialShoot()
    {
        Debug.Log("Special shoot! Boom!");
    }
}