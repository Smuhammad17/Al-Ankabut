using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCAdaptionsScript : MonoBehaviour
{
    public NPCClass NPC = new NPCClass();
    public string CurrentText;
    public GameObject NPCName;
    public bool CanMoveDialogue;
    //dialogue ui
    public GameObject DiaCanvas;
    public GameObject BackPanel;
    public Text NPCText;
    public Text NPCNameBox;

    //Adaption Camera
    public Camera AdCam;

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
        //3d text npc name
        NPCName.GetComponent<TextMesh>().text = NPC.Name;

        GameObject.FindGameObjectWithTag("PlayerViewport").GetComponent<Camera>().enabled = true;
        AdCam.enabled = false;

    }

    public int x = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            NPCName.GetComponent<TextMesh>().text = "Press E to begin Adaption";

            CanMoveDialogue = true;

            NPCNameBox.text = NPC.Name;


            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

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
       
         NPCText.text = NPC.Dialogue[x];
            if (x > 0 && x < NPC.Dialogue.Length - 1)
            {
                GameObject.FindGameObjectWithTag("PlayerViewport").GetComponent<Camera>().enabled = false;
                AdCam.enabled = true;
            }

            else if(x == 0 || x >= NPC.Dialogue.Length - 1)
            {
                GameObject.FindGameObjectWithTag("PlayerViewport").GetComponent<Camera>().enabled = true;
                AdCam.enabled = false;
            }
         
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
            CanMoveDialogue = false;

            NPCNameBox.text = " ";
            NPCText.text = " ";

            GameObject.FindGameObjectWithTag("PlayerViewport").GetComponent<Camera>().enabled = true;
            AdCam.enabled = false;
        }
    }



}
