using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*****************************************************************************
// File Name :         PauseMenu.cs
// Author :            Noah M. Lipowski
// Creation Date :     April 8th, 2025
//
// Brief Description : Insert Description
*****************************************************************************/

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    // what happens when the player presses the resume button
    public void Resume()
    {
        // disables the pause menu so it isn't shown on screen
        pause.SetActive(false);

        // locks the cursor when the game resumes
        Cursor.lockState = CursorLockMode.Locked;

        // makes the cursor invisible
        Cursor.visible = false;

        // resumes time
        Time.timeScale = 1f;
    }

    // what happens when the player pauses the game
    public void Paused()
    {
        // enables the pause meny so it is shown on the screen
        pause.SetActive(true);

        // pauses time
        Time.timeScale = 0f;
    }

    // what happens when the player presses quit
    public void QuitGame()
    {
        // closes the game
        Application.Quit();

        // shows a debug to confirm that this function is working
        Debug.Log("You have quit.");
    }

    // what happens when the player restarts the level
    public void Restart()
    {
        // resumes time
        Time.timeScale = 1f;

        // gets the active level and loads it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // what happens when the player tries to go to the main menu
    public void MainMenu()
    {
        // resumes time
        Time.timeScale = 1f;

        // loads the main menu scene
        SceneManager.LoadScene(0);
    }
}
