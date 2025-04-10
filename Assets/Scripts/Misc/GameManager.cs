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
    [SerializeField] private int documents = 0;
    public GameObject gameOver;

    // when the object this script is attached to is collided with something
    private void OnCollisionEnter(Collision collision)
    {
        // if the exit hits a game object that has the tag "player" and if 3 documents have been collected
        if (collision.transform.tag == "player" && documents == 3)
        {
            // restarts the scene back to the start position
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Document()
    {
        // adds 1 to the amount of documents collected
        documents++;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
