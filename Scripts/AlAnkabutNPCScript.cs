using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlAnkabutNPCScript : MonoBehaviour
{
    public NPCClass NPC = new NPCClass();
    public GameObject Player;
    public string CurrentText;
    public GameObject NPCName;
    public bool CanMoveDialogue;
    //dialogue ui
    public GameObject DiaCanvas;
    public GameObject BackPanel;
    public Text NPCText;
    public Text NPCNameBox;
    public GameObject DialogueCount;


    //Dialogue System
    GameObject DialogueInstructions;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        CanMoveDialogue = false;
        //dialogue box variables
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
        NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
        //3d text npc name
        NPCName.GetComponent<TextMesh>().text = NPC.Name;
        NPCName.SetActive(false);



    }

    public int x = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
   

            CanMoveDialogue = true;

            NPCNameBox.text = NPC.Name;


            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;
            DialogueCount.SetActive(true);
        }


    }



    void Update()
    {
        //look at player
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        NPCName.GetComponent<TextMesh>().text = NPC.Name.ToString();

        if (CanMoveDialogue)
        {


            if (Input.GetKeyDown("e"))
            {
                if (x < NPC.Dialogue.Length - 1)
                {
                    x++;




                }






            }

            if (Input.GetKeyDown("q"))
            {
                if (x >= 0 + 1)
                {
                    x--;


                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            NPCName.SetActive(true);
            NPCText.text = NPC.Dialogue[x];
            DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + NPC.Dialogue.Length;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            NPCName.SetActive(false);
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
            CanMoveDialogue = false;

            NPCNameBox.text = " ";
            NPCText.text = " ";


        }
    }



}
