using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MathChallenge : MonoBehaviour
{
    public Contract Equation;
    public Text Equation_Text; 
    public bool GameActive;
    public MathChallengeResponses[] ChallengeOptions;
    public MathChallengeResponses ChosenOption = null;
    public Player User;
    public int Money_Payout = 100;

    public Text remainingContracts_text;
    public int Iter;
    public Text Iter_text; 
    public bool AnswerGiven;
    public int UserAnswer;

    public int CorrectAnswers;
           int RequiredCorrects;
    public Image RC_slider;
    public Image Opposing_Slider;
    
    public bool PassedChallenge;
  

    public AudioSource Option_aud_source;
    public AudioClip Positive_aud;
    public AudioClip Negative_aud;

   public int PlayerCurrentHealth;

    public GameObject PauseMenu;

    public GameObject HCanv;

    public void Start()
    {

        GameActive = false;
        PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        HCanv = GameObject.FindGameObjectWithTag("LevelCanvas");
        User = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerCurrentHealth = User.Health;
      
    }


    public AudioSource Music;

    public bool exittrigger = false;

    public Collider[] PlayerColliders;

    public void Update()
    {
        if (GameActive)
        {
           exittrigger = true;

            User.GetComponent<FaithFieldCollision>().FaithFieldActive = true;
            User.GetComponent<FaithFieldCollision>().FaithFieldVal = 100;

            if (!Music.isPlaying)
            {
                Music.Play();
            }

            PlayerCurrentHealth = User.Health;
            User.Health = PlayerCurrentHealth;

            if(!User.FinaleCutscene)
           User.FinaleCutscene = true;

           this.gameObject.GetComponent<Canvas>().enabled = true;
            //Pause Menu Conflicts with this - This is a soft patch
            if (PauseMenu && PauseMenu.activeSelf)
            {
                PauseMenu.GetComponent<PauseMenuScript>().MenuIsOn = false;
            }
         
        }
        
        else
        {

            //Comment out this because it causes finale cutscene to be disabled when talking to NPCS (8/1/22)
            //User.FinaleCutscene = false;
            this.gameObject.GetComponent<Canvas>().enabled = false;

            //Pause Menu Conflicts with this - This is a soft patch
            if (GameObject.FindGameObjectWithTag("PauseMenu") && !GameObject.FindGameObjectWithTag("PauseMenu").activeSelf)
            {
                GameObject.FindGameObjectWithTag("PauseMenu").SetActive(true);
            }

            if (exittrigger)
            {
               exittrigger = false;
                Music.Stop();

                User.GetComponent<FaithFieldCollision>().FaithFieldActive = false;



            }

          

        }

        RC_slider.fillAmount = (float)((CorrectAnswers - 0.0001) / (RequiredCorrects - 0.0001));
        Opposing_Slider.fillAmount = 1 - RC_slider.fillAmount;

     

       
       

    }

    int x = 0;
    public bool negChallenge_ActivateOnce = false;
    public IEnumerator RunChallenge(int its, Dynamic_NPC_Script NPCResponse)
    {
        Iter = its;
        
        if (!NPCResponse.Dynamic_NPC.Resurrected)
        {
            GameActive = true;
         

            CorrectAnswers = 0;


            RequiredCorrects = Iter;

            negChallenge_ActivateOnce = false;

            while (x < Iter)
            {
                AnswerGiven = false;
                //Challenge Status
                if(x >= -5)
                remainingContracts_text.text = "<color=aqua>Contracts Remaining:</color> " + (Iter-x).ToString() + "\n";

                else
                {
                    remainingContracts_text.text = "<color=red>Warning! Danger zone reached!:</color> " + (Iter-x).ToString() + "\n";
                }

                ChallengeIteration();

                while (!AnswerGiven)
                {

                    
                    yield return new WaitForSeconds(1);

                    for (int y = 0; y < ChallengeOptions.Length; y++)
                    {
                        if (ChallengeOptions[y].Pressed)
                        {
                            ChosenOption = ChallengeOptions[y];
                            UserAnswer = ChosenOption.Option_value;
                            AnswerGiven = true;

                            //ResetPressed
                            for (int z = 0; z < ChallengeOptions.Length; z++)
                                ChallengeOptions[z].Pressed = false;
                        }

                    }

                    
                }

                if (AnswerGiven && UserAnswer == Equation.Results)
                {
              
                    ChosenOption.Positive();

                 


                    Option_aud_source.clip = Positive_aud;
                    Option_aud_source.Play();

                    x++;
                    CorrectAnswers = x;

                    //ResetPressed
                    for (int z = 0; z < ChallengeOptions.Length; z++)
                        ChallengeOptions[z].Pressed = false;


                    for (int f = 0; f < ChallengeOptions.Length; f++)
                    {

                        ChallengeOptions[f].Positive();

                    }
                }

                else if (AnswerGiven && UserAnswer != Equation.Results)
                {

                  
                    Option_aud_source.clip = Negative_aud;
                    Option_aud_source.Play();
                    ChosenOption.Negative();

                    //Incorrect number
                    if (x >= -5)
                    {
                        x--;
                        CorrectAnswers = x;
                    }

                    //Player reached danager zone
                    else
                    {
                        GameActive = false;
              
                        PassedChallenge = false;
                        GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Animation>().Play("Challenge_Celebrations_Negative");
                        GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Celebration_SoundManager>().negativePlay();
                        //End the Loop
                        Exit();
                        x = 0;
                    }
                   
                }

                else
                {
                
                    yield return new WaitForSeconds(1);
                }
            }


            GameActive = false;
            
            //Win Challenge Information
            if (CorrectAnswers >= RequiredCorrects)
            {
                PassedChallenge = true;

                User.FinaleCutscene = false;
                Iter = 0;
                CorrectAnswers = 0;
                x = 0;
                AnswerGiven = false;
                GameActive = false;

                //Give Money
                User.StartCoroutine(User.AddMoney(Money_Payout));


                GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Animation>().Play("Challenge_Celebrations_Positive");
                GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Celebration_SoundManager>().positivePlay();
                //Load first resurrected dialogue after challenge completion
                NPCResponse.StartCoroutine(NPCResponse.TypeSentence(NPCResponse.Dynamic_NPC.ResurrectedDialogues[0].Sentence));

                //Rememberance for each Dynamic NPC
                if (PlayerPrefs.HasKey(NPCResponse.gameObject.name))
                {
                    PlayerPrefs.SetInt(NPCResponse.gameObject.name, 1);
                }

            }

            else
            {
                PassedChallenge = false;
                Exit();
               
                    GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Animation>().Play("Challenge_Celebrations_Negative");
                
            }

            NPCResponse.Dynamic_NPC.Resurrected = PassedChallenge;
            NPCResponse.x = 0;
         
        }

     
    }

    public void ChallengeIteration()
    {
      
        Equation.GenerateContract();
     
        Equation_Text.GetComponent<Animation>().Play("MathChallenge_Equation_PopAnim");
        switch (Equation.ArithmeticOp)
            {
                case Contract.Arithmetic.Add:
                    Equation_Text.text = Equation.FV.ToString() + " + " + Equation.SV.ToString();
                    break;

                case Contract.Arithmetic.Subtract:
                    Equation_Text.text = Equation.FV.ToString() + " - " + Equation.SV.ToString();
                    break;

                case Contract.Arithmetic.Multiply:
                    Equation_Text.text = Equation.FV.ToString() + " X " + Equation.SV.ToString();
                    break;
                case Contract.Arithmetic.Divide:
                    Equation_Text.text = Equation.FV.ToString() + " / " + Equation.SV.ToString();
                    break;
                case Contract.Arithmetic.Modulo:
                    Equation_Text.text = Equation.FV.ToString() + " % " + Equation.SV.ToString();
                    break;

            }

        int RandomAnswer;
        RandomAnswer = Random.Range(0, ChallengeOptions.Length);

        for (int y = 0; y < ChallengeOptions.Length; y++)
          {



            if (y != RandomAnswer)
            {
                ChallengeOptions[y].Option_Button = ChallengeOptions[y].Option.GetComponent<Button>();
                int RanRange = 0;
                if (Random.Range(0, 1) == 1)
                {
                    RanRange = Random.Range(-Equation.Range, Equation.Range);

                }

                else
                {
                    //Random Sign used to make the other options more complicated than single integers
                    int RanSign = Random.Range(0, 2);
                    switch (RanSign)
                    {
                        case 0:
                            RanRange = Random.Range(-Equation.Range + -Equation.Range, Equation.Range + Equation.Range);
                            break;
                        case 1:
                            RanRange = Random.Range(-Equation.Range - -Equation.Range, Equation.Range - Equation.Range * 2);
                            break;

                        case 2:
                            RanRange = Random.Range(-Equation.Range * Equation.Range, Equation.Range * Equation.Range);
                            break;
                    }
                }
                ChallengeOptions[y].Options_Text.text = RanRange.ToString();
                ChallengeOptions[y].Option_value = RanRange;

             

            }
            else
            {
                ChallengeOptions[y].Options_Text.text = Equation.Results.ToString();
                ChallengeOptions[y].Option_value = Equation.Results;
              
            }


        }
      
                   


    }

    public void ReturnAnswer(int answer)
    {

        AnswerGiven = true;
        UserAnswer = answer;

    }

    public void Exit()
    {
        User.FinaleCutscene = false;
        Iter = 0;
        CorrectAnswers = 0;
        x = 0;        
        AnswerGiven = false;
        GameActive = false;
        PassedChallenge = false;

        HCanv.GetComponent<Canvas>().enabled = true;

        GameObject.FindGameObjectWithTag("Celebrations").GetComponent<Animation>().Play("Challenge_Celebrations_Negative");
    }



}
