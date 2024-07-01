using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalSoundSystem : MonoBehaviour
{
    public AudioClip[] Songs;
    public AudioSource Source;


    float standard_volume;

    bool started = false;

    private void Start()
    {
        if(Songs.Length > 0)
        Source.clip = Songs[Random.Range(0, Songs.Length)];
        Source.Play();
        Source.loop = false;

        standard_volume = Source.volume;
     
    }

    public bool in_a_nonPlayerLevel;

    // Update is called once per frame
    void Update()
    {

        if (!in_a_nonPlayerLevel)
        {

            if (!Source.isPlaying && !started && !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().LgndMusicPlaying)
            {
                started = true;
                ChooseNewSong();
            }

            if (!Source.isPlaying && started)
            {
                started = false;
            }




            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().LgndMusicPlaying)
            {
                Source.volume = 0;
            }

            else
            {
                Source.volume = standard_volume;
            }


        }

        else
        {
            if (!Source.isPlaying && !started)
            {
                started = true;
                ChooseNewSong();
            }

            if (!Source.isPlaying && started)
            {
                started = false;
            }
        }
    }


    void ChooseNewSong()
    {
        
        Source.clip = Songs[Random.Range(0, Songs.Length)];
        Source.PlayDelayed(Random.Range(0, 20));
    }

  
}
