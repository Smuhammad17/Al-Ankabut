using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MotivatorClass
{
    public TextMesh EnemyLevelText;
    public TextMesh EnemyValueText;

    public int EnemyValue = 0;
    public bool EnemyHit;


    //change color of Text as level increases
   public Color[] EnemyLevelColors = {
        
        //White LV1
        new Color(255f, 255f, 255f, 3f),
        //White LV2
        new Color(255f, 255f, 230f, 3f),
         //White LV3
        new Color(255f, 255f, 200f, 3f),
         //Yellow LV4
        new Color(255f, 255f, 150f, 3f),
          //Yellow LV5
        new Color(255f, 255f, 50f, 3f),
          //Yellow LV6
        new Color(255f, 255f, 0f, 3f),
            //RedishYellow LV7
        new Color(255f, 225f, 0f, 3f),
         //RedishYellow LV8
        new Color(255f, 200f, 0f, 3f),
         //RedishYellow LV9
        new Color(255f, 150f, 0f, 3f),
         //Red LV10
        new Color(255f, 100f, 0f, 3f),
         //Red LV11
        new Color(255f, 40f, 0f, 3f),
          //Red LV12
        new Color(255f, 0f, 0f, 3f),
    };


    public string[] EnemyRunningStatements = {
       "You'll Never take me alive!",
       "You can't catch me",
       "rebel!!!!",
       "Run away!!!",
       "You found me!",
       


    };
    

     public string LevelName
    {
        get
        {
           
            switch (level)
            {
                case 1:
                    return levelname = "Doubter";
                   
                    break;
                case 2:
                    return levelname = "Envier";
                   
                    break;
                case 3:
                    return levelname = "Disbeliever";
                    
                    break;
                case 4:
                    return levelname = "Unrighteous";
                    
                    break;
                case 5:
                    return levelname = "Procrastinator";
                    break;
                case 6:
                    return levelname = "Liar";
                    break;
                case 7:
                    return levelname = "Wicked";
                    break;
                case 8:
                    return levelname = "Iniquitous";
                    break;
                case 9:
                    return levelname = "Hypocrite";
                    break;
                case 10:
                    return levelname = "Jinn";
                    break;
                case 11:
                    return levelname = "Devil";
                    break;
                case 12:
                    return levelname = "Satanic";
                    break;

                default:
                    return levelname = "Unrated";


            }


        }



    }


}



   

