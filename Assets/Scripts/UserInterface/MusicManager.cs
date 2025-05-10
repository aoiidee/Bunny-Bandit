using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*****************************************************************************
// File Name :         MusicManager.cs
// Author :            Noah M. Lipowski
// Creation Date :     May 10, 2025
//
// Brief Description : The code for controlling the volume of the music using
the slider in settings.
*****************************************************************************/

public class MusicManager : MonoBehaviour
{
    [SerializeField] private Slider music;

    // chesk the preferences at the start of the game
    void Start()
    {
        // if the player hasn't put their preferences in
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            // automatically sets the volume to 1
            PlayerPrefs.SetFloat("musicVolume", 1);
            
            // calls the load function
            Load();
        }

        // if the player has put their preferences in
        else
        {
            // calls the load function
            Load();
        }
    }

    // grabs the audio in the game and sets it to the music function here
    public void ChangeMusic()
    {
        // sets the in game volume to the music slider we made here
        AudioListener.volume = music.value;
        
        // calls the save function
        Save();
    }

    // loads the last volume save
    private void Load()
    {
        // sets the music volume to the players preferences
        music.value = PlayerPrefs.GetFloat("musicVolume");
    }

    // saves the volume preference
    private void Save()
    {
        // sets the preferences to the value the player chose
        PlayerPrefs.SetFloat("musicVolume", music.value);
    }
}
