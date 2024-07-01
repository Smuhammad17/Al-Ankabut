using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2D_Health : MonoBehaviour
{
    //Player Health
    public int Health;
    public int MaxHealth = 10;
    public GameObject HealthBar;
    public Sprite[] HealthBarSprites;

    bool isdead = false;

    public  GameObject DeathCanvas;

    private void Start()
    {
        DeathCanvas = GameObject.FindGameObjectWithTag("DeathCanvas");

        Health = 10;
        DeathCanvas.GetComponent<Canvas>().enabled = false;
      
    }

    // Update is called once per frame
    void Update()
    {




        //Set Health Bar sprites
        if (Health > 0)
        {
            HealthBar.GetComponent<Image>().sprite = HealthBarSprites[Health - 1];

        }

        if (Health < 1 && !isdead)
        {
            isdead = true;
            StartCoroutine(Die());
        }
    }



    IEnumerator Die(){

        DeathCanvas.GetComponent<Canvas>().enabled = true;


        this.GetComponent<Player2D_MovementScript>().enabled = false;



        DeathCanvas.GetComponent<Animation>().Play();

        /*
        GameObject DiaCanvas;
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        DiaCanvas.GetComponent<Canvas>().enabled = false;
        */



     

        yield return new WaitForSeconds(6);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        DeathCanvas.GetComponent<Canvas>().enabled = false;
       
        //  DiaCanvas.GetComponent<Canvas>().enabled = true;
        isdead = false;

        Health = MaxHealth;

    }

}
