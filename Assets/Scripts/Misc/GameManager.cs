using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private TMP_Text docnum;


    // sets the UI text at the beginning of the scene/game
    private void Awake()
    {
        docnum.text = "Documents = 0";
    }

    // when the object this script is attached to is collided with something
    private void OnCollisionEnter(Collision collision)
    {
        // if the exit hits a game object that has the tag "player" and if 8 documents have been collected
        if (collision.transform.tag == "player" && documents == 8)
        {
            // checks what scene it is and if it is Level3
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
            {
                // pauses the time
                Time.timeScale = 0f;

                // sets the gameWon screen in game to true
                gameWon.SetActive(true);
            }

            else
            {
                // restarts the scene back to the start position
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    // when the player grabs a document it adds onto the number, also does a specific event in level 3
    public void Document()
    {
        // adds 1 to the amount of documents collected
        documents++;
        docnum.text = "Documents = " + documents;

        // checks if the scene is Level3
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && documents == 1)
        {
            // grabs the enemies from Level3
            Boss[] level3 = FindObjectsOfType<Boss>(); 

            foreach (Boss boss in level3)
            {
                // allows the enemies to move
                boss.Move = true;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && documents == 8)
        {
            docnum.text = "Get to the back window!!";
        }
    }

    // when the player loses 
    public void GameOver()
    {
        // sets the game over screen active 
        gameOver.SetActive(true);

        // unlocks the cursor
        Cursor.lockState = CursorLockMode.None;

        // makes the cursor visible
        Cursor.visible = true;

        // pauses the time
        Time.timeScale = 0f;
    }
}
