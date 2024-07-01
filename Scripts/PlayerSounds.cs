using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource FootstepSource;
    public AudioSource PlayerDamage;
    public AudioSource PlayerFiring;
    public AudioSource FaithFieldSource;
    public AudioSource FaithFieldConstantHum;
    public AudioSource RewardSound;


    public AudioClip[] FootstepClips;
    public AudioClip[] PlayerDamageClips;
    public AudioClip[] PlayerFiringClips;
    public AudioClip[] FaithFieldRedirection;
    public AudioClip[] FaithFieldIgnition;

    public AudioClip ConstantHum;

    public Player myPlayer;
    public FaithFieldCollision FF;

    public void Start()
    {
        myPlayer = this.gameObject.GetComponent<Player>();
        FF = this.gameObject.GetComponent<FaithFieldCollision>();
        PlayerDamage.clip = PlayerDamageClips[Random.Range(0, PlayerDamageClips.Length)];
    }


    public void Update()
    {

       

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Footsteps();
        }

        else
        {
            FootstepSource.loop = false;
        }

        if (myPlayer != null)
        {
            if (Input.GetMouseButtonDown(0) && !myPlayer.HandsFull)
            {

                PlayerFiring.PlayOneShot(PlayerFiringClips[Random.Range(0, PlayerFiringClips.Length)]);
            }
        }

        if(Input.GetMouseButtonDown(1) && FF.FaithFieldVal > 80 && !FF.FaithFieldActive)
        {
            FaithFieldSource.clip = FaithFieldIgnition[Random.Range(0, FaithFieldIgnition.Length)];
            FaithFieldSource.Play();
        };

        if (FF.FaithFieldActive)
        {
            FaithFieldConstantHum.clip = ConstantHum;
            FaithFieldConstantHum.loop = true;
            FaithFieldConstantHum.Play();
        }

        else
        {
            FaithFieldConstantHum.loop = false;
            FaithFieldConstantHum.Stop();
        }


       

    }


   public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "NonRectEnemy" || other.gameObject.tag == "RedBullet" || other.gameObject.tag == "YellowBullet" || other.gameObject.tag == "Enemy")
        {
            if (!this.GetComponent<FaithFieldCollision>().FaithFieldActive)
            {
                PlayerDamage.clip = PlayerDamageClips[Random.Range(0, PlayerDamageClips.Length)];
                PlayerDamage.Play();
            }
            else
            {
                FaithFieldSource.clip = FaithFieldRedirection[Random.Range(0, FaithFieldRedirection.Length)];
                FaithFieldSource.Play();
            }
        }

    }

    //Speed Strip
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpeedStrip")
        {
            FaithFieldSource.clip = FaithFieldRedirection[Random.Range(0, FaithFieldRedirection.Length)];
            FaithFieldSource.Play();
        }
    }

    void Footsteps()
    {
        if (!FootstepSource.isPlaying)
        {
            FootstepSource.clip = FootstepClips[Random.Range(0, FootstepClips.Length)];
            FootstepSource.Play();
           
        }

       
      

    }



}
