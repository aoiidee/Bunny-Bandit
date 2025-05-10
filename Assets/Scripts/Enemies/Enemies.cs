using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*****************************************************************************
// File Name :         Enemies.cs
// Author :            Noah M. Lipowski
// Creation Date :     April 3rd, 2025
//
// Brief Description : Creates destinations for the enemies to go to and creates
the death event if the enemy catches the player.
*****************************************************************************/

public class Enemies : MonoBehaviour
{
    private PlayerMovement pm;
    private GameManager gm;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    public Transform[] points;
    [SerializeField] private float wanderTimer;
    private float timer;
    private int ranPoint;

    // grabs components needed for the script, starts a timer, and checks if a scene is level 3 at the start
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();

        // a timer for when the enemies can stay in a specific spot
        timer = wanderTimer;
    }

    // checks to see if the player is in a hiding spot
    private void OnTriggerEnter(Collider other)
    {
        // if the collision is the player and they aren't hiding
        if (other.transform.tag == "player" && pm.hiding == false)
        {
            // sets the enemy position to the player
            enemy.transform.position = player.transform.position;

            // 'kills' the player ending the game
            gm.GameOver();
        }
    }


    // the code to move the enemies around to specific spots if they are able to move
    void Update()
    {
        // sets the timer equal to real time
        timer += Time.deltaTime;

        // if the timer is greater than wanderTimer
        if (timer >= wanderTimer)
        {
            // grabs a random point from the points list 
            ranPoint = Random.Range(0, points.Length);

            // sets the destination to that specific point
            agent.SetDestination(points[ranPoint].position);

            // sets the timer to zero
            timer = 0;
        }

        // if the enemy is at the random point
        if (enemy.transform.position == points[ranPoint].position)
        {
            // grabs another point
            ranPoint = Random.Range(0, points.Length);

            // sets that as the new desination
            agent.SetDestination(points[ranPoint].position);
        }
    }
}
