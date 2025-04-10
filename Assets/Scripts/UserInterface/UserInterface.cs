using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    // what happens when the player presses play
    public void PlayGame()
    {
        // loads the game
        SceneManager.LoadScene(1);
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
