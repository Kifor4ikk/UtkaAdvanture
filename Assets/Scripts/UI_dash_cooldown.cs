using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_dash_cooldown : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image img;

    void FixedUpdate()
    {
        text.text = "Dash CD = " + PlayerController.getDashCooldownCurrent();
        img.fillAmount = 1 - 1 * (PlayerController.getDashCooldownCurrent() / 4);
    }
}
