using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_dash_cooldown : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image img;

    [SerializeField] private bool isDeveloperModeOn = false;

    private StringBuilder totalText = new StringBuilder();
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1)) isDeveloperModeOn = !isDeveloperModeOn;

        if (isDeveloperModeOn)
        {
            totalText.Clear();
            totalText.Append("Dash cooldown ->" + PlayerControl.getDashCooldownCurrent() + "\n");
            totalText.Append("Current player speed ->" + PlayerControl.getSpeedCurrent() + "\n");
            totalText.Append("Is weapon near ->" + PlayerControl.IsWeaponNear() + "\n");
            totalText.Append("Weapon ->" + PlayerControl.getWeapon() + "\n");
            totalText.Append("This is developer mode!");

            text.SetText(totalText.ToString());
        }
    }
}
