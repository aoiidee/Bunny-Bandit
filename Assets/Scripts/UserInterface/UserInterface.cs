using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*****************************************************************************
// File Name :         UserInterface.cs
// Author :            Noah M. Lipowski
// Creation Date :     April 8th, 2025
//
// Brief Description : Creates the script for the UI for starting and quitting
the game.
*****************************************************************************/

public class UserInterface : MonoBehaviour
{
    // what happens when the player presses play
    public void PlayGame()
    {
        // loads the tutorial
        SceneManager.LoadScene(1);
    }

    // what happenes when the player presses start in the tutorial
    public void StartGame()
    {
        // loads the first level
        SceneManager.LoadScene(2);
    }

    // what happens when the player presses quit
    public void QuitGame()
    {
        // closes the game
        Application.Quit();

        // shows a debug to confirm that this function is working
        Debug.Log("You have Quit");
    }
}
