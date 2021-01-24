using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_MusicTimeEffect : MonoBehaviour
{
    public Powers_TimeManager timeManager;
    public AudioClip normalMusic;
    public AudioClip reversedMusic;

    private AudioSource musicSource;

    //checks if time is positive
    private bool isTimePositive;

    private void Start()
    {
        //Get the audio source component on the gameobject
        musicSource = GetComponent<AudioSource>();

        //Set the music clip to normal and play
        musicSource.clip = normalMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float musicTime = musicSource.time;

        //Checks if the time variable is between -1 and 0.
        if (timeManager.time > -1 && timeManager.time < 0)
        {
            if (isTimePositive)
            {
                //Set the new music and the time correlating to the music
                musicSource.clip = reversedMusic;
                musicSource.time = (normalMusic.length - musicTime);

                //Play the music
                musicSource.Play();
            }

            //Sets pitch to the opposite of the time variable.
            musicSource.pitch = -timeManager.time;

            //Sets time positive to false.
            if (timeManager.time != 0) isTimePositive = false;
        }
        //Otherwise checks if the time variable is between 0 and 1.
        else if (timeManager.time > 0 && timeManager.time < 1)
        {
            if (!isTimePositive)
            {
                //Set the new music and the time correlating to the music
                musicSource.clip = normalMusic;
                musicSource.time = (reversedMusic.length - musicTime);

                //Play the music
                musicSource.Play();
            }

            //Sets pitch of the time variable.
            musicSource.pitch = timeManager.time;

            //Sets time positive to true.
            if (timeManager.time != 0) isTimePositive = true;
        }
        //Otherwise if the current time is above one, the pitch will be limited to 1.
        else if (timeManager.time > 1) musicSource.pitch = 1;
    }
}
