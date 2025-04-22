using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*****************************************************************************
// File Name :         GameManager.cs
// Author :            Noah M. Lipowski
// Creation Date :     March 24th, 2025
//
// Brief Description : Game Manager controls the main component of going to the
next level after all documents are collected and counting the documents.
*****************************************************************************/

public class GameManager : MonoBehaviour
{
    public int documents = 0;
    public GameObject gameOver;
    public GameObject gameWon;

    // when the object this script is attached to is collided with something
    private void OnCollisionEnter(Collision collision)
    {
        // if the exit hits a game object that has the tag "player" and if 8 documents have been collected
        if (collision.transform.tag == "player" && documents == 8)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
            {
                Time.timeScale = 0f;
                gameWon.SetActive(true);
            }

            else
            {
                // restarts the scene back to the start position
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void Document()
    {
        // adds 1 to the amount of documents collected
        documents++;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && documents == 1)
        {
            Enemies[] level3 = FindObjectsOfType<Enemies>(); 

            foreach (Enemies enem in level3)
            {
                enem.Move = true;
            }
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
