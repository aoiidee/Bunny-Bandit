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
    public PauseMenu pm;
    private InputAction move;
    private InputAction jump;
    private InputAction interact;
    private InputAction pause;
    private InputAction restart;
    private InputAction lasso;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spot;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 0.1f;
    private List<HidingSpots> HidingSpots = new List<HidingSpots>();
    private HidingSpots currentlyHighlighted;
    [SerializeField] private Lasso lassoBO;
    private bool canMove = true;
    public bool hiding = false;

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

        // grabs the input actions from the action map in unity
        move = pi.currentActionMap.FindAction("Move");
        jump = pi.currentActionMap.FindAction("Jump");
        interact = pi.currentActionMap.FindAction("Interact");
        pause = pi.currentActionMap.FindAction("Pause");
        restart = pi.currentActionMap.FindAction("Restart");
        lasso = pi.currentActionMap.FindAction("Lasso");

        // creates the functions for the input actions
        jump.started += Jump_started;
        pause.started += Pause_started;
        restart.started += Restart_started;
        interact.started += Interact_started;
        lasso.started += Lasso_started;

        StartCoroutine(Highlight());
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
    }

    // when the player presses space it will activate this function
    private void Jump_started(InputAction.CallbackContext obj)
    {
        // checks if the player is on the ground
        if (IsGrounded())
        {
            // pushes the player up making them 'jump'
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    // checks if the player is on the ground for the player to jump
    private bool IsGrounded()
    {
        // if the player is on the ground
        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            // they can jump
            return true;
        }
        // if not, they can't jump
        return false;
    }

    // disables all of the input functions
    private void OnDisable()
    {
        jump.started -= Jump_started;
        pause.started -= Pause_started;
        restart.started -= Restart_started;
        interact.started -= Interact_started;
        lasso.started -= Lasso_started;
    }

    // the script for player movement
    void Update()
    {
        // checks if the player is allowed to move
        if (canMove)
        {
            // reads the value of Vector2 of the player and sets that as moveDirection
            Vector2 moveDirection = move.ReadValue<Vector2>();
            
            // the player's velocity will be the moveDirection at the certain speed in both X and Y from the Vector2 
            rb.velocity = new Vector3(moveDirection.y * speed * -1, rb.velocity.y, moveDirection.x * speed);
        }
    }

    // when the player gets close to a hiding spot it adds that hiding spot to a list
    private void OnTriggerEnter(Collider other)
    {
        // if the hiding spot isn't null
        if (other.gameObject.GetComponent<HidingSpots>() != null)
        {
            // adds the hiding spot to the HidingSpots list
            HidingSpots.Add(other.gameObject.GetComponent<HidingSpots>());
        }
    }

    // when the player moves away from a hiding spot it will remove that from the list
    private void OnTriggerExit(Collider other)
    {
        // if the hiding spot isn't null
        if (other.gameObject.GetComponent<HidingSpots>() != null)
        {
            // removes the hiding spot to the HidingSpots list
            HidingSpots.Remove(other.gameObject.GetComponent<HidingSpots>());
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
        canMove = false;
        
        // makes everything wait for 1 second
        yield return new WaitForSeconds(1);
        
        // after those seconds the player can move again
        canMove = true;

        // calls the function from the lasso script to return the lasso after those seconds
        lassoBO.ReturnLasso();
    }
}