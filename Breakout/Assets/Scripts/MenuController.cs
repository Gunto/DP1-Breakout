using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public bool InMenu = true;
    public bool InstructionsOpened = false;
    public bool Exited = false;
    public bool PlayAgainClicked = false;

	public void ToggleMenu()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (!Application.isEditor)
                SceneManager.LoadScene("Level1");
            InMenu = true;
        }
        else
        {
            if (!Application.isEditor)
                SceneManager.LoadScene("Level1");
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

	/// <summary>
	/// When the user clicks the 'Play Again' button, the scene is reset
	/// </summary>
	public void PlayAgain()
	{
		if (!Application.isEditor) // Avoids crashes during testing due to how the Unity framework works
			SceneManager.LoadScene("Level1");
		PlayAgainClicked = true;
	}
}
