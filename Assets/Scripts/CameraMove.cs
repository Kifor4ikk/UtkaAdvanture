using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform playerTransform;
    [SerializeField] private string objectTag;

    public void Awake()
    {
        if (this.playerTransform == null)
        {
            if (this.objectTag == "") objectTag = "player";
            this.playerTransform = GameObject.FindGameObjectWithTag(this.objectTag).transform;
        }

        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y,
            playerTransform.position.z - 10);
    }

    // Update is called once per frame
    private float cameraX;
    private float cameraY;
    private float cameraZ;
    private float xCameraBoost;
    private float yCameraBoost;
    private float maxCameraBoost = 2.0F;
    void Update()
    {
        //this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z-10);
        
        cameraX = playerTransform.position.x;
        cameraY = playerTransform.position.y;
        cameraZ = playerTransform.position.z - 10;
        
        this.transform.position = new Vector3(cameraX, cameraY, cameraZ);
        
        Debug.Log("X: " + Camera.main.ScreenToWorldPoint(Input.mousePosition).x + "," + playerTransform.position.x +
                  " |Y:" + Camera.main.ScreenToWorldPoint(Input.mousePosition).y + "," + playerTransform.position.y +
                  " |Z: " + Camera.main.ScreenToWorldPoint(Input.mousePosition).z + "," + playerTransform.position.z);
    }
}