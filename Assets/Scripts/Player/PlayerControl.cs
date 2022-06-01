using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Speed
    [SerializeField] private float Speed = 1.8F;
    private static float speedCurrent;

    private float slowDownTimeCurrent;

    //dash
    [SerializeField] private int dashCoolDownMax = 4;
    [SerializeField] private static float dashCoolDownCurrent = 0;
    private static float timeBeforeRegenDash = 0;

    private Vector2 vec;
    private Rigidbody2D playerBody;
    private CircleCollider2D playerTakeBox;

    //Animate part of player
    [SerializeField] private Animator face;
    [SerializeField] private Animator head;
    [SerializeField] private Animator body;

    [SerializeField] private Animator foot;

    //Gun settings
    private static Weapon gun = null;
    private static GameObject gunObject = null;
    private static bool isWeaponNear = false;
    private GameObject gunNear;

    [SerializeField] private SpriteRenderer hand;
    private float timeToDrawHand;
    
    //Life settings
    public int healh;
    public int isHelmet;
    public int isArmor;
    [SerializeField] private float tookDamageTime;
    public float tookDamageCurrent;
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerTakeBox = GetComponent<CircleCollider2D>();
        speedCurrent = Speed;
        tookDamageCurrent = 0;
        healh = 3;
        isHelmet = 1;
        isArmor = 1;
    }

    void Update()
    {
        
        if (tookDamageCurrent >= 0) tookDamageCurrent -= Time.deltaTime;
        if(isHelmet > 0) head.SetBool("isArmored", true );
        else head.SetBool("isArmored", false );
        if(isArmor > 0) body.SetBool("isArmored", true );
        else body.SetBool("isArmored", false );
        if (healh <= 0) Destroy(this.gameObject);
        //Set animation
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");

        face.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - playerBody.position.x);
        face.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - playerBody.position.y);
        face.SetFloat("Speed", speedCurrent);

        head.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - playerBody.position.x);
        head.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - playerBody.position.y);
        head.SetFloat("Speed", speedCurrent);

        body.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - playerBody.position.x);
        body.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - playerBody.position.y);
        body.SetFloat("Speed", speedCurrent);

        foot.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - playerBody.position.x);
        foot.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - playerBody.position.y);
        foot.SetFloat("Speed", vec.magnitude);
        
        //Drop/Get weapon
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isWeaponNear)
            {
                if (gunObject != null)
                {
                    gunObject.GetComponent<Weapon>().IsUsedByPlayer = false;
                    Instantiate(gunObject, playerBody.transform.position, transform.rotation);
                    Destroy(gunObject);
                }
                
                gunObject = gunNear;
                gunObject.GetComponent<Weapon>().IsUsedByPlayer = true;
                gunObject.GetComponent<Weapon>().PlayerFace = face;
                gunObject.GetComponent<Weapon>().PlayerBody = playerBody;
                gunObject.GetComponent<Weapon>().PlayerSprite = (body.GetComponent<SpriteRenderer>());
                
                gun = gunObject.GetComponent<Weapon>();
                gunNear = null;
            }
            
        }
        //Shooting
        if (gunObject != null)
        {
            if (Input.GetMouseButton(0))
            {
                gun.shoot();
                slowDownTimeCurrent = 0.1f;
            }

            if (Input.GetMouseButton(1))
            {
                gun.specialShoot();
                slowDownTimeCurrent = 0.2f;
            }

            if (gun is SimpleGun)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    gun.GetComponent<SimpleGun>().reload();
                    slowDownTimeCurrent = 2f;
                }
            }
        }
        //SpeedChange
        speedCurrent = Speed;
        if (Input.GetKey(KeyCode.LeftShift) && dashCoolDownCurrent > 0 || slowDownTimeCurrent > 0)
        {
            if (slowDownTimeCurrent > 0)
            {
                speedCurrent = Speed / 1.5F;
                slowDownTimeCurrent -= Time.deltaTime;
            }
            //Dash
            if (Input.GetKey(KeyCode.LeftShift) && dashCoolDownCurrent > 0)
            {
                timeBeforeRegenDash = 2;
                dashCoolDownCurrent -= Time.deltaTime;
                speedCurrent = Speed * 1.5f;
            }
        }
        else { speedCurrent = Speed; }
        
        if (timeBeforeRegenDash <= 0 && dashCoolDownCurrent < dashCoolDownMax) dashCoolDownCurrent += Time.deltaTime;
        if (timeBeforeRegenDash > 0) timeBeforeRegenDash -= Time.deltaTime;
        //Weapon near check
        if (timeToDrawHand > 0) { hand.enabled = true; timeToDrawHand -= Time.deltaTime; }
        else hand.enabled = false;

        
    }    
    
    void OnCollisionEnter2D(Collision2D collider2D)
    {
        Debug.Log("SALO CONTACT " + collider2D);
        if (collider2D.gameObject.tag == "EnemyBullet")
        {
            if (tookDamageCurrent <= 0)
            {
                slowDownTimeCurrent = 1F;
                if (isHelmet > 0) isHelmet -= 1;
                else
                {
                    if (isArmor > 0) isArmor -= 1;
                    else
                    {
                        healh -= 1;
                    }
                }
            }
            Destroy(collider2D.gameObject);
            tookDamageCurrent = tookDamageTime;
        }
        
    }
    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "weapon" && !trigger.gameObject.GetComponent<Weapon>().IsUsedByPlayer)
        {
            gunNear = trigger.gameObject;
            isWeaponNear = true;
            timeToDrawHand = 0.1f;
        }
        else
        {
            isWeaponNear = false;
        }
    }
    
    //Move player here;
    private void FixedUpdate()
    {
        playerBody.MovePosition(playerBody.position + vec * speedCurrent * Time.fixedDeltaTime);
    }
    
    
    //GETTER AND SETTERS
    public static float getDashCooldownCurrent()
    {
        return dashCoolDownCurrent;
    }

    public static float getSpeedCurrent()
    {
        return speedCurrent;
    }

    public static bool IsWeaponNear()
    {
        return isWeaponNear;
    }

    public static Weapon getWeapon()
    {
        return gun;
    }

    public float SlowDownTimeCurrent
    {
        get => slowDownTimeCurrent;
        set => slowDownTimeCurrent = value;
    }
}