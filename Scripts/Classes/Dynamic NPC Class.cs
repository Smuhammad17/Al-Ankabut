using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class DynamicNPCClass 
{

    public SpriteRenderer Render;
    public Animation CamAnim;
    public Sprite NPCICON;
    
    public string CurrentText;
    public Camera Cam;
    public string Name;
    public Dialogue[] Dialogues;
   
    public string PreviewDialogue;
    public string PostResurrectionPreview;
    public GameObject SpiderToSpawn;
    public bool SpiderInDisguise;
    public Transform SpiderSpawn;

    public bool Resurrected; 
    public Dialogue[] ResurrectedDialogues;




}






[System.Serializable]
public class Dialogue 
{


    [TextArea(3,10)]
    public string Sentence;


   
    public bool PlayerSpeaking;


    

    public Sprite Emotion;
    public bool Emotion_Active;

    public Challenge NPC_Challenge;
   
    public bool Challenge_Active;

    public ParticleSystem EMOTICON;
    public bool EMO_Active;

    public bool CamAnim_Active;
    public AnimationClip CamClip;

    public bool SpiderInDisguise;

 

    public void PlayDialogue(Animation CamAnim, SpriteRenderer Render) {

     
          
    


            if (EMO_Active)
            {
                EMOTICON.Play();
      
            }

        if (Emotion_Active)
            Render.sprite = Emotion;


        if (CamAnim_Active) {
            CamAnim.clip = CamClip;
            CamAnim.Play();

           }

        if (Challenge_Active)
        {
            NPC_Challenge.StartChallenge(NPC_Challenge.NPC_Challenge_Type);
        }

    }

}


[System.Serializable]
public class Challenge {

    public Dynamic_NPC_Script Recipient;
    public int mathIter;
    public Transform RequiredItem;
    public string RequisitionMessage;
    public int surIter;
    //To stop the challenge from being run multiple times
    public bool surChalRun;

    public enum ChallengeType
    {
        Survival, Math, Requisition
    }

    public ChallengeType NPC_Challenge_Type;

  

    public void StartChallenge(ChallengeType ct) {



        switch (ct)
        {
            case ChallengeType.Math:
             
                MathChallenge mchal = GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>();
                mchal.StartCoroutine(mchal.RunChallenge(mathIter, Recipient));
                break;

            case ChallengeType.Survival:
                if (!surChalRun)
                {
                    SurvivalChallengeScript schal = GameObject.FindGameObjectWithTag("SurvivalChallenge").GetComponent<SurvivalChallengeScript>();
                    schal.StartCoroutine(schal.RunChallenge(surIter, Recipient));
                    surChalRun = true;
                }

                break;

            case ChallengeType.Requisition:
                RequisitionChallenge rchal = new RequisitionChallenge();
                rchal = GameObject.FindGameObjectWithTag("RequisitionChallenge").GetComponent<RequisitionChallenge>();
                rchal.StartCoroutine(rchal.RunChallenge(RequiredItem, Recipient,RequisitionMessage));
                break;




        }
            
    
    
    }


}