using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InfectAbdulJacobson : MonoBehaviour
{

    public bool followplayer = false;
    public float movespeed;
    public Transform SpiderMesh;
    Rigidbody rgbd;

    public Canvas ToNewScreen;

    public bool caught = false;

    public AudioSource CaughtSound;
    public AudioSource Music;

    public float acceleration = 0;

    public float musicVolume;
    
    GameObject CaughtCanvas;

    GameObject User;

   public ParticleSystem CapturedParticles;

    private void Start()
    {
        User = GameObject.FindGameObjectWithTag("Player");
        caught = false;
        CaughtCanvas = GameObject.FindGameObjectWithTag("CaughtCanvas");
    
        ToNewScreen = CaughtCanvas.GetComponent<Canvas>();
        ToNewScreen.enabled = false;
        followplayer = false;
        rgbd = this.GetComponent<Rigidbody>();
        StartCoroutine(Waittime());
        InvokeRepeating("SQuotes", 0, 2);
    }



    // Update is called once per frame
    void Update()
    {

        Music.volume = musicVolume;

        if(!caught)
        SpiderMesh.transform.LookAt(User.transform.position);



        if (followplayer)
        {
            rgbd.AddRelativeForce((User.transform.position - this.transform.position).normalized * movespeed);
            rgbd.velocity = (User.transform.position - this.transform.position) * movespeed / 6;


            while (acceleration < 50)
            {
                acceleration+= Time.deltaTime * 100f;
            }

            movespeed += Time.deltaTime * Time.deltaTime * acceleration;
        
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "TalkNPC") 
        {
            
            StartCoroutine(Caught());

        }
    }

    public IEnumerator Caught()
    {
      
        CaughtSound.Play();
        caught = true;
        CapturedParticles.Play();
        User.GetComponent<Al_AnkabutPlayer>().FinaleCutscene = true;
       
        yield return new WaitForSeconds(5f);
        CaughtCanvas.GetComponent<Animation>().Play();
        ToNewScreen.enabled = true;
       

        this.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(17f);

     

     

        yield return new WaitForSeconds(1f);

        if (GameObject.FindGameObjectWithTag("Loading"))
        {
            GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("New Game Opening");
        }
        else
        {
        
            SceneManager.LoadScene("New Game Opening");
        }

    }

    public IEnumerator Waittime()
    {
        yield return new WaitForSeconds(1f);
        followplayer = true;
    }

  
}
