using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private int documents = 0;
    public GameObject gameOver;

    private void OnCollisionEnter(Collision collision)
    {
        // if the exit hits a game object that has the tag "player" and if 1 documents have been collected
        if (collision.transform.tag == "player" && documents == 1)
        {
            gameOver.SetActive(true);
        }
    }
}
