using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCScript : MonoBehaviour
{
    public NPCClass NPC = new NPCClass();
    public string CurrentText;
    public GameObject NPCName;
    public bool CanMoveDialogue;
    public bool PlayerInArea;
    //dialogue ui
    public GameObject DiaCanvas;
    public GameObject KhadimsThoughts;
    public GameObject BackPanel;
    public Text NPCText;
    public Text NPCNameBox;
    public Image ScreenICON;


    public GameObject DialogueCount;

    public GameObject HCanv;

    //Dialogue System
    GameObject DialogueInstructions;

    GameObject User;
 

    void Start()
    {
        User = GameObject.FindGameObjectWithTag("Player");

        CanMoveDialogue = false;
        //dialogue box variables
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");

        KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;

        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
        NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
       
        //3d text npc name
        NPCName.GetComponent<TMP_Text>().text = NPC.Name.ToString();
        NPCName.SetActive(false);

        HCanv = GameObject.FindGameObjectWithTag("LevelCanvas");

        ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();

        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
    }

   public int x = 0;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TalkNPC")
        {

            HCanv.GetComponent<Canvas>().enabled = false;

            PlayerInArea = true;
            CanMoveDialogue = true;

            KhadimsThoughts.SetActive(false);

            BackPanel.SetActive(true);

            NPCName.SetActive(true);

            NPCNameBox.text = " ";
            NPCText.text = " ";
            DialogueCount.GetComponent<Text>().enabled = true;
            DialogueCount.GetComponent<Text>().text = " ";
            NPCNameBox.text = NPC.Name;
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;
            DialogueCount.SetActive(true);
        }

        if(other.gameObject.tag == "PlayerShot")
        {
            if(!HitByPlayerShot)
            StartCoroutine(HitByPlayer());
    
        }
    }
    bool HitByPlayerShot;
    string[] HitbyPlayerWords ={
        
        "Ouch", "Knock it off", "Stop!", "That hurts!", "Watch out!", "Hey!"
    };
    public IEnumerator HitByPlayer()
    {
        HitByPlayerShot = true;
        HCanv.GetComponent<Canvas>().enabled = false;

        //PlayerInArea = true;
        CanMoveDialogue = true;

        KhadimsThoughts.SetActive(false);

        BackPanel.SetActive(true);

        NPCName.SetActive(true);

        NPCNameBox.text = " ";
        NPCText.text = HitbyPlayerWords[Random.Range(0,HitbyPlayerWords.Length)];
        DialogueCount.GetComponent<Text>().enabled = true;
        DialogueCount.GetComponent<Text>().text = " ";
        NPCNameBox.text = NPC.Name;
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;
        DialogueCount.SetActive(true);


        yield return new WaitForSeconds(3f);

        HCanv.GetComponent<Canvas>().enabled = true;

        NPCName.SetActive(false);
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
        CanMoveDialogue = false;

        NPCNameBox.text = " ";
        NPCText.text = " ";

        DialogueCount.GetComponent<Text>().text = " ";
        HitByPlayerShot = false;
       // PlayerInArea = false;

    }


    void Update()
    {

        float playerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - User.transform.position.x, 2) + Mathf.Pow(transform.position.y - User.transform.position.y, 2) + Mathf.Pow(transform.position.z - User.transform.position.z, 2));


        //look at player
        if (playerDist < 50)
            transform.LookAt(User.transform.position);
        //Set Name
        if (NPCName.GetComponent<TMP_Text>().text != NPC.Name.ToString())
        NPCName.GetComponent<TMP_Text>().text = NPC.Name.ToString();

        if (CanMoveDialogue) {


            //Screen ICON set to off for non dynamic NPCs
            ScreenICON.enabled = false;
            


            if (Input.GetKeyDown("e"))
        {
                if (x < NPC.Dialogue.Length - 1)
                {
                    x++;
                    DiaCanvas.GetComponent<AudioSource>().Play();
                    DiaCanvas.GetComponent<AudioSource>().pitch = Random.Range(0.90f, 1.1f);
                }

            




            }

        if (Input.GetKeyDown("q"))
        {
                if (x >= 0 + 1)
                {
                    x--;
                    DiaCanvas.GetComponent<AudioSource>().Play();
                    DiaCanvas.GetComponent<AudioSource>().pitch = Random.Range(0.90f, 1.1f);
                }
        }
                             }


        if (PlayerInArea && NPCText.text !=NPC.Dialogue[x])
        {
            NPCText.text = NPC.Dialogue[x];
            DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + NPC.Dialogue.Length;

        }
    }

 


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.tag == "TalkNPC")
        {
            HCanv.GetComponent<Canvas>().enabled = true;

            NPCName.SetActive(false);
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false ;
            CanMoveDialogue = false;

            NPCNameBox.text = " "; 
            NPCText.text = " ";

            DialogueCount.GetComponent<Text>().text = " ";

            PlayerInArea = false;

        }
    }



}
