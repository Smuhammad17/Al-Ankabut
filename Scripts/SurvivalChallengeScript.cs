using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalChallengeScript : MonoBehaviour
{

    public bool GameActive;
    public SurvivalChallengeClass SC;
    public Transform Player;

    public Player User;
    public int Money_Payout = 100;

    public void Start()
    {
        User = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        SC.Label_RC.enabled = false;
        SC.Remaining_Count_Text.enabled = false;

    }

    public IEnumerator RunChallenge(int Enemy_count, Dynamic_NPC_Script NPCResponse)
    {
        if (!NPCResponse.Dynamic_NPC.Resurrected)
        {
            GameActive = true;
            //End Dialogue once challenge starts
            NPCResponse.Talking = false;


            GameObject[] SpawnedSpiders = new GameObject[Enemy_count];
            yield return new WaitForSeconds(2);
            for (int x = 0; x < Enemy_count; x++)
            {
                int RanRange;
                RanRange = Random.Range(0, SC.Spiders_to_Spawn.Length);
                //Spawn spiders on the ground 
                SpawnedSpiders[x] = Instantiate(SC.Spiders_to_Spawn[RanRange], (Player.transform.position + new Vector3(Random.insideUnitSphere.x, 0 , Random.insideUnitSphere.z).normalized * Random.Range(10,13)) - new Vector3(0,Player.transform.position.y,0), Quaternion.identity);

             

            }

          

            while (GameActive)
            {
                SC.Label_RC.enabled = true;
                SC.Remaining_Count_Text.enabled = true;
                NPCResponse.Talking = false;
                int y = 0;
                for (int x = 0; x < SpawnedSpiders.Length; x++)
                {
                    if (SpawnedSpiders[x] != null)
                    {
                        y++;
                    }
                }

                SC.Remaining_count = y;
                SC.Remaining_Count_Text.text = SC.Remaining_count.ToString();

                if (y <= 0)
                {
                    GameActive = false;
                    GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Animation>().Play("Challenge_Celebrations_Positive");
                    GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Celebration_SoundManager>().positivePlay();
                    SC.Label_RC.enabled = false;
                    SC.Remaining_Count_Text.enabled = false;
                    NPCResponse.Dynamic_NPC.Resurrected = true;



                    //Give Money
                    User.StartCoroutine(User.AddMoney(Money_Payout));


                    //Rememberance for each Dynamic NPC
                    if (PlayerPrefs.HasKey(NPCResponse.gameObject.name))
                    {
                        PlayerPrefs.SetInt(NPCResponse.gameObject.name, 1);
                    }


                    //Load first resurrected dialogue after challenge completion
                    //NPCResponse.StartCoroutine(NPCResponse.TypeSentence(NPCResponse.Dynamic_NPC.ResurrectedDialogues[0].Sentence));

                    NPCResponse.x = 0;
                }

                yield return new WaitForSeconds(1);
            }




        }
    }
}
