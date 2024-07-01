using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchScript : MonoBehaviour
{

    public GameObject KhadimsThoughts;
    public Image ScreenICON;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;


            BackPanel.transform.Find("Dialogue Count").GetComponent<Text>().enabled = false;

            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Weapon Selection Station";
            NPCText.text = "Press 1 to equip <color=aqua><b>Atom Blasts</b></color> \nPress 2 to equip <color=white><b>Swords of the Spirit</b></color> \nPress 3 to equip <color=yellow><b>The Sabre of the Sure Truth</b></color> \nPress 4 to equip <color=orange><b>The Stars of Justice</b></color> ";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;
            
            KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;

            KhadimsThoughts.SetActive(false);

            ScreenICON.enabled = false;

            if (Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4"))
                DiaCanvas.GetComponent<AudioSource>().Play();

            if (Input.GetKey("1"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(1);
                NPCText.text = "<color=aqua>Atom Blasts Equipped!</color>";
               
            }

            

            if (Input.GetKey("2"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(2);
                NPCText.text = "<color=white>Swords of the Spirit Equipped!</color>";
            }

            if (Input.GetKey("3"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(3);
                NPCText.text = "<color=yellow>Sabre of the Sure Truth Equipped!</color>";
            }

            if (Input.GetKey("4"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(4);
                NPCText.text = "<color=orange>The Stars Of Justice Equipped!</color>";
            }

     

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            GameObject DiaCanvas;
            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            DiaCanvas.GetComponent<Canvas>().enabled = false;

        }
    }
}
