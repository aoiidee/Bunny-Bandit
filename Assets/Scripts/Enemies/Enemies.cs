using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    private bool move = true;

    public bool Move { get => move; set => move = value; }

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();

        timer = wanderTimer;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            Move = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "player" && pm.hiding == false)
        {
            enemy.transform.position = player.transform.position;
            gm.GameOver();
        }
    }

    void Update()
    {
        if (move)
        {
            timer += Time.deltaTime;
            if (timer >= wanderTimer)
            {
                ranPoint = Random.Range(0, points.Length);
                agent.SetDestination(points[ranPoint].position);
                timer = 0;
            }

            if (enemy.transform.position == points[ranPoint].position)
            {
                ranPoint = Random.Range(0, points.Length);
                agent.SetDestination(points[ranPoint].position);
            }
        }
    }
}
