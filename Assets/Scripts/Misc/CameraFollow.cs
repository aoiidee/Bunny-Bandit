using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         CameraFollow.cs
// Author :            Noah M. Lipowski
// Creation Date :     March 24th, 2025
//
// Brief Description : The script for having the main camera follow the player
from a certain position behind them.
*****************************************************************************/

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;

    // sets the offset
    void Start()
    {
        // sets the vector3 offsets for x, y, and z
        offset = new Vector3(4, 5, 0);
    }

    // sets the position of the camera
    void Update()
    {
        // sets the camera's position to the players position with the offset
        transform.position = player.transform.position + offset;
    }
}
