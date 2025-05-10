using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         Hiding.cs
// Author :            Noah M. Lipowski
// Creation Date :     April 22nd, 2025
//
// Brief Description : This script is for the trigger of entering and leaving
a hiding spot.
*****************************************************************************/

public class Hiding : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;

    // when the player enters a specific trigger in game
    private void OnTriggerEnter(Collider other)
    {
        // reference to the Player Movement script and the function EnterHiding
        pm.EnterHiding(other); 
    }

    // when the player exits a specific trigger in game
    private void OnTriggerExit(Collider other)
    {
        // reference to the Player Movement script and the function ExitHiding
        pm.ExitHiding(other);
    }
}
