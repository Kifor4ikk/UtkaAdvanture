using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingTimeControl : MonoBehaviour
{

    [SerializeField] private float livingTime;
    private float currentLivingTime = 0;
    // Update is called once per frame
    void Start()
    {
    }
    void Update()
    {

        currentLivingTime += Time.deltaTime;
        if(currentLivingTime > livingTime) Destroy(gameObject);
    }
}
