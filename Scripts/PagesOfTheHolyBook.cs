using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PagesOfTheHolyBook : MonoBehaviour
{
    public POTHBClass POTB;
    public GameObject POTBHoverText;
    public int x = 0;

    GameObject DiaCanvas;
    GameObject BackPanel;
    Text NPCText;
    Text NPCNameBox;
    GameObject DialogueCount;

    public Image ScreenICON;

    public void Start()
    {
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
        NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
        DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + POTB.Description.Length;

        POTBHoverText = this.transform.Find("POTHBText").gameObject;
        POTBHoverText.SetActive(false);
        POTBHoverText.GetComponent<TextMesh>().text = POTB.Verse;

        ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
        

    }

    public void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

       

        if (Input.GetKeyDown("e"))
        {
            if (x < POTB.Description.Length - 1)
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


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TalkNPC")
        {
            x = 0;
        }
    }


    public void OnTriggerStay(Collider other)
    {
        POTBHoverText.SetActive(true);
        
        if (other.tag == "TalkNPC")
        {

            NPCNameBox.text = POTB.Verse;

            DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + POTB.Description.Length;

            if (!POTB.Collected)
            {
            
                other.GetComponentInParent<Player>().POTHBCount++;
                other.GetComponentInParent<Player>().SetPOTHB(other.GetComponentInParent<Player>().POTHBCount);
                POTB.Collected = true;
                this.GetComponent<AudioSource>().Play();
            }


            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;

            NPCText.text = POTB.Description[x];
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

     
            


          



        }
    }

    public void OnTriggerExit(Collider other)
    {
        x = 0;
    
        if (other.tag == "TalkNPC")
        {
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
            POTBHoverText.SetActive(false);
        }
    }

}
