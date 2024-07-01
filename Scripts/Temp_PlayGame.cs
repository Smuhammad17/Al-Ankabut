using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp_PlayGame : MonoBehaviour
{
    public LevelManager LevelLoader;
    public GameObject confirmationScreen;



    private void Start()
    {
        confirmationScreen.GetComponent<Canvas>().enabled = false;
    }


    public void BackButton(){
        confirmationScreen.GetComponent<Canvas>().enabled = false;
    }


    public void ShowConfirmation (int choice)
    {
        confirmationScreen.GetComponent<Canvas>().enabled = true;

        if (choice == 0)
        {
            confirmationScreen.transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = "Start a New Game? (Old progress will be lost)";
            confirmationScreen.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate { PlayNewGame(); });
            
        }

        else
        {
            confirmationScreen.transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = "Continue Game?";
            confirmationScreen.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate { ContinueGame(); });
        }
    }



    public void ContinueGame()
    {

        if (GameObject.FindGameObjectWithTag("Loading"))
        {
           LevelLoader.ContinueGame();



        }

        else
        {
            PlayerPrefs.DeleteAll();
            LevelLoader.LoadScene("TheWanderingSpider");
        }
    }

    public void PlayNewGame()
    {


        PlayerPrefs.DeleteAll();
        LevelLoader.LoadScene("TheWanderingSpider");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

}
