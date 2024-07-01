using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class RequisitionChallenge : MonoBehaviour
{
    public bool GameActive;

    public Player User;
    public int Money_Payout = 100;


    public Transform Player;
    public Text Instructions;
    public GameObject Panel;

    public void Start()
    {
        User = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Instructions.enabled = false;
        Panel.SetActive(false);
    }

 

    public IEnumerator ShowObjectiveMessage()
    {

        Instructions.enabled = true;
        Panel.SetActive(true);
        yield return new WaitForSeconds(4f);
        Instructions.enabled = false;
        Panel.SetActive(false);
    }
   


    public IEnumerator RunChallenge(Transform item, Dynamic_NPC_Script NPCResponse, string Message)
    {
   

        if (!NPCResponse.Dynamic_NPC.Resurrected)
        {
            item.gameObject.SetActive(true);

            GameActive = true;
            //End Dialogue once challenge starts
            NPCResponse.Talking = false;
            NPCResponse.PlayerOBJ.FinaleCutscene = false;
          





            while (GameActive)
            {

                float PlayerDist = Mathf.Sqrt(Mathf.Pow(NPCResponse.transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(NPCResponse.transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(NPCResponse.transform.position.z - Player.transform.position.z, 2));


                if (PlayerDist < 15)
                {
                    Instructions.text = Message;
                    StartCoroutine(ShowObjectiveMessage());
                }
           
               

                float Dist = Mathf.Sqrt(Mathf.Pow(NPCResponse.transform.position.x - item.transform.position.x, 2) + Mathf.Pow(NPCResponse.transform.position.y - item.transform.position.y, 2) + Mathf.Pow(NPCResponse.transform.position.z - item.transform.position.z, 2));

                if (Dist <= 4)
                {
                  
                    GameActive = false;
                    GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Animation>().Play("Challenge_Celebrations_Positive");
                    GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Celebration_SoundManager>().positivePlay();

                    //Give Money
                    User.StartCoroutine(User.AddMoney(Money_Payout));


                    NPCResponse.Dynamic_NPC.Resurrected = true;

                    //Rememberance for each Dynamic NPC
                    if (PlayerPrefs.HasKey(NPCResponse.gameObject.name))
                    {
                        PlayerPrefs.SetInt(NPCResponse.gameObject.name,1);
                    }


                    item.gameObject.SetActive(false);
                    //Load first resurrected dialogue after challenge completion
                    //NPCResponse.StartCoroutine(NPCResponse.TypeSentence(NPCResponse.Dynamic_NPC.ResurrectedDialogues[0].Sentence));
                    Instructions.enabled = false;
                    NPCResponse.x = 0;
                }


                yield return new WaitForSeconds(1);
            }



        }

    }

}
