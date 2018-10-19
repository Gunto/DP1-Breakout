using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public bool InMenu = true;
    public bool InstructionsOpened = false;
    public bool Exited = false;

    public void ToggleMenu()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (!Application.isEditor)
                SceneManager.LoadScene("Menu");
            InMenu = true;
        }
        else
        {
            if (!Application.isEditor)
                SceneManager.LoadScene("Main");
            InMenu = false;
        }
    }

    public void InstructionsButton()
    {
        if (!InstructionsOpened)
        {
            InstructionsOpened = true;
        } else
        {
            InstructionsOpened = false;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        if (!Exited)
        {
            Exited = true;
        }
        else
        {
            Exited = false;
        }
    }
}
