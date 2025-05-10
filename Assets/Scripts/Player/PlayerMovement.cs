using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*****************************************************************************
// File Name :         PlayerMovement.cs
// Author :            Noah M. Lipowski
// Creation Date :     March 24th, 2025
//
// Brief Description : Holds all the player movement and interaction controls.
This script also includes the code for hiding spots and a lasso timer.
*****************************************************************************/

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput pi;
    [SerializeField] private Rigidbody rb;
    private Enemies enemy;
    
    public PauseMenu pm;
    private InputAction move;
    private InputAction interact;
    private InputAction pause;
    private InputAction restart;
    private InputAction lasso;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spot;
    [SerializeField] private float speed = 10f;
    
    private List<HidingSpots> HidingSpots = new List<HidingSpots>();
    private HidingSpots currentlyHighlighted;
    
    [SerializeField] private Lasso lassoBO;
    public bool hiding = false;
    [SerializeField] private Camera cam;

    // Gets certain components for input actions and also starts the highlight couroutine 
    void Awake()
    {
        // gets the player input component and activates it
        pi = GetComponent<PlayerInput>();
        pi.currentActionMap.Enable();

        // grabs the PauseMenu script to take functions from
        pm = FindObjectOfType<PauseMenu>();

        // grabs the rigid body component from the player
        rb = GetComponent<Rigidbody>();

        // grabs the enemy script for enemies
        enemy = FindObjectOfType<Enemies>();

        // grabs the input actions from the action map in unity
        move = pi.currentActionMap.FindAction("Move");
        interact = pi.currentActionMap.FindAction("Interact");
        pause = pi.currentActionMap.FindAction("Pause");
        restart = pi.currentActionMap.FindAction("Restart");
        lasso = pi.currentActionMap.FindAction("Lasso");

        // creates the functions for the input actions
        move.canceled += Move_canceled;
        pause.started += Pause_started;
        restart.started += Restart_started;
        interact.started += Interact_started;
        lasso.started += Lasso_started;

        // start the coroutine later in the script as soon as the game starts
        StartCoroutine(Highlight());

        // makes the cursor lock in the middle of the screen when the game starts
        Cursor.lockState = CursorLockMode.Locked;

        // makes the cursor invisiable 
        Cursor.visible = false;
    }

    // when the player stops pressing the input buttons for movement
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        // sets velocity to zero so the player doesn't move
        rb.velocity = Vector3.zero;
    }

    // when the player hits the LMB it will activate this function
    private void Lasso_started(InputAction.CallbackContext obj)
    {
        // grabs this function for the lasso script to be used when the input action is called
        lassoBO.ThrowLasso();

        // starts the lasso time coroutine 
        StartCoroutine(LassoTime());
    }

    // when the player presses E it will activate this function
    private void Interact_started(InputAction.CallbackContext obj)
    {
        // if the hiding spot is highlighted
        if (currentlyHighlighted != null)
        {
            // teleports the player to the highlighted hiding spot
            player.transform.position = currentlyHighlighted.transform.position;

            hiding = true;
        }
    }

    // when the player presses R it will activate this function
    private void Restart_started(InputAction.CallbackContext obj)
    {
        // gets the scene we're currently in and restarts it to the start position
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // when the player presses ESC it will activate this function
    public void Pause_started(InputAction.CallbackContext obj)
    {
        // opens up the pause menu which was implemented in the PauseMenu script
        pm.Paused();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // disables all of the input functions
    private void OnDisable()
    {
        move.canceled -= Move_canceled;
        pause.started -= Pause_started;
        restart.started -= Restart_started;
        interact.started -= Interact_started;
        lasso.started -= Lasso_started;
    }

    // the script for player movement
    void FixedUpdate()
    {
        // reads the value of Vector2 of the player and sets that as moveDirection
        Vector2 direction = move.ReadValue<Vector2>();

        Vector3 camHorizontal = cam.transform.right;
        Vector3 camDirection = cam.transform.forward;

        // making the y zero is what stops the player from flying up when the camera is facing up
        camDirection.y = 0f;
        camHorizontal.y = 0f;

        // moves the player in the direction towards the camrea + left and right in that direction as well
        Vector3 velocity = (camDirection * speed * direction.y) + (camHorizontal * speed * direction.x);
        velocity.y = rb.velocity.y;

        // sets the RigidBody velocity to the Vector3 velocity I made
        rb.velocity = velocity;

        // moves the player to look in the direction of where the camera is facing
        transform.LookAt(transform.position + camDirection);
    }

    public void EnterHiding(Collider other)
    {
        // if the hiding spot isn't null
        if (other.gameObject.GetComponent<HidingSpots>() != null)
        {
            // adds the hiding spot to the HidingSpots list
            HidingSpots.Add(other.gameObject.GetComponent<HidingSpots>());
        }
    }

    public void ExitHiding(Collider other)
    {
        // if the hiding spot isn't null
        if (other.gameObject.GetComponent<HidingSpots>() != null)
        {
            // removes the hiding spot to the HidingSpots list
            HidingSpots.Remove(other.gameObject.GetComponent<HidingSpots>());

            // sets the current hiding spot to false so it doesn't show in the game
            currentlyHighlighted.spot.SetActive(false);

            // sets the entire hiding spot to null so the player can no longer teleport to it
            currentlyHighlighted = null;
        }

        // checks if the player is on top of a hiding spot
        if (other.transform.tag == "hidespot")
        {
            hiding = false;
        }
    }

    // a coroutine for finding the nearest hiding spot and highlighting it
    IEnumerator Highlight()
    {
        // creates a transform called location
        Transform location;

        for (; ; )
        {
            location = null;

            // if the amount of items in the HidingSpot list isn't 0 
            if (HidingSpots.Count != 0)
            {
                // sets location to equal the hiding spot's transform
                location = HidingSpots[0].transform;
            }

            // sets i to 0 and the count to greater than i
            for (int i = 0; i < HidingSpots.Count; i++)
            {
                // if the distance between the player and hidingspots object is less than the player and location
                // transform
                if (Vector3.Distance(player.transform.position, HidingSpots[i].gameObject.transform.position) <
                    Vector3.Distance(player.transform.position, location.position))
                {
                    // sets location to the hiding spots transform
                    location = HidingSpots[i].transform;
                }
            }

            // if hiding spots cound doesn't equal 0
            if (HidingSpots.Count != 0)
            {
                //calls the HighlightSpot function
                HighlightSpot(location);
            }

            // waits for end of frame
            yield return new WaitForEndOfFrame();
        }
    }

    // officially highlights the hiding spot
    private void HighlightSpot(Transform position)
    {
        // if the spot isn't null and the transform doesn't equal the position
        if (currentlyHighlighted != null && currentlyHighlighted.transform != position)
        {
            // sets the hiding spot false so it wont be shown
            currentlyHighlighted.spot.SetActive(false);
        }
        // sets currentlyHighlighted to the position of the hiding spot
        currentlyHighlighted = position.gameObject.GetComponent<HidingSpots>();
        
        // sets the hiding spot active so it can be seen when nearby
        currentlyHighlighted.spot.SetActive(true);
    }

    // a coroutine for making the lasso return and keeping the player stuck while the lasso is out
    IEnumerator LassoTime()
    {
        // sets canMove to false so the player cannot move
        //canMove = false;
        
        // makes everything wait for 1 second
        yield return new WaitForSeconds(1);
        
        // after those seconds the player can move again
        //canMove = true;

        // calls the function from the lasso script to return the lasso after those seconds
        lassoBO.ReturnLasso();
    }
}