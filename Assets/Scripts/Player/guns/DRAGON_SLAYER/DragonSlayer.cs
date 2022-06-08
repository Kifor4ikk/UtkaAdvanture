using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class DragonSlayer : SimpleMeleeWeapon
{

    [SerializeField] private Animator _animator;
    void Start()
    {
        setParams();
        GunAnimation = _animator;
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
    
    
}
