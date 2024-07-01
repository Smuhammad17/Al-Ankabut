using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Advanced_Combat_Engagement : MonoBehaviour
{
    [SerializeField]
    int Skill_Multiplier = 0;
    [SerializeField]
    float Multiplier_Cooldown = 0f;
    [SerializeField]
    float Multiplier_Cooldown_Max = 2f;
    [SerializeField]
    bool BeginCooldown = false;

    //The player should lose time slower at the end and faster near the max so that they can easily build up meter
    float VariableDeacceleration = 0.1f;


    //UI ELEMENTS FOR Skill Multiplier
    public GameObject Hcanv;
    public Image Skill_Multiplier_FillBar;
    public Text Skill_Multiplier_LevelText;

    //Player for effects
    Player PlayerElements;
    //Combat engagement Elements
    Combat_Engagement CE;

    //Color Normalization
     float normalized_scale;

    // Start is called before the first frame update
    void Start()
    {
        PlayerElements = this.GetComponent<Player>();

        Hcanv = GameObject.FindGameObjectWithTag("LevelCanvas");
        Skill_Multiplier_FillBar = Hcanv.transform.Find("SkillMultiplier").transform.Find("SkillMultiplier_FillBar").GetComponent<Image>();

        Skill_Multiplier_LevelText = Hcanv.transform.Find("SkillMultiplier").transform.Find("SkillMultiplier_Star").transform.Find("Text").GetComponent<Text>();
    }

    public int GetSkillMultiplier()
    {
        return Skill_Multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Multiplier_Cooldown <= 0.10)
        {
            if (Skill_Multiplier > 0)
            {
                Skill_Multiplier -= 1;
                Multiplier_Cooldown = (Multiplier_Cooldown_Max - 0.5f);



            }

            else
            {
                Skill_Multiplier = 0;

                BeginCooldown = false;
            }

        }

        if (Skill_Multiplier >= 4)
        {
            Skill_Multiplier = 3;
            Multiplier_Cooldown = 0f;
            Multiplier_Cooldown_Max = 2f;
        }

        //The player should lose time slower at the end and faster near the max so that they can easily build up meter
        if (Multiplier_Cooldown < 0.5)
            VariableDeacceleration = 0.05f;

        else if (Multiplier_Cooldown > (Multiplier_Cooldown - 0.90))
            VariableDeacceleration = 0.7f;


        //Reduce Buildup if no spider is being killed
        if (BeginCooldown)
            Multiplier_Cooldown -= VariableDeacceleration * Time.deltaTime;


        if (Multiplier_Cooldown < 0)
            Multiplier_Cooldown = 0;

        else if (Multiplier_Cooldown > Multiplier_Cooldown_Max)
            Multiplier_Cooldown = Multiplier_Cooldown_Max;


        //Update Level Canvas Fill Bar
        Skill_Multiplier_LevelText.text = Skill_Multiplier.ToString();
        Skill_Multiplier_FillBar.fillAmount = (Multiplier_Cooldown / Multiplier_Cooldown_Max);

        //Make a new color as the fill bar expands it turns green
        //Use normalization to put 0-255 on the 0-1 scale
        normalized_scale = ((Multiplier_Cooldown / Multiplier_Cooldown_Max) * 255);

        Skill_Multiplier_FillBar.color = new Color(100, normalized_scale, 0);


        if (Skill_Multiplier == 0)
        {
            PlayerElements.UpgradeEffects.Stop();
        }



    }

    public void Add_Multiplier_Cause_An_Enemy_Destroyed()
    {
        Multiplier_Cooldown += 1f;
        //Begin the cooldown when an enemy is destroyed
        BeginCooldown = true;
        if(Multiplier_Cooldown >= (Multiplier_Cooldown_Max - 0.50))
        {
            if(Skill_Multiplier < 4)
            Skill_Multiplier += 1;
            PlayerElements.HealthParticles.Play();

            AddBuffs();
        }


       

   
    }

    public void AddBuffs()
    {
        switch (Skill_Multiplier)
        {

            case 0:
                PlayerElements.UpgradeEffects.Stop();
                break;

            case 1:
                //Set Player elements Multiplier skill buff so that base speed won't be reset while the buff is active
          
                PlayerElements.UpgradeEffects.Play();
               
                break;
        }
    }
}
