using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionGate : MonoBehaviour
{
    public Dynamic_NPC_Script[] RessurectedNPCs;

    public GameObject Construct;

    public string PlayerPrefName_ToClearGate;

    private void Start()
    {

      

        Construct.SetActive(false);


    }

     //Update is called once per frame
    void Update()
    {
        if (RessurectedNPCs[0] != null)
        {
            bool AllRessurrected = true;

            for (int x = 0; x < RessurectedNPCs.Length; x++)
            {
                if (!RessurectedNPCs[x].Dynamic_NPC.Resurrected)
                {
                    AllRessurrected = false;
                }
            }

            if (AllRessurrected)
            {

                Construct.SetActive(true);
            }
        }



    }

    
}
