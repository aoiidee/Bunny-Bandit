using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
// File Name :         Lasso.cs
// Author :            Noah M. Lipowski
// Creation Date :     March 25th, 2025
//
// Brief Description : This script is for all of the lasso's functionality.
*****************************************************************************/

public class Lasso : MonoBehaviour
{
    private BoxCollider box;
    private LineRenderer lr;
    [SerializeField] private GameObject player;
    private GameManager gm;
    private GameObject caught;

    // grabs unity components for the lasso and sets them to disable at the start
    void Start()
    {
        // grabs the components from unity 
        box = GetComponent<BoxCollider>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        
        // immedietely sets the box collider and line rendered to disabled at the start of the game
        box.enabled = false;
        lr.enabled = false;
    }

    // the function for when the lasso is thrown
    public void ThrowLasso()
    {
        // sets the box collider to true so it can read to grab
        box.enabled = true;
    }

    // the function for when the lasso is returned
    public void ReturnLasso()
    {
        // sets the box collider back to disabled
        box.enabled = false;
        
        // if the player caught a document in their lasso throw
        if (caught != null)
        {
            // activates the document function in game manager
            gm.Document();
            
            // destroys the document when its been caught
            Destroy(caught);
            
            // sets caught to null so the player can catch another document if needed
            caught = null;
            
            // disables the line renderer
            lr.enabled = false;
        }
    }

    // if the box collider hits a certain object
    private void OnTriggerEnter(Collider other)
    {
        // checks if the object hit has the layer "document" attached
        if (other.gameObject.layer == 6)
        {
            // calls the draw line function
            DrawLine(other.transform);
            
            // sets the box collider to false
            box.enabled = false;
        }
    }

    // draws a line that represents a lasso
    private void DrawLine(Transform other)
    {
        // if the tag of the object that was hit has the tag "document" attached
        if(other.transform.tag == "document")
        {
            // sets the line renderer to active
            lr.enabled = true;
            
            // creates a starting position from the player 
            lr.SetPosition(0, transform.position);
            
            // sets the end position of the line
            lr.SetPosition(1, other.position);
            
            // sets caught to the game object that has the tag
            caught = other.gameObject;
        }
    }
}
