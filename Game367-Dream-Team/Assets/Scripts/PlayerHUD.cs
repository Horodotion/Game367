using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text ammoText;
    public Text healthText;

    public playerContorller playerScript;

    public void SetAmmoText(int ammovalue)
    {
        ammoText.text = "Ammo: " + ammovalue.ToString();
    }
    public void SetHealthText(int healthvalue)
    {
        healthText.text = "Health: " + healthvalue.ToString();
    }
}
