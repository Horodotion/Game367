using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public Text ammoText;
    public Text healthText;
    public Text pauseMenu;

    public int levelToLoad;

    public playerContorller playerScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void SetAmmoText(int ammovalue)
    {
        ammoText.text = "Ammo: " + ammovalue.ToString();
    }
    public void SetHealthText(int healthvalue)
    {
        healthText.text = "Health: " + healthvalue.ToString();
    }
    public void PauseUnpause()
    {
        if(pauseMenu.gameObject.activeInHierarchy == false)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application has been terminated");
    }
}
