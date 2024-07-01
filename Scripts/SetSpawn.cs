using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetSpawn : MonoBehaviour
{
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

            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Sign Post";
            NPCText.text = "Hold E to Set Respawn Point";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;


            if (Input.GetKey("e"))
            {
                PlayerPrefs.DeleteKey("Scene_Location");

                //Set PlayerPrefs Spawn Point
                PlayerPrefs.SetFloat("SpawnX", other.gameObject.transform.position.x);
                PlayerPrefs.SetFloat("SpawnY", other.gameObject.transform.position.y);
                PlayerPrefs.SetFloat("SpawnZ", other.gameObject.transform.position.z);
                if(!PlayerPrefs.HasKey("Scene_Location"))
                PlayerPrefs.SetString("Scene_Location", SceneManager.GetActiveScene().name);
                NPCText.text = "<color=gold>Spawn Point Set!!!</color>\n<color=gold>Money Saved!!!</color>";


                if (!PlayerPrefs.HasKey("SavedMoney"))
                    PlayerPrefs.SetInt("SavedMoney", GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Money);

            }

        }
    }
}
