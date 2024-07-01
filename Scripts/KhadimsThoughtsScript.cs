using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KhadimsThoughtsScript : MonoBehaviour
{
    public NPCClass NPC = new NPCClass();
    public string CurrentText;

    public bool CanMoveDialogue;
    public bool PlayerInArea;
    //dialogue ui
    public GameObject DiaCanvas;
    public GameObject KhadimsThoughts;

    public GameObject BackPanel;
    public Text NPCText;

    public Text DialogueCount;

    public GameObject HCanv;



    void Start()
    {

        CanMoveDialogue = false;
        //dialogue box variables
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");

        KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;
        KhadimsThoughts.SetActive(false);
        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        DialogueCount = KhadimsThoughts.transform.Find("Dialogue Count").GetComponent<Text>();
        NPCText = KhadimsThoughts.transform.Find("KT_Text").GetComponent<Text>();


       


        HCanv = GameObject.FindGameObjectWithTag("LevelCanvas");

    }

    public int x = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {

            HCanv.GetComponent<Canvas>().enabled = false;

            PlayerInArea = true;
            CanMoveDialogue = true;

            KhadimsThoughts.SetActive(true);


            BackPanel.SetActive(false);
          

           
            NPCText.text = " ";
          
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;
      
        }


    }



    void Update()
    {
     

        if (CanMoveDialogue)
        {


          


            if (Input.GetKeyDown("e"))
            {
                if (x < NPC.Dialogue.Length - 1)
                {
                    x++;
                    DiaCanvas.GetComponent<AudioSource>().Play();
                }






            }

            if (Input.GetKeyDown("q"))
            {
                if (x >= 0 + 1)
                {
                    x--;
                    DiaCanvas.GetComponent<AudioSource>().Play();

                }
            }
        }


        if (PlayerInArea)
        {
            NPCText.text = NPC.Dialogue[x];
            DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + NPC.Dialogue.Length;

        }
    }




    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            HCanv.GetComponent<Canvas>().enabled = true;

           
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
            CanMoveDialogue = false;

        
            NPCText.text = " ";

            KhadimsThoughts.SetActive(false);

            BackPanel.SetActive(true);

            PlayerInArea = false;

        }
    }


}
