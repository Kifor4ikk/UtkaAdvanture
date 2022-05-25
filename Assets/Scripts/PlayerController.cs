using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 2.0F;
    private Vector2 vec;
    private Rigidbody2D playerBody;
    [SerializeField] private Animator animator;
    
    [SerializeField] private int dashCoolDownmax = 4;
    [SerializeField] private static float dashCoolDownCurrent = 0;
    [SerializeField] private float dashForce = 1.05F;
    private bool isDashCooldown = false;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vec.x = Input.GetAxisRaw("Horizontal");   
        vec.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", Camera.main.ScreenToWorldPoint(Input.mousePosition).x - playerBody.position.x);
        animator.SetFloat("Vertical", Camera.main.ScreenToWorldPoint(Input.mousePosition).y - playerBody.position.y);
        animator.SetFloat("Speed", vec.sqrMagnitude);
        

    }

    private void FixedUpdate() {
        playerBody.MovePosition(playerBody.position + vec * Speed * Time.fixedDeltaTime);
        
        if (Input.GetKey(KeyCode.LeftShift) && !isDashCooldown) {
            isDashCooldown = true;
            dashCoolDownCurrent = 4;
        }

        if (isDashCooldown)
        {
            if(dashCoolDownCurrent > 3.93) playerBody.MovePosition(playerBody.position + vec * Speed * Time.fixedDeltaTime * dashForce);
            if(dashCoolDownCurrent > 0) dashCoolDownCurrent -= Time.deltaTime;
            if (dashCoolDownCurrent < 0)
            {
                dashCoolDownCurrent = 0;
                isDashCooldown = false;
            }
        }
    }

    public static float getDashCooldownCurrent()
    {
        return dashCoolDownCurrent;
    }
}
