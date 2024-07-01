using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Infomrative_Sign_Script : MonoBehaviour
{
    public NPCClass NPC = new NPCClass();
    public string CurrentText;

    public bool CanMoveDialogue;
    public bool PlayerInArea;
    //dialogue ui
    public GameObject DiaCanvas;
    public GameObject BackPanel;
    public Text NPCText;
    public Text NPCNameBox;



    public GameObject DialogueCount;

    public GameObject HCanv;

    //Dialogue System
    GameObject DialogueInstructions;


    void Start()
    {

        CanMoveDialogue = false;
        //dialogue box variables
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
        NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
    

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


            

            NPCNameBox.text = " ";
            NPCText.text = " ";
            DialogueCount.GetComponent<Text>().text = " ";
            NPCNameBox.text = NPC.Name;
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;
            DialogueCount.SetActive(true);
        }


    }



    void Update()
    {
        //look at player
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
       

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

            NPCNameBox.text = " ";
            NPCText.text = " ";

            DialogueCount.GetComponent<Text>().text = " ";

            PlayerInArea = false;

        }
    }

}
