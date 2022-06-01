using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class DragonSlayer : SimpleMeleeWeapon
{

    void Start()
    {
        setParams();
    }
    
    void Update()
    {
        base.animationSimple();
        //Update animation
        GunAnimation.SetFloat("Shooting", ShootingTempCurrent);
        if(ShootingTempCurrent > 0) ShootingTempCurrent -= Time.deltaTime;
    }
    
    public override void shoot()
    {
        //action  DADADAD
        if (ShootingTempCurrent <= 0)
        {

            ShootingTempCurrent = ShootingTemp;
        }
    }

    public override void specialShoot()
    {
    }



    // void OnTriggerStay2D(Collider2D trigger)
    // {
    //     Debug.Log("Collision!");
    //     if (trigger.gameObject.tag == "Enemy") trigger.gameObject.GetComponent<LivingEntity>().takeDamage((int) Damage);
    // }
    
}
