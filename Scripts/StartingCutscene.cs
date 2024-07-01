using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutscene : MonoBehaviour
{


    Player PlayerOBJ;
    Al_AnkabutPlayer PlayerOBJAA;

    public bool CutscenePlaying;
    public bool FinaleReset = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>())
            {
                PlayerOBJ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            }

            else
            {
           
                PlayerOBJAA = GameObject.FindGameObjectWithTag("Player").GetComponent<Al_AnkabutPlayer>();
            }
        }

        this.GetComponent<Animation>().Play(); 
    }

    


    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Animation>().isPlaying)
        {
            if(PlayerOBJ != null)
            {
                PlayerOBJ.FinaleCutscene = true;
            }

            else if (PlayerOBJAA != null)
            {
                PlayerOBJAA.FinaleCutscene = true;
            }

            GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").GetComponent<Camera>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").transform.Find("FPSCam").GetComponent<Camera>().enabled = false;
        }



       else if (!FinaleReset)
        {
            FinaleReset = true;

            if (PlayerOBJ != null)
            {
                PlayerOBJ.FinaleCutscene = false;
            }

            else if (PlayerOBJAA != null)
            {
                PlayerOBJAA.FinaleCutscene = false;
            }

            GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").GetComponent<Camera>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").transform.Find("FPSCam").GetComponent<Camera>().enabled = false;
        }

      
        

        
        
    }


}
