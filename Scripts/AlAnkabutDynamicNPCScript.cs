using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AlAnkabutDynamicNPCScript : MonoBehaviour
{
    //Dynamic NPC
    public DynamicNPCClass Dynamic_NPC = new DynamicNPCClass();
    public GameObject PreviewBox;
    public GameObject PreviewText;
    public GameObject NPCName;
    //UI Elements
    public Text NPC_Text_Element;
    public Text NPC_Name_Element;
    public Image ScreenICON;
    public Sprite AlAnkabutICON;

    public GameObject ObjBase;

    public GameObject DiaCanvas;

    public GameObject KhadimsThoughts;

    public GameObject DialogueCount;

    public GameObject NPCIndicator;



    //Dialogue State
    public bool Talking;
    public bool ToTalkAgain;
    public Al_AnkabutPlayer PlayerOBJ;

    public ResurrectionGate ConnectedGate;

    GameObject CaughtCanvas;

    public bool PlayerInArea;

    public void Start()
    {

        Talking = false;
        ToTalkAgain = true;
        PlayerOBJ = GameObject.FindGameObjectWithTag("Player").GetComponent<Al_AnkabutPlayer>();

        CaughtCanvas = GameObject.FindGameObjectWithTag("CaughtCanvas");
        CaughtCanvas.GetComponent<Canvas>().enabled = false;

        Dynamic_NPC.Cam.enabled = false;

        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;
        DialogueCount = GameObject.FindGameObjectWithTag("DialogueCount");
        DiaCanvas.GetComponent<Canvas>().enabled = false;
        NPC_Text_Element = DiaCanvas.transform.Find("BackPanel").transform.Find("UIDia").GetComponent<Text>();






        //StartCoroutine(TypeSentence(Dynamic_NPC.Dialogues[0].Sentence));
        //NPC_Text_Element.text = Dynamic_NPC.Dialogues[0].Sentence;

        NPC_Name_Element = DiaCanvas.transform.Find("BackPanel").transform.Find("UIName").GetComponent<Text>();
        ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
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


    bool FinaleCutscene_Hasturnedoff = false;
    void TurnOFF_FinaleCutscene()
    {
        if (!FinaleCutscene_Hasturnedoff)
        {
            PlayerOBJ.FinaleCutscene = false;
            PlayerOBJ.Player_is_Talking = false;
            FinaleCutscene_Hasturnedoff = true;
        }

    }


    public void Update()
    {


        if (Dynamic_NPC.Resurrected)
        {
            NPCName.GetComponent<TextMesh>().text = "<color=gold>Resurrected</color>";
            PreviewText.GetComponent<TextMesh>().text = Dynamic_NPC.PostResurrectionPreview;



        }


        //look at player
        if (!Talking)
            ObjBase.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").transform.position);

        float playerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - PlayerOBJ.transform.position.x, 2) + Mathf.Pow(transform.position.y - PlayerOBJ.transform.position.y, 2) + Mathf.Pow(transform.position.z - PlayerOBJ.transform.position.z, 2));


        if (playerDist > 15)
        {
            NPCName.SetActive(false);
            PreviewBox.SetActive(false);
        }

        else
        {
            NPCName.SetActive(true);
            PreviewBox.SetActive(true);

        }



        if (Talking)
        {
            //Place this inside of the talking condition so that it only activates when the player is talking
            PlayerOBJ.Player_is_Talking = Talking;

            //Speaker Names

            if (!Dynamic_NPC.Resurrected)
            {
                if (Dynamic_NPC.Dialogues[x].PlayerSpeaking)
                {

                    NPC_Name_Element.text = "You";
                    ScreenICON.enabled = true;
                    ScreenICON.sprite = AlAnkabutICON;


                }

                else
                {
                    NPC_Name_Element.text = Dynamic_NPC.Name;
                    ScreenICON.sprite = Dynamic_NPC.NPCICON;
                    ScreenICON.enabled = true;
                }
            }

            else if (Dynamic_NPC.Resurrected)
            {
                if (Dynamic_NPC.ResurrectedDialogues[x].PlayerSpeaking)
                {
                    NPC_Name_Element.text = "You";
                    ScreenICON.enabled = true;
                    ScreenICON.sprite = AlAnkabutICON;
                }


                else
                {
                    NPC_Name_Element.text = Dynamic_NPC.Name;
                    ScreenICON.sprite = Dynamic_NPC.NPCICON;
                    ScreenICON.enabled = true;
                }
            }






            

            PlayerOBJ.transform.Find("OriginalManModel").GetComponent<SpriteRenderer>().enabled = false;





            //Reset Mesh position towards Camera
            ObjBase.transform.LookAt(Dynamic_NPC.Cam.transform.position);

            NPCIndicator.SetActive(false);

            PlayerOBJ.FinaleCutscene = true;
            FinaleCutscene_Hasturnedoff = false;



            NPCName.SetActive(false);


            Dynamic_NPC.Cam.enabled = true;
            DiaCanvas.GetComponent<Canvas>().enabled = true;
            KhadimsThoughts.SetActive(false);
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
                        NPC_Name_Element.text = " ";
                        NPC_Text_Element.text = " ";
                        StopAllCoroutines();


                        x++;

                        Dynamic_NPC.Dialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);
                        //artCoroutine(TypeSentence(Dynamic_NPC.Dialogues[x].Sentence));

                        NPC_Text_Element.text = Dynamic_NPC.Dialogues[x].Sentence;

                        DiaCanvas.GetComponent<AudioSource>().Play();


                    }

                    else 
                    {
                        Talking = false;
                        x = Dynamic_NPC.Dialogues.Length - 1;

                        PlayerOBJ.FinaleCutscene = false;
                        SpawnTheEnemySpider();
                        ToTalkAgain = false;
                        PreviewBox.SetActive(false);
                        NPCIndicator.SetActive(true);
                        NPCName.GetComponent<TextMesh>().text = Dynamic_NPC.Name;
                        NPC_Text_Element.text = " ";
                        StopAllCoroutines();
                        DiaCanvas.GetComponent<Canvas>().enabled = false;
                        x = 0;
                        Destroy(this.gameObject, 1f);



                        //stop typing sentece if player skips dialogue. 
                        StopAllCoroutines();

                    }


                }

                if (Input.GetKeyDown("q"))
                {
                    if (x >= 1)
                    {
                        //stop typing sentece if player skips dialogue. 
                        NPC_Name_Element.text = " ";
                        NPC_Text_Element.text = " ";
                        StopAllCoroutines();

                        x--;

                        //artCoroutine(TypeSentence(Dynamic_NPC.Dialogues[x].Sentence));

                        NPC_Text_Element.text = Dynamic_NPC.Dialogues[x].Sentence;


                        DiaCanvas.GetComponent<AudioSource>().Play();
                        Dynamic_NPC.Dialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);

                    }
                    //If Math challenge is active the player can't leave dialogue

                    else
                    {
                        Talking = false;
                        // x = 0;




                        //stop typing sentece if player skips dialogue. 
                        NPC_Name_Element.text = " ";
                        NPC_Text_Element.text = " ";
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
                        NPC_Name_Element.text = " ";
                        NPC_Text_Element.text = " ";
                        StopAllCoroutines();

                        x++;

                        //StartCoroutine(TypeSentence(Dynamic_NPC.ResurrectedDialogues[x].Sentence));



                        NPC_Text_Element.text = Dynamic_NPC.ResurrectedDialogues[x].Sentence;


                        DiaCanvas.GetComponent<AudioSource>().Play();
                        Dynamic_NPC.ResurrectedDialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);

                    }

                    else
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
                        NPC_Name_Element.text = " ";
                        NPC_Text_Element.text = " ";
                        StopAllCoroutines();

                        x--;

                        //StartCoroutine(TypeSentence(Dynamic_NPC.ResurrectedDialogues[x].Sentence));



                        NPC_Text_Element.text = Dynamic_NPC.ResurrectedDialogues[x].Sentence;


                        DiaCanvas.GetComponent<AudioSource>().Play();
                        Dynamic_NPC.ResurrectedDialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);

                    }
                    //If Math challenge is active the player can't leave dialogue
                    else 
                    {
                        Talking = false;
                        //x = -1;



                        //stop typing sentece if player skips dialogue. 
                        NPC_Name_Element.text = " ";
                        NPC_Text_Element.text = " ";
                        StopAllCoroutines();

                    }

                }
            }
        }

        else
        {
            TurnOFF_FinaleCutscene();

            Dynamic_NPC.Cam.enabled = false;



        }


        if (PlayerInArea)
        {
            if (Input.GetKeyDown("e") && !Dynamic_NPC.SpiderInDisguise && !Talking && ToTalkAgain)
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


            PlayerInArea = true;


            NPCName.GetComponent<TextMesh>().text = "Press E to Talk";
            PreviewBox.SetActive(true);

        }
    }



    public IEnumerator BeginDialogue()
    {
        if (ToTalkAgain)
        {
            if (!Dynamic_NPC.Resurrected)
                Dynamic_NPC.Dialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);
            else
            {
                Dynamic_NPC.ResurrectedDialogues[x].PlayDialogue(Dynamic_NPC.CamAnim, Dynamic_NPC.Render);
            }

            ToTalkAgain = false;

            x = 0;
            if (!Dynamic_NPC.Resurrected)
            {
                //StartCoroutine(TypeSentence(Dynamic_NPC.Dialogues[0].Sentence));
                NPC_Text_Element.text = Dynamic_NPC.Dialogues[0].Sentence;


            }
            else
            {
                // StartCoroutine(TypeSentence(Dynamic_NPC.ResurrectedDialogues[0].Sentence));
                NPC_Text_Element.text = Dynamic_NPC.ResurrectedDialogues[0].Sentence;
            }
            yield return null;
            Talking = true;

        
    
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            if (x < 0)
            {
                Talking = false;


            }

            else
            {

                if (Dynamic_NPC.Resurrected && x > Dynamic_NPC.ResurrectedDialogues.Length)
                    Talking = false;

                else if (!Dynamic_NPC.Resurrected && x > Dynamic_NPC.Dialogues.Length)
                    Talking = false;
            }

            if (!Talking)
            {
                ToTalkAgain = true;
                PreviewBox.SetActive(false);
                NPCIndicator.SetActive(true);
                NPCName.GetComponent<TextMesh>().text = Dynamic_NPC.Name;
                NPC_Text_Element.text = " ";
                StopAllCoroutines();
                DiaCanvas.GetComponent<Canvas>().enabled = false;
                x = 0;

                PlayerInArea = false;

                Dynamic_NPC.CamAnim.Rewind();

  
            }

        }
    }

    public GameObject EnemySpider;
    public Transform infectingSpiderSpawn;
    public bool enespawn = false;
    void SpawnTheEnemySpider()
    {
        if (!enespawn)
        {
            enespawn = true;
            Instantiate(EnemySpider, infectingSpiderSpawn.transform.position, Quaternion.identity);

        }
    }
}
