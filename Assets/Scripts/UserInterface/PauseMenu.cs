using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    // what happens when the player presses the resume button
    public void Resume()
    {
        // disables the pause menu so it isn't shown on screen
        pause.SetActive(false);

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

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
