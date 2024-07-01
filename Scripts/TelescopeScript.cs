using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeScript : MonoBehaviour
{
    [TextArea(3, 10)]
    public string NPCText;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TalkNPC")
        {


            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCTextBox;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
            NPCTextBox = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Hold E to look through telescope";
            NPCTextBox.text = NPCText;
          
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;


        }

    }

    private void OnTriggerExit(Collider other)
    {
        GameObject DiaCanvas;
        GameObject BackPanel;
        Text NPCText;
        Text NPCNameBox;

        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
        NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
        NPCNameBox.text = " ";
        NPCText.text = " ";
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;


    }
}
