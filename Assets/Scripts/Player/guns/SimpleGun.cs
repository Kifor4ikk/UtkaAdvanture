using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public abstract class SimpleGun : Weapon
{
    //ammo
    [SerializeField] private int ammo;
    private int ammoCurrent;
    //Reload
    [SerializeField] private float reloadTime;
    private float reloadTimeCurrent;
    //Bullet 
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject specialBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    //accuracy from 0 to 180
    [SerializeField] private float accuracy;

    //Shooting poing before flip and after;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private GameObject shootingPointFlip;
    [NonSerialized] private GameObject currentShootingPoint;
    //Shooting animation(when u click shoot spawn particle before trunk(ствол)
    //Audio
    [SerializeField] private AudioClip outOfAmmoSound;
    [SerializeField] private AudioClip reloadSound;

    
    private Vector3 rotateSimpleGun;

    private float fixX;
    private float fixY;
    void Start()
    {
        setParams();
        ShootingTemp /= BoosterVariables.gameSpeed;
        SpecialShootingTemp /= BoosterVariables.gameSpeed;
        reloadTime /= BoosterVariables.gameSpeed;
        bulletSpeed /= BoosterVariables.gameSpeed;
        bulletLifeTime /= BoosterVariables.gameSpeed;
        GunImage = this.GetComponent<SpriteRenderer>();
        GunAnimation = this.GetComponent<Animator>();
        if (accuracy > 180) accuracy = 180;
        if (accuracy < 0) accuracy = 0;
        accuracy /= 2;
        bullet.GetComponent<SimpleBullet>().setDamage(Damage);
        bullet.GetComponent<SimpleBullet>().setLifeTime(bulletLifeTime);
        bullet.GetComponent<SimpleBullet>().setSpeed(bulletSpeed);
    }

    void Update()
    {
        if (IsUsedByPlayer)
        {
            //Animator
            GunAnimation.SetFloat("Shooting", ShootingTempCurrent);
            GunAnimation.SetFloat("Reload", reloadTimeCurrent);
            PlayerFace.SetFloat("Shooting", ShootingTempCurrent);
            //Shooting cooldown
            if (HandsImage != null && reloadTimeCurrent < 0) HandsImage.enabled = true;
            else HandsImage.enabled = false;
            if (ShootingTempCurrent > 0) ShootingTempCurrent -= Time.deltaTime;
            if (SpecialShootingTempCurrent > 0) SpecialShootingTempCurrent -= Time.deltaTime;
            if (reloadTimeCurrent > 0) reloadTimeCurrent -= Time.deltaTime;

            fixX = 0.0f;
            fixY = -0.2f;
            //ANIMATION
            if (PlayerSprite.sprite.name.Contains(EntityName + "_back"))
            {
                GunImage.sortingOrder = -3;
                if (HandsImage != null) HandsImage.sortingOrder = -3;
                if (PlayerSprite.sprite.name.Contains("left")) fixX = -0.2f;
                if (PlayerSprite.sprite.name.Contains("right")) fixX = +0.2f;
                
                    fixY = -0.08f;
                if (PlayerSprite.sprite.name.Contains("player_back_body")) fixY = +0.12f;
                transform.position = new Vector3(PlayerBody.transform.position.x + fixX, PlayerBody.transform.position.y + fixY);
            }
            else
            {
                GunImage.sortingOrder = 2;
                if (HandsImage != null) HandsImage.sortingOrder = 4;
                if (PlayerSprite.sprite.name.Contains("left")) fixX = -0.05f;
                if (PlayerSprite.sprite.name.Contains("right")) fixX = +0.05f;
                transform.position = new Vector3(PlayerBody.transform.position.x + fixX, PlayerBody.transform.position.y + fixY);
            }

            rotateSimpleGun = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateSimpleGunY = Mathf.Atan2(rotateSimpleGun.y, rotateSimpleGun.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateSimpleGunY);

            if (Mathf.Atan2(rotateSimpleGun.y, rotateSimpleGun.x) > 1.57f || Mathf.Atan2(rotateSimpleGun.y, rotateSimpleGun.x) < -1.57f)
            {
                
                GunImage.flipY = true;
                if (HandsImage != null) HandsImage.flipY = true;
                currentShootingPoint = shootingPointFlip;
                
            }
            else
            {
                GunImage.flipY = false;
                if (HandsImage != null) HandsImage.flipY = false;
                currentShootingPoint = shootingPoint;
            }
        }
        else
        {
            if (HandsImage != null) HandsImage.enabled = false;
        }
    }

    public void reload()
    {
        if (ammoCurrent < ammo && ShootingTempCurrent <= 0)
        {
            AudioSource.clip = reloadSound;
            reloadTimeCurrent = reloadTime;
            ammoCurrent = ammo;
            AudioSource.Play();
        }
    }

    public override void shoot()
    {
        if (ammoCurrent > 0 && reloadTimeCurrent <= 0)
        {
            if (ShootingTempCurrent <= 0)
            {
                AudioSource.clip = ShootingSound;
                ammoCurrent -= 1;
                ShootingTempCurrent = ShootingTemp;
        
                float rnd = ((new Random((uint)DateTime.UtcNow.Millisecond * (uint)DateTime.UtcNow.Millisecond)).NextFloat(2)-1);
                float rotateWithSpread = (rnd * accuracy);
                float angle = 0f;
                Vector3 axis;
                //Получение оси и угла поворота
                transform.rotation.ToAngleAxis(out angle, out axis);
                Instantiate(bullet, currentShootingPoint.transform.position, Quaternion.AngleAxis((angle + rotateWithSpread), axis));
                AudioSource.Play();
            }
        }
        else
        {
            if (ShootingTempCurrent <= 0 && reloadTimeCurrent <= 0)
            {
                //ShootingTempCurrent = 0.2f;
                AudioSource.clip = outOfAmmoSound;
                AudioSource.Play();
            }
            
        }
    }

    public override  void specialShoot()
    {
        Debug.Log("Special shoot! Boom!");
    }

    public int Ammo
    {
        get => ammo;
        set => ammo = value;
    }

    public int AmmoCurrent
    {
        get => ammoCurrent;
        set => ammoCurrent = value;
    }

    public float ReloadTime
    {
        get => reloadTime;
        set => reloadTime = value;
    }

    public float ReloadTimeCurrent
    {
        get => reloadTimeCurrent;
        set => reloadTimeCurrent = value;
    }

    public GameObject Bullet
    {
        get => bullet;
        set => bullet = value;
    }

    public GameObject SpecialBullet
    {
        get => specialBullet;
        set => specialBullet = value;
    }

    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    public float BulletLifeTime
    {
        get => bulletLifeTime;
        set => bulletLifeTime = value;
    }

    public float Accuracy
    {
        get => accuracy;
        set => accuracy = value;
    }

    public GameObject ShootingPoint
    {
        get => shootingPoint;
        set => shootingPoint = value;
    }

    public GameObject ShootingPointFlip
    {
        get => shootingPointFlip;
        set => shootingPointFlip = value;
    }

    public GameObject CurrentShootingPoint
    {
        get => currentShootingPoint;
        set => currentShootingPoint = value;
    }

    public AudioClip OutOfAmmoSound
    {
        get => outOfAmmoSound;
        set => outOfAmmoSound = value;
    }

    public AudioClip ReloadSound
    {
        get => reloadSound;
        set => reloadSound = value;
    }

    public Vector3 RotateSimpleGun
    {
        get => rotateSimpleGun;
        set => rotateSimpleGun = value;
    }
}
