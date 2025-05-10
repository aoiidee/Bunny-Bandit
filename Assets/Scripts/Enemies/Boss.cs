using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*****************************************************************************
// File Name :         Boss.cs
// Author :            Noah M. Lipowski
// Creation Date :     May 10, 2025
//
// Brief Description : The code for the boss in level three for his movement
and death of the player.
*****************************************************************************/

public class Boss : MonoBehaviour
{
    private PlayerMovement pm;
    private GameManager gm;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    private bool move = false;

    public bool Move { get => move; set => move = value; }

    // Grabs components needed for this script 
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    // checks to see if the player is in a hiding spot
    private void OnTriggerEnter(Collider other)
    {
        // if the collision is the player and they aren't hiding
        if (other.transform.tag == "player" && pm.hiding == false)
        {
            // sets the enemy position to the player
            boss.transform.position = player.transform.position;

            // 'kills' the player ending the game
            gm.GameOver();
        }
    }

    // movement for the boss
    void Update()
    {
        // checks if the boss can move
        if (move)
        {
            // has the boss follow the player
            agent.SetDestination(player.transform.position);
        }
    }
}
