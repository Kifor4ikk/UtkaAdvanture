using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SCAR : SimpleGun
{    
    
    // Full copy of SimpleGun.shoot() just for test here
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
                Instantiate(Bullet, CurrentShootingPoint.transform.position, Quaternion.AngleAxis((angle + rotateWithSpread), axis));
                
                Debug.Log("пук нахуй");
                if(ShootingAnimation != null) Instantiate(ShootingAnimation, CurrentShootingPoint.transform.position, Quaternion.Euler(0f, 0f, Mathf.Atan2(Rotate.y, Rotate.x)));
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
