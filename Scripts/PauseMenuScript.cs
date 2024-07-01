using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool MenuIsOn;
    Canvas PauseMenu;
   public GameObject PauseMain;
   public GameObject PauseOptions;

   public  Dropdown DifficultyDropdown;


   public Slider Volume;

    public GameObject User;


   

    public bool MenuStatus()
    {
        return MenuIsOn;
    }

    public void MenuSwitch()
    {
        MenuIsOn = !MenuIsOn;
    
    }

    public void Resume()
    {
        MenuIsOn = false;
        User.GetComponent<Player>().FinaleCutscene = false;


        User.GetComponent<Player>().Hcanv.GetComponent<Canvas>().enabled = true;
        

        User.transform.Find("PlayerViewport").GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().enabled = true;
    }

    public void ExitGame()
    {
     
        Time.timeScale = 1;
       // Debug.Log(Time.timeScale);
        StartCoroutine(ExitGameC());
    }

    IEnumerator ExitGameC()
    {
        
        yield return new WaitForSeconds(2f);

        if (GameObject.FindGameObjectWithTag("Loading"))
        {
            GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("MainMenu");
        }
        else
        {
        
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ExitToDesktop()
    {
        MenuIsOn = false;
        Application.Quit();
    }

    public void OptionsSwitch() {
        
            PauseMain.SetActive(!PauseMain.activeSelf);
            PauseOptions.SetActive(!PauseOptions.activeSelf);

            if(PauseMain.activeSelf == PauseOptions.activeSelf)
            {
                PauseMain.SetActive(true);
                PauseOptions.SetActive(false);
            }
        }


    //private void OnLevelWasLoaded(int level)
    //{
    //    PauseMenu = this.GetComponent<Canvas>();
    //    MenuIsOn = false;

    //    PauseMain.SetActive(true);
    //    PauseOptions.SetActive(false);

   
    //}

    // Start is called before the first frame update
    void Start()
    {

        User = GameObject.FindGameObjectWithTag("Player");

       

        PauseMenu = this.GetComponent<Canvas>();
        MenuIsOn = false;

        PauseMain.SetActive(true);
        PauseOptions.SetActive(false);

        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.SetInt("Difficulty", DifficultyDropdown.value);
            DifficultyDropdown.value = 2;
        }

        else
        {
            DifficultyDropdown.value = PlayerPrefs.GetInt("Difficulty");
            PlayerPrefs.SetInt("Difficulty", DifficultyDropdown.value);
            
            
        }


        if (!PlayerPrefs.HasKey("UserVolume"))
        {
            PlayerPrefs.SetFloat("UserVolume", Volume.value);
        }

        else
        {
            PlayerPrefs.SetFloat("UserVolume", Volume.value);
        }

    }

    bool TimehasStopped;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //Only do and look for these items if the player is not on the main menu. Player can't pause on the main menu. 
            if (User)
            {


                MenuSwitch();

            }
        }

                if (MenuIsOn)
                {
                    if (!PauseMain.activeSelf)
                    {
                         this.GetComponent<Canvas>().enabled = true;
                    
                if (!PauseOptions.activeSelf)
                        PauseMain.SetActive(true);
                     
                       
                     }

                    if (!PauseMenu.enabled == false)
                    PauseMenu.enabled = true;
               
                    
                    if (!TimehasStopped)
                    {
                        Time.timeScale = 0;
                        TimehasStopped = true;
                    }


                    if (User && !User.GetComponent<Player>().FinaleCutscene )
                    {
                        User.GetComponent<Player>().FinaleCutscene = true;
                        User.transform.Find("PlayerViewport").GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().enabled = false;
                    }
                }

                else
                {

                    if (PauseMenu.enabled = true && Time.timeScale == 0 && TimehasStopped)
                    {
                        PauseMenu.enabled = false;
                        Time.timeScale = 1;
                        TimehasStopped = false;
                    }

                                        //This should fix the bug that causes finale cutscene to be deisabled when the menu is off and the player is just talking to NPCS.
                    if (!User.GetComponent<Player>().Player_is_Talking)
                    {
                        User.GetComponent<Player>().FinaleCutscene = false;

                        //Get Key Up is to make sure that when the player presses the space bar the menu doesn't come up. 
                        if (User && Input.GetKeyUp("space") || Input.GetKeyUp(KeyCode.Escape))
                        {

                            User.GetComponent<Player>().FinaleCutscene = false;
                            User.GetComponent<Player>().Hcanv.GetComponent<Canvas>().enabled = true;
                            User.transform.Find("PlayerViewport").GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip>().enabled = true;
                        }
                    }

                    if (PauseMain.activeSelf)
                    {
                         PauseMain.SetActive(false);
                        PauseOptions.SetActive(false);
                        this.GetComponent<Canvas>().enabled = false;
                    }

            DifficultyDropdown.Hide();
                }

                //Set Options to equal player prefs
                if(PlayerPrefs.GetInt("Difficulty") != DifficultyDropdown.value)
                PlayerPrefs.SetInt("Difficulty", DifficultyDropdown.value);
         



                AudioListener.volume = PlayerPrefs.GetFloat("UserVolume");

        //set player prefs
        if (PlayerPrefs.GetInt("UserVolume") != Volume.value)
            PlayerPrefs.SetFloat("UserVolume", Volume.value);



            

          

        

    }
}
