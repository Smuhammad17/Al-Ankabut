using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject FinalBoss;
    public Animation AlAnkabutLogo;
    public Image panel;
    public bool EndingActive;
    public AudioSource EndSound;
    public GameObject EndingSpeechOBJ;

    public Animation FinaleMusic_ToCreditsVolume;

    public Text DialogueCount;

    public bool SpeechReady;
    public int x;
    [TextArea(3, 10)]
    public string[] EndingSpeech;

    private void Start()
    {
        FinalBoss = GameObject.Find("Al-'Ankabut Final Boss");
        panel.enabled = false;
        x = 0;
        DialogueCount.enabled = false;
        EndingSpeechOBJ.SetActive(false);
    }

    public void Update()
    {

        if (x >= EndingSpeech.Length - 1)
        {
            x = EndingSpeech.Length - 1;
        }

        else if (x < 0)
        {
            x = 0;
        }

        EndingSpeechOBJ.GetComponent<Text>().text = EndingSpeech[x];


       

        if (!FinalBoss && !EndingActive)
        {
            StartCoroutine(EndGame());
        }

        if (SpeechReady)
        {
            if (Input.GetKeyDown("e"))
            {
                x++;
                
            }

            else if (Input.GetKeyDown("q"))
            {
                x--;
            }
        }

        if(x == EndingSpeech.Length - 1)
        {
            SpeechReady = false;
            EndingSpeechOBJ.GetComponent<Text>().fontSize -= 1 * Mathf.RoundToInt(Time.deltaTime);
            StartCoroutine(EndGame2());
        }

        if (EndingActive)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<Player>().Health = Player.GetComponent<Player>().MaxHealth;
            Player.transform.position = new Vector3(1000, 1000, 1000);
            Player.GetComponent<Player>().FinaleCutscene = true;
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
            GameObject.FindGameObjectWithTag("LevelCanvas").GetComponent<Canvas>().enabled = false;

        }

        DialogueCount.text = x.ToString() + "/" + (EndingSpeech.Length - 1);

    }

    bool endsoundanim_played = false;

    IEnumerator EndGame()
    {
        EndingActive = true;
        panel.enabled = true;
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Player>().Health = Player.GetComponent<Player>().MaxHealth;
        Player.transform.position = new Vector3(1000, 1000, 1000);
        Player.GetComponent<Player>().FinaleCutscene = true;
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("LevelCanvas").GetComponent<Canvas>().enabled = false;


    
            EndSound.Play();

        

        AlAnkabutLogo.Play();
        yield return new WaitForSeconds(6f);
        x = 0;
        EndingSpeechOBJ.SetActive(true);
        EndingSpeechOBJ.GetComponent<Animation>().Play();
     
        SpeechReady = true;

        yield return new WaitForSeconds(6f);
        DialogueCount.enabled = true;




    }

    IEnumerator EndGame2()
    {
        if (!endsoundanim_played)
        {
            endsoundanim_played = true;
            FinaleMusic_ToCreditsVolume.Play();
        }
        yield return new WaitForSeconds(5f);

        if (GameObject.FindGameObjectWithTag("Loading"))
        {
            GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("Repentance");
        }
        else
        {
           // Debug.Log("No Loading Assets Found, reverting to simple load");
            SceneManager.LoadScene("Repentance");
        }
    }
}
