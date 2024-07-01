using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_NPC_Script : MonoBehaviour
{
    public Name_Builder nBuilder;

    public GameObject[] Male_NPCS;
    public GameObject[] Female_NPCS;

    public string[] names;

    public Dialogue_Builder Dbuilder;


    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 2) == 0)
        {

            GameObject SelectedNPC;
            int Range = Random.Range(0, Male_NPCS.Length);
            SelectedNPC = Male_NPCS[Range];
            for (int x = 0; x < Male_NPCS.Length; x++)
            {
                if (Male_NPCS[x] != Male_NPCS[Range])
                {
                    Male_NPCS[x].SetActive(false);
                }
            }

            for (int x = 0; x < Female_NPCS.Length; x++)
            {
               
               Female_NPCS[x].SetActive(false);
                
            }



            SelectedNPC.GetComponent<NPCScript>().NPC.Dialogue[0] = Dbuilder.dialogues[Random.Range(0, Dbuilder.dialogues.Length)];

            //No Duplicate Names


            //SelectedNPC.GetComponent<NPCScript>().NPC.Name = names[Random.Range(0,names.Length)];
            SelectedNPC.GetComponent<NPCScript>().NPC.Name = nBuilder.BuildName(0);
        }

        else
        {
            GameObject SelectedNPC;
            int Range = Random.Range(0, Female_NPCS.Length);
            SelectedNPC = Female_NPCS[Range];
            for (int x = 0; x < Female_NPCS.Length; x++)
            {
                if (Female_NPCS[x] != Female_NPCS[Range])
                {
                    Female_NPCS[x].SetActive(false);
                }
            }

            for (int x = 0; x < Male_NPCS.Length; x++)
            {
                
                    Male_NPCS[x].SetActive(false);
               
            }



            SelectedNPC.GetComponent<NPCScript>().NPC.Dialogue[0] = Dbuilder.dialogues[Random.Range(0, Dbuilder.dialogues.Length)];

            //No Duplicate Names


            //SelectedNPC.GetComponent<NPCScript>().NPC.Name = names[Random.Range(0,names.Length)];
            SelectedNPC.GetComponent<NPCScript>().NPC.Name = nBuilder.BuildName(1);
        }
    }

}
