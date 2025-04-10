using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private PlayerMovement pm;
    private GameManager gm;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "player" && pm.hiding == false)
        {
            enemy.transform.position = player.transform.position;
            gm.GameOver();
        }
    }
}
