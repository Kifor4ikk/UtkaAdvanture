using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Params to check speed attack
    [SerializeField] private float shootingTemp;
    private float shootingTempCurrent;
        //special attack 
    [SerializeField] private float specialShootingTemp;
    private float specialShootingTempCurrent;
    //Images just textures   
    private SpriteRenderer gunImage;
    [SerializeField] private SpriteRenderer handsImage;
    //Animator
    private Animator gunAnimation;
    //Player params to change animation/layer sorting/flip image and etc
        //Player texture for check when we should change shooting point, layer, move up or down and etc;
    [SerializeField] private SpriteRenderer playerSprite;
        //to draw shooting face animation mb in future
    [SerializeField] private Animator playerFace;
        //player body to change position
    [SerializeField] private Rigidbody2D player;
    
    //Audio
    [SerializeField] private AudioSource audioSource;
        //just attack sound when u click left mouse button
    [SerializeField] private AudioClip shootingSound;
    //Using entity (tag before sprite (example -> (player_back_body_armored) in this case tag is player); 
    [SerializeField] private string entityName;
    //isUsedByPlayer need to check is used by player genius!;
    private bool isUsedByPlayer = false;

    //function get all(which can) params from weapon
    public void setParams()
    {
        gunImage = this.GetComponent<SpriteRenderer>();
        gunAnimation = this.GetComponent<Animator>();
        this.tag = "weapon";
    }
    //Dont need in melee
    public abstract void reload();
    public abstract void shoot();
    public abstract void specialShoot();

    public float ShootingTemp
    {
        get => shootingTemp;
        set => shootingTemp = value;
    }

    public float ShootingTempCurrent
    {
        get => shootingTempCurrent;
        set => shootingTempCurrent = value;
    }

    public float SpecialShootingTemp
    {
        get => specialShootingTemp;
        set => specialShootingTemp = value;
    }

    public float SpecialShootingTempCurrent
    {
        get => specialShootingTempCurrent;
        set => specialShootingTempCurrent = value;
    }

    public SpriteRenderer GunImage
    {
        get => gunImage;
        set => gunImage = value;
    }

    public SpriteRenderer HandsImage
    {
        get => handsImage;
        set => handsImage = value;
    }

    public Animator GunAnimation
    {
        get => gunAnimation;
        set => gunAnimation = value;
    }

    public SpriteRenderer PlayerSprite
    {
        get => playerSprite;
        set => playerSprite = value;
    }

    public Animator PlayerFace
    {
        get => playerFace;
        set => playerFace = value;
    }

    public Rigidbody2D PlayerBody
    {
        get => player;
        set => player = value;
    }

    public AudioSource AudioSource
    {
        get => audioSource;
        set => audioSource = value;
    }

    public AudioClip ShootingSound
    {
        get => shootingSound;
        set => shootingSound = value;
    }

    public string EntityName
    {
        get => entityName;
        set => entityName = value;
    }

    public bool IsUsedByPlayer
    {
        get => isUsedByPlayer;
        set => isUsedByPlayer = value;
    }
}
