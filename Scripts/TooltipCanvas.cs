using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TooltipCanvas : MonoBehaviour
{

    public GameObject[] Screens;
    public int x = 1;
    Canvas UI;
    Player Player;
    bool TimehasStopped;
    GameObject PauseMenu;

    GameObject LoadScreen;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Loading"))
        LoadScreen = GameObject.FindGameObjectWithTag("Loading");

        UI = this.GetComponent<Canvas>();
        UI.enabled = false;

        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (SceneManager.GetActiveScene().name == "Tutorial 2.0")
        {
            UI.enabled = true;
            this.gameObject.SetActive(true);


            PauseMenu.GetComponent<PauseMenuScript>().enabled = false;
            PauseMenu.GetComponent<Canvas>().enabled = false;


            if (!Player.FinaleCutscene)
            {
                Player.FinaleCutscene = true;
            }

            if (!LoadScreen)
            {
                if (!TimehasStopped)
                {
                    Time.timeScale = 0;
                    TimehasStopped = true;
                }
            }
            else
            {
                StartCoroutine(WaittoPauseTime());
            }


        }
    }

    IEnumerator WaittoPauseTime()
    {
        while (LoadScreen.GetComponent<LevelManager>().IsLoading)
        {
            yield return new WaitForSeconds(1f);


        }

        if (!LoadScreen.GetComponent<LevelManager>().IsLoading)
        {
            if (!TimehasStopped)
            {
                Time.timeScale = 0;
                TimehasStopped = true;
            }
        }
    }

    bool keypressed;

    // Update is called once per frame
    void Update()
    {

    


        if (Input.GetKey("e") && !keypressed && x < Screens.Length){
            keypressed = true;
            x++;
        }

        if(Input.GetKey("q") && !keypressed && x > 0)
        {
            keypressed = true;
            x--;
        }

        if (Input.GetKeyUp("e") || Input.GetKeyUp("q") && keypressed)
        {
            keypressed = false;
        }


        for (int y = 0; y <= Screens.Length - 1; y++)
        {
            if (y != x)
            {
                Screens[y].gameObject.SetActive(false);
            }

            else
            {
                Screens[y].gameObject.SetActive(true);
            }
        }


        if (x < 0)
        {
            x = 0;
        }

        else if (x > Screens.Length - 1)
        {
            Exit(0);
            PauseMenu.GetComponent<PauseMenuScript>().enabled = true;

        }
    }

    public void Exit(int p)
    {
        if (p == 0)
        {
            this.gameObject.SetActive(false);
            if (Player.FinaleCutscene && !GameObject.FindGameObjectWithTag("PauseMenu"))
            {
                Player.FinaleCutscene = false;
            }
            TimehasStopped = false;
            Time.timeScale = 1;

        }

        else
        {
            UI.enabled = false;
        }
    }

    public void OpenTooltip()
    {
        UI.enabled = true;
        this.gameObject.SetActive(true);

        PauseMenu.GetComponent<Canvas>().enabled = false;
        if (!Player.FinaleCutscene)
        {
            Player.FinaleCutscene = true;
        }

        if (!TimehasStopped)
        {
            Time.timeScale = 0;
            TimehasStopped = true;
        }
    }
}
