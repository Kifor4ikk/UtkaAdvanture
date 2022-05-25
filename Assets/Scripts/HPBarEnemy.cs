using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarEnemy : MonoBehaviour
{

    [SerializeField] private GameObject back;
    [SerializeField] private GameObject front;
    [SerializeField] private SpriteRenderer frontImage;

    [SerializeField] private GameObject enemy;
    // Update is called once per frame
    void Update()
    {
        frontImage.size = new Vector2(  0.33f * enemy.GetComponent<LivingEntity>().getHP() / enemy.GetComponent<LivingEntity>().getMaxHP(),0.03f);
        front.transform.position = new Vector3(enemy.transform.position.x,enemy.transform.position.y+0.2f, enemy.transform.position.z);
        back.transform.position = new Vector3(enemy.transform.position.x,enemy.transform.position.y+0.2f, enemy.transform.position.z);
    }
}
