using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPoint : MonoBehaviour
{
    public GameObject KhadimsThoughts;
    public GameObject[] Moveables;
    public GameObject[] Keys;
    public GameObject[] CalculatorPins;
    public Vector3[] KeyPositions;
    public Vector3[] Positions;
    public Vector3[] CalcPinPositions;

    private void Start()
    {

        Moveables = GameObject.FindGameObjectsWithTag("Moveables");
        Keys = GameObject.FindGameObjectsWithTag("MoveableKey");
        CalculatorPins = GameObject.FindGameObjectsWithTag("CalculatorPin");

        for (int x = 0; x < Moveables.Length; x++)
        {
            Positions[x] = new Vector3(Moveables[x].transform.position.x, Moveables[x].transform.position.y, Moveables[x].transform.position.z);

        }

        for (int x = 0; x < Keys.Length; x++)
        {
            KeyPositions[x] = new Vector3(Keys[x].transform.position.x, Keys[x].transform.position.y, Keys[x].transform.position.z);
        }

        for (int x = 0; x < CalculatorPins.Length; x++)
        {
            CalcPinPositions[x] = new Vector3(CalculatorPins[x].transform.position.x, CalculatorPins[x].transform.position.y, CalculatorPins[x].transform.position.z);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
       if(other.tag == "TalkNPC")
        {
            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Reset Point";

            NPCText.text = "Hold E to reset all Keys, Weights, and Blocks";

        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "TalkNPC")
        {
         
            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Reset Point";
            
            

       
       


            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;

            KhadimsThoughts.SetActive(false);


            if (Input.GetKeyDown("e"))
            {

                NPCText.text = "Key, Weight, and Block Positions Reset";

                this.GetComponent<AudioSource>().Play();

                Moveables = GameObject.FindGameObjectsWithTag("Moveables");

                for (int x = 0; x < Moveables.Length; x++)
                {
                    if (Moveables[x])
                    {
                        Moveables[x].transform.position = Positions[x];
                        Moveables[x].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }


                for (int x = 0; x < Keys.Length; x++)
                {
                    if (Keys[x] != null && Keys[x])
                    {
                        Keys[x].transform.position = KeyPositions[x];
                        Keys[x].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }

                for (int x = 0; x < CalculatorPins.Length; x++)
                {
                    if (CalculatorPins[x] != null && CalculatorPins[x])
                    {
                        CalculatorPins[x].transform.position = CalcPinPositions[x];
                        CalculatorPins[x].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }

            }
            
        }

    }

   

  
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
      
            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();

            NPCNameBox.text = "";
            NPCText.text = "";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;
        }
    }
}


