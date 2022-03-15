using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance = null;
    public bool paused;

    void Awake()
    {
        // This if statement checks if there is a general manager
        // If it finds no manager, it becomes the manager. If not, it destroys itself.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Pause();
    }

    public static void TogglePause()
    {
        if (instance.paused)
        {
            instance.Unpause();
        }
        else
        {
            instance.Pause();
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
}
