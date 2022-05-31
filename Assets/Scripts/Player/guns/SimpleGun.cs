using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SimpleGun : MonoBehaviour
{
    //Shooting Settings
    //shooting temp for attack speed control
    [SerializeField] private float shootingTemp;

    private float shootingTempCurrent;

    //special attack 
    [SerializeField] private float specialShootingTemp;

    private float specialShootingTempCurrent;

    //ammo
    [SerializeField] private int ammo;
    
    private int ammoCurrent;

    //Reload
    [SerializeField] private float reloadTime;
    
    private float reloadTimeCurrent;

    //Bullet 
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject specialBullet;

    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    //Gun image    
    private SpriteRenderer gunImage;

    //Animator
    private Animator gunAnimation;

    //Shooting poing before flip and after;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private GameObject shootingPointFlip;

    [NonSerialized] private GameObject currentShootingPoint;

    //Shooting animation(when u click shoot spawn particle before trunk(ствол)
    [SerializeField] private GameObject shootingAnimation;
    
    
    //Player texture for check when we should change shooting point, layer, move up or down and etc;
    [SerializeField] private SpriteRenderer playerSprite;

    //to draw shooting face animation mb in future
    [SerializeField] private Animator playerFace;

    //player body to change position
    [SerializeField] private Rigidbody2D player;

    //Audio
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioClip outOfAmmoSound;
    [SerializeField] private AudioClip reloadSound;
    
    [SerializeField] private string entityName;

    public bool isUsedByPlayer = false;

    void Start()
    {
        shootingTemp /= BoosterVariables.gameSpeed;
        specialShootingTemp /= BoosterVariables.gameSpeed;
        reloadTime /= BoosterVariables.gameSpeed;
        bulletSpeed /= BoosterVariables.gameSpeed;
        bulletLifeTime /= BoosterVariables.gameSpeed;
        gunImage = this.GetComponent<SpriteRenderer>();
        gunAnimation = this.GetComponent<Animator>();
        
        bullet.GetComponent<SimpleBullet>().setDamage(bulletDamage);
        bullet.GetComponent<SimpleBullet>().setLifeTime(bulletLifeTime);
        bullet.GetComponent<SimpleBullet>().setSpeed(bulletSpeed);
        
        audioSource.volume = BoosterVariables.volume;
        
    }

    void Update()
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
                Debug.Log("Test -> ");
                gunImage.sortingOrder = -3;
                transform.position = new Vector3(player.transform.position.x-0.01f, player.transform.position.y-0.1f);
            }
            else
            {
                gunImage.sortingOrder = 2;
                transform.position = new Vector3(player.transform.position.x-0.03f, player.transform.position.y-0.2f);
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
    }

    public void reload()
    {
        if (ammoCurrent < ammo && shootingTempCurrent <= 0)
        {
            audioSource.clip = reloadSound;
            reloadTimeCurrent = reloadTime;
            ammoCurrent = ammo;
            audioSource.Play();
        }
    }

    public void shoot()
    {
        if (ammoCurrent > 0 && reloadTimeCurrent <= 0)
        {
            if (shootingTempCurrent <= 0)
            {
                audioSource.clip = shootingSound;
                ammoCurrent -= 1;
                shootingTempCurrent = shootingTemp;
                Instantiate(bullet, currentShootingPoint.transform.position, transform.rotation);
                if(shootingAnimation != null) Instantiate(shootingAnimation, currentShootingPoint.transform.position, transform.rotation);
                audioSource.Play();
            }
        }
        else
        {
            if (shootingTempCurrent <= 0)
            {
                //shootingTempCurrent = 0.2f;
                audioSource.clip = outOfAmmoSound;
                audioSource.Play();
            }
            
        }
    }

    public void specialShoot()
    {
        Debug.Log("Special shoot! Boom!");
    }

    public void setPlayerBody(Rigidbody2D rigidbody2D)
    {
        this.player = rigidbody2D;
    }

    public void setPlayerFace(Animator face)
    {
        this.playerFace = face;
    }

    public void setPlayerSprite(SpriteRenderer spriteRenderer)
    {
        this.playerSprite = spriteRenderer;
    }

    public void usedByPlayer(bool used)
    {
        this.isUsedByPlayer = used;
    }

    public float getAmmoMax()
    {
        return this.ammo;
    }

    public float getAmmoCurrent()
    {
        return this.ammoCurrent;
    }
    public bool IsUsedByPlayer => isUsedByPlayer;
}
