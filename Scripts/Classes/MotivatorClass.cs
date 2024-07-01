using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotivatorClass
{
    private int experience;
    public string levelname;

 

    public int Experience
    {

        //by ommitting get or set we can make the property write or read only.
        get
        {
            //we can treat these like functions
            return experience;

        }

        set
        { experience = value; }
    }

    public int level
    {
        get
        {
            return experience / 1000;
        }

        set
        {
            if (value > 12)
            {
                //Debug.Log("level can't be set below 1 or above 12");
                value = 12;
            }

            else if (value < 1)
            {
               // Debug.Log("level can't be set below 1 or above 12");
                value = 1;
            }

            else
            {
                experience = value * 1000;
              //  Debug.Log("The level has been set to" + experience / 1000);
            }
        }
    }

   

};

