using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Berette : SimpleGun
{

    [SerializeField] private GameObject shooting_pos_3;
    private bool alternativeShoot;
    
    void Start(){
        setParams();
        alternativeShoot = false;
    }
    public override void shoot()
    {
        if (AmmoCurrent > 0 && ReloadTimeCurrent <= 0)
        {
            if (ShootingTempCurrent <= 0)
            {
                AudioSource.clip = ShootingSound;
                AmmoCurrent -= 1;
                ShootingTempCurrent = ShootingTemp;

                float rnd = ((new Random((uint)DateTime.UtcNow.Millisecond * (uint)DateTime.UtcNow.Millisecond)).NextFloat(2)-1);
                float rotateWithSpread = (rnd * Accuracy);
                float angle = 0f;
                Vector3 axis;
                //Получение оси и угла поворота
                transform.rotation.ToAngleAxis(out angle, out axis);
                Vector3 shooting_pos;
                if (alternativeShoot)
                {
                    if(CurrentShootingPoint != ShootingPointFlip) shooting_pos = ShootingPoint.transform.position;
                    else shooting_pos = shooting_pos_3.transform.position;
                }
                else
                {
                    shooting_pos = ShootingPointFlip.transform.position;
                }
                alternativeShoot = !alternativeShoot;
                Instantiate(Bullet, shooting_pos, Quaternion.AngleAxis((angle + rotateWithSpread), axis));
                
                AudioSource.Play();
            }
        }
        else
        {
            if (ShootingTempCurrent <= 0 && ReloadTimeCurrent <= 0)
            {
                //shootingTempCurrent = 0.2f;
                AudioSource.clip = OutOfAmmoSound;
                AudioSource.Play();
            }
            
        }
    }
}
