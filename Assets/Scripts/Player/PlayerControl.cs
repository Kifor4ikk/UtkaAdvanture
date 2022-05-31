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
    private static SimpleGun gun = null;
    private static GameObject gunObject = null;
    private static bool isWeaponNear = false;

    private GameObject gunNear;

    [SerializeField] private SpriteRenderer hand;
    private float timeToDrawHand;
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerTakeBox = GetComponent<CircleCollider2D>();
        speedCurrent = Speed;
    }

    void Update()
    {
        
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Pressed Z");
            if (isWeaponNear)
            {
                if (gunObject != null)
                {
                    gunObject.GetComponent<SimpleGun>().usedByPlayer(false);
                    Instantiate(gunObject, playerBody.transform.position, transform.rotation);
                    Destroy(gunObject);
                }
                
                gunObject = gunNear;
                gunObject.GetComponent<SimpleGun>().usedByPlayer(true);
                
                gunObject.GetComponent<SimpleGun>().setPlayerFace(face);
                gunObject.GetComponent<SimpleGun>().setPlayerBody(playerBody);
                gunObject.GetComponent<SimpleGun>().setPlayerSprite(body.GetComponent<SpriteRenderer>());
                
                gun = gunObject.GetComponent<SimpleGun>();
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

            if (Input.GetKey(KeyCode.R))
            {
                gun.reload();
                slowDownTimeCurrent = 2f;
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
    
    
    void OnTriggerStay2D (Collider2D trigger)
    {
        if (trigger.gameObject.tag == "weapon" && !trigger.gameObject.GetComponent<SimpleGun>().IsUsedByPlayer)
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

    public static SimpleGun getWeapon()
    {
        return gun;
    }
}