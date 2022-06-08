using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    //Damage
    [SerializeField] private float damage;
    //Params to check speed attack
        //in seconds
    [SerializeField] private float shootingTemp;
    
    private float shootingTempCurrent;

    //special attack 
    [SerializeField] private float specialShootingTemp;

    private float specialShootingTempCurrent;

    //Images just textures   
    private SpriteRenderer gunImage;

    [SerializeField] private SpriteRenderer handsImage;

    //Animator
    [SerializeField] private Animator gunAnimation;
    
    //HitBoxes and body
    private Rigidbody2D gunBody;
    private Collider2D gunHitBox;
    
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

    private Vector3 rotate;

    //function get all(which can) params from weapon
    public void setParams()
    {
        gunImage = this.GetComponent<SpriteRenderer>();
        gunAnimation = this.GetComponent<Animator>();
        gunBody = this.GetComponent<Rigidbody2D>();
        gunHitBox = this.GetComponent<Collider2D>();
        gunBody.gravityScale = 0.0f;
        
        shootingTempCurrent = 0;
        this.tag = "weapon";
        audioSource.volume = BoosterVariables.volume;
    }

    //Dont need in melee
    public abstract void shoot();
    public abstract void specialShoot();

    public void animationSimple()
    {
        //ANIMATION
        if (IsUsedByPlayer)
        {
            if (playerSprite.sprite.name.Contains(entityName + "_back"))
            {
                Debug.Log("Test -> ");
                gunImage.sortingOrder = -3;
                if (handsImage != null) handsImage.sortingOrder = -3;
                transform.position = new Vector3(PlayerBody.transform.position.x - 0.01f,
                    PlayerBody.transform.position.y - 0.1f);
            }
            else
            {
                GunImage.sortingOrder = 2;
                if (HandsImage != null) HandsImage.sortingOrder = 4;
                transform.position = new Vector3(PlayerBody.transform.position.x - 0.03f,
                    PlayerBody.transform.position.y - 0.2f);
            }

            rotate = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateY = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateY);

            if (Mathf.Atan2(rotate.y, rotate.x) > 1.57f || Mathf.Atan2(rotate.y, rotate.x) < -1.57f)
            {
                GunImage.flipY = true;
            }
            else
            {
                GunImage.flipY = false;
            }
        }
        else
        {
            if (HandsImage != null) HandsImage.enabled = false;
        }
    }


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

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public Vector3 Rotate
    {
        get => rotate;
        set => rotate = value;
    }
}