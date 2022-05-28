using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomethingFollowToSomething : MonoBehaviour
{

    [SerializeField] private GameObject toFollow;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
