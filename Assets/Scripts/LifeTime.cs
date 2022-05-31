using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float livingTime;
    private float currentLivingTime = 0;
    void Update()
    {
        currentLivingTime += Time.deltaTime;
        if (currentLivingTime > livingTime) Destroy(gameObject);
    }
}