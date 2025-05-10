using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         Art.cs
// Author :            Noah M. Lipowski
// Creation Date :     April 22nd, 2025
//
// Brief Description : This script is used for the in-game sprites for them
to face the camera.
*****************************************************************************/

public class Art : MonoBehaviour
{
    [SerializeField] private Camera cam;

    // makes the in-game sprites look at the camera at all times
    private void LateUpdate()
    {
        // gets the camera's position
        Vector3 camPos = cam.transform.position;

        // makes it rotate on y-axis
        camPos.y = transform.position.y;

        // makes the sprite face the camera
        transform.LookAt(camPos);

        // rotate 180 on Y because of spriterenderer works
        transform.Rotate(0f, 180f, 0f);
    }
}
