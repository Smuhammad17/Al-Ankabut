using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RequisitionItem : MonoBehaviour
{
    public RequisitionItem_ScriptableObj ObjData;

    public GameObject HoverText;
    public GameObject DiaCanvas;
    public GameObject DialogueCount;
    public Image ScreenICON;
    public Transform Player;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        HoverText.GetComponent<TextMesh>().text = ObjData.HovertextMessage;
       
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
        DiaCanvas.GetComponent<Canvas>().enabled = false;
        HoverText.SetActive(false);

        //Disable object so it can't be found until player starts quest
        this.gameObject.SetActive(false);

    }

    public void Update()
    {
        HoverText.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

        float PlayerDist = Mathf.Sqrt(Mathf.Pow(this.gameObject.transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(this.gameObject.transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(this.gameObject.transform.position.z - Player.transform.position.z, 2));
        if(PlayerDist < 15)
        {
            HoverText.SetActive(true);
        }

        else
        {
            HoverText.SetActive(false);
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "TalkNPC" && other.tag != "NPC")
        {
            DiaCanvas.GetComponent<Canvas>().enabled = true;
            DiaCanvas.transform.Find("BackPanel").transform.Find("UIDia").GetComponent<Text>().text = ObjData.Message;
            DiaCanvas.transform.Find("BackPanel").transform.Find("UIName").GetComponent<Text>().text = ObjData.Label;


            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "TalkNPC")
        {
            DiaCanvas.GetComponent<Canvas>().enabled = false;
            DiaCanvas.transform.Find("BackPanel").transform.Find("UIDia").GetComponent<Text>().text = " ";
            DiaCanvas.transform.Find("BackPanel").transform.Find("UIName").GetComponent<Text>().text = " ";
        }
    }


}
