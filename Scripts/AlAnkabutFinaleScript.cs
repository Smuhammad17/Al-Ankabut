using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AlAnkabutFinaleScript : MonoBehaviour
{


    //Dynamic NPC
    public DynamicNPCClass Dynamic_NPC = new DynamicNPCClass();
    public GameObject PreviewBox;
    public GameObject PreviewText;
    public GameObject NPCName;
    //UI Elements
    public Text NPC_Text_Element;
    public Text NPC_Name_Element;

    public GameObject ObjBase;

    public GameObject DiaCanvas;
    public GameObject DialogueCount;

    public GameObject NPCIndicator;

    //Dialogue State
    public bool Talking;
    public bool ToTalkAgain;
    Player PlayerOBJ;

    public ResurrectionGate ConnectedGate;

    public void Start()
    {

        Talking = false;

        PlayerOBJ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Dynamic_NPC.Cam.enabled = false;

        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
        DiaCanvas.GetComponent<Canvas>().enabled = false;
        NPC_Text_Element = DiaCanvas.transform.Find("BackPanel").transform.Find("UIDia").GetComponent<Text>();

        //StartCoroutine(TypeSentence(Dynamic_NPC.Dialogues[0].Sentence));
        //NPC_Text_Element.text = Dynamic_NPC.Dialogues[0].Sentence;

        NPC_Name_Element = DiaCanvas.transform.Find("BackPanel").transform.Find("UIName").GetComponent<Text>();
        NPCName.GetComponent<TextMesh>().text = Dynamic_NPC.Name;
        PreviewText.GetComponent<TextMesh>().text = Dynamic_NPC.PreviewDialogue;

        NPCName.SetActive(false);
        PreviewBox.SetActive(false);
        NPCIndicator.SetActive(true);

        //if the player has cleared this gate set resurrection for all connected npcs;
        if (ConnectedGate != null)
        {
            if (PlayerPrefs.GetInt(ConnectedGate.PlayerPrefName_ToClearGate) == 1)
            {
                Dynamic_NPC.Resurrected = true;
                PlayerPrefs.SetInt(this.gameObject.name, 1);
            }
        }



    }


    public int x = 0;



    public void Update()
    {
        if (Dynamic_NPC.Resurrected)
        {
            NPCName.GetComponent<TextMesh>().text = "<color=gold>Resurrected</color>";
            PreviewText.GetComponent<TextMesh>().text = Dynamic_NPC.PostResurrectionPreview;



        }


        //look at player
        if (!Talking)
            ObjBase.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

        float playerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - PlayerOBJ.transform.position.x, 2) + Mathf.Pow(transform.position.y - PlayerOBJ.transform.position.y, 2) + Mathf.Pow(transform.position.z - PlayerOBJ.transform.position.z, 2));


        if (playerDist > 15)
        {
            NPCName.SetActive(false);
        }

        else
        {
            NPCName.SetActive(true);


        }



        if (Talking)
        {


            NPC_Name_Element.text = Dynamic_NPC.Name;

            //Reset Mesh position towards Camera
            ObjBase.transform.LookAt(Dynamic_NPC.Cam.transform.position);

            NPCIndicator.SetActive(false);

            PlayerOBJ.FinaleCutscene = true;
          


            NPCName.SetActive(false);


            Dynamic_NPC.Cam.enabled = true;
            DiaCanvas.GetComponent<Canvas>().enabled = true;
            PreviewBox.SetActive(false);

            //Dialogue Count Features
            DialogueCount.SetActive(true);
            if (!Dynamic_NPC.Resurrected)
                DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + Dynamic_NPC.Dialogues.Length;
            else
            {
                DialogueCount.GetComponent<Text>().text = (x + 1) + "/" + Dynamic_NPC.ResurrectedDialogues.Length;
            }

            if (!Dynamic_NPC.Resurrected)
            {

                if (Input.GetKeyDown("e"))
                {
                    if (x < Dynamic_NPC.Dialogues.Length - 1)
                    {
                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();


                        x++;

                        Dynamic_NPC.Dialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);
                        StartCoroutine(TypeSentence(Dynamic_NPC.Dialogues[x].Sentence));
                        DiaCanvas.GetComponent<AudioSource>().Play();


                    }

                    else if (!GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive)
                    {
                        Talking = false;
                        x = Dynamic_NPC.Dialogues.Length - 1;

                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();
                        StartCoroutine(LoadFinale());
                    }


                }

                if (Input.GetKeyDown("q"))
                {
                    if (x >= 1)
                    {
                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();

                        x--;

                        StartCoroutine(TypeSentence(Dynamic_NPC.Dialogues[x].Sentence));
                        DiaCanvas.GetComponent<AudioSource>().Play();
                        Dynamic_NPC.Dialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);

                    }
                    //If Math challenge is active the player can't leave dialogue

                    else if (!GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive)
                    {
                        Talking = false;
                        // x = 0;

                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();

                    }

                }

            }
            //Resurrected dialogue
            else
            {
                if (Input.GetKeyDown("e"))
                {
                    if (x < Dynamic_NPC.ResurrectedDialogues.Length - 1)
                    {
                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();

                        x++;

                        StartCoroutine(TypeSentence(Dynamic_NPC.ResurrectedDialogues[x].Sentence));
                        DiaCanvas.GetComponent<AudioSource>().Play();
                        Dynamic_NPC.ResurrectedDialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);

                    }

                    else if (!GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive)
                    {
                        Talking = false;
                        x = Dynamic_NPC.ResurrectedDialogues.Length - 1;
                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();
                       
                    }


                }

                if (Input.GetKeyDown("q"))
                {
                    if (x >= 1)
                    {
                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();

                        x--;

                        StartCoroutine(TypeSentence(Dynamic_NPC.ResurrectedDialogues[x].Sentence));
                        DiaCanvas.GetComponent<AudioSource>().Play();
                        Dynamic_NPC.ResurrectedDialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);

                    }
                    //If Math challenge is active the player can't leave dialogue
                    else if (!GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive)
                    {
                        Talking = false;
                        //x = -1;

                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();

                    }

                }
            }
        }

        else
        {
            // PlayerOBJ.FinaleCutscene = false;
            Dynamic_NPC.Cam.enabled = false;



        }
    }

    //Writing Text Effect START ----------------------------------




    public IEnumerator TypeSentence(string sentence)
    {
        bool colortext = false;
        bool PlayerColorText = false;
        NPC_Text_Element.text = "";

        foreach (char letter in sentence.ToCharArray())
        {

            if (letter == '[')
            {
                colortext = true;
            }

            else if (letter == '(')
            {
                PlayerColorText = true;

            }

            if (!colortext && !PlayerColorText)
            {
                NPC_Text_Element.text += letter;
            }

            else if (colortext)
            {
                NPC_Text_Element.text += "<color=aqua>" + letter + "</color>";
            }

            else if (PlayerColorText)
            {
                NPC_Text_Element.text += "<color=#FFFF00>" + letter + "</color>";
            }

            if (letter == ')')
            {
                PlayerColorText = false;
            }

            else if (letter == ']')
            {
                colortext = false;
            }

            yield return null;


        }




    }

    string ColorToHexString(Color color)
    {
        Color32 color32 = color;
        return String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color32.r, color32.g, color32.b, color32.a);
    }

    //Writing Text Effect END ----------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            ToTalkAgain = true;
            PreviewBox.SetActive(false);
            NPCIndicator.SetActive(false);
            NPCName.SetActive(true);

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {

            NPCName.GetComponent<TextMesh>().text = "Press E To Face The Spider";
            PreviewBox.SetActive(true);


            if (Input.GetKey("e") && !Dynamic_NPC.SpiderInDisguise && !Talking && ToTalkAgain && !GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive)
            {

                StartCoroutine(BeginDialogue());


            }

            else if (Input.GetKey("e") && Dynamic_NPC.SpiderInDisguise)
            {
                //NPC is a Spider in Disguise
                Instantiate(Dynamic_NPC.SpiderToSpawn, Dynamic_NPC.SpiderSpawn.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }

        }

    }

    public IEnumerator BeginDialogue()
    {
        Dynamic_NPC.Dialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);
        ToTalkAgain = false;
        x = 0;
        if (!Dynamic_NPC.Resurrected)
            StartCoroutine(TypeSentence(Dynamic_NPC.Dialogues[0].Sentence));
        else
        {
            StartCoroutine(TypeSentence(Dynamic_NPC.ResurrectedDialogues[0].Sentence));
        }
        yield return null;
        Talking = true;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            Talking = false;
            ToTalkAgain = true;
            PreviewBox.SetActive(false);
            NPCIndicator.SetActive(true);
            NPCName.GetComponent<TextMesh>().text = Dynamic_NPC.Name;
            NPC_Text_Element.text = "";
            DiaCanvas.GetComponent<Canvas>().enabled = false;
            x = 0;


        }
    }





public IEnumerator LoadFinale()
    {
        yield return new WaitForSeconds(2f);
        if (GameObject.FindGameObjectWithTag("Loading"))
        {
            GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("Finale");
        }
        else
        {
            //Debug.Log("No Loading Assets Found, reverting to simple load");
            SceneManager.LoadScene("Finale");
        }
    }
}
