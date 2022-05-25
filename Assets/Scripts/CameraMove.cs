using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform playerTransform;
    [SerializeField] private string objectTag;

    public void Awake() {
        if(this.playerTransform == null) {
            if (this.objectTag == "") objectTag = "player";
            this.playerTransform = GameObject.FindGameObjectWithTag(this.objectTag).transform;
        }
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 10);
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z-10);
    }
}
