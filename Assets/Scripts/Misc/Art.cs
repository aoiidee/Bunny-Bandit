using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Art : MonoBehaviour
{
    [SerializeField] private Camera cam;

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
