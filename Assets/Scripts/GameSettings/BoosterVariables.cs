using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterVariables : MonoBehaviour
{
    //VolumeSetting
    [SerializeField] public static float volume = 0.1f;

    //Booster settings
    [SerializeField] private static float boosterTimeMax; 
        
    [SerializeField] private static float speed;
    private float speedTimeCurrent;
    
    [SerializeField] private static float damage;
    private float damageTimeCurrent;
    
    [SerializeField] private static float attackTemp;
    private float attackTempTimeCurrent;
    
    [SerializeField] private static bool isImmortal;
    private float immortalTimeCurrent;

    [SerializeField] public static float gameSpeed = 1f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedTimeCurrent > 0) speedTimeCurrent -= Time.deltaTime;
        if (damageTimeCurrent > 0) speedTimeCurrent -= Time.deltaTime;
        if (attackTempTimeCurrent > 0) speedTimeCurrent -= Time.deltaTime;
        if (immortalTimeCurrent > 0) speedTimeCurrent -= Time.deltaTime;
    }

    public void activateSpeed()
    {    
        
        if (speedTimeCurrent <= 0) speedTimeCurrent = boosterTimeMax;
    }
    
    public void activateDamage()
    {
        if (damageTimeCurrent <= 0) damageTimeCurrent = boosterTimeMax;
    }

    public void activateAttackTemp()
    {
        if (attackTempTimeCurrent <= 0) attackTempTimeCurrent = boosterTimeMax;
    }

    public void activateImmortal()
    {
        if (immortalTimeCurrent <= 0) immortalTimeCurrent = boosterTimeMax;
    }

    public float speedBoost()
    {
        if (speedTimeCurrent > 0) {return speed;}
        return 1;
    }

    public float damageBoost()
    {
        if (damageTimeCurrent > 0) return damage;
        return 1;
    }

    public float attackTempBoost()
    {
        if (attackTempTimeCurrent > 0) return attackTemp;
        return 1;
    }

    public bool isImmortalBooster()
    {
        if (attackTempTimeCurrent > 0) return true;
        return false;
    }

    public static float getSeedChangeable()
    {
        return (float) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
    
}
