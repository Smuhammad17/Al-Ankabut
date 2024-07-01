using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatingDoor : MonoBehaviour
{
    public GameObject ConnnectedOBJ;
    Animation anim;
    public bool Activated;
    public bool Offset;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.Find("ActivatingDoor"))
        anim = transform.Find("ActivatingDoor").GetComponent<Animation>();

        if (transform.Find("WeightedDoor"))
        anim = transform.Find("WeightedDoor").GetComponent<Animation>();

        if (Offset)
        {
            Activated = true;
            ActivateDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ConnnectedOBJ.GetComponent<PhysicalCalculatorManager>())
        {
            if (!Offset)
            {

                if (ConnnectedOBJ.GetComponent<PhysicalCalculatorManager>().Open)
                {
                    ActivateDoor();
            
                }

                if (!ConnnectedOBJ.GetComponent<PhysicalCalculatorManager>().Open)
                {
                    RemoveDoor();
                }

            }


            else
            {

                if (ConnnectedOBJ.GetComponent<PhysicalCalculatorManager>().Open)
                {
                    ActivateDoor();
                }

                if (!ConnnectedOBJ.GetComponent<PhysicalCalculatorManager>().Open)
                {
                    RemoveDoor();
                }
            }
        }

        //If Connected OBject is a Button
        if (ConnnectedOBJ.GetComponent<ButtonScript>())
        {

            if (!Offset)
            {

                if (ConnnectedOBJ.GetComponent<ButtonScript>().activated)
                {
                 
                    RemoveDoor();
                }

                if (!ConnnectedOBJ.GetComponent<ButtonScript>().activated)
                {
                    ActivateDoor();
                }

            }


            else
            {

                if (ConnnectedOBJ.GetComponent<ButtonScript>().activated)
                {
                    ActivateDoor();
                }

                if (!ConnnectedOBJ.GetComponent<ButtonScript>().activated)
                {
                    RemoveDoor();
                }
            }

        }
        //end Button Communications


        //if Connected Object is a Switch
        if (ConnnectedOBJ.GetComponent<SwitchScript>())
        {
            if (!Offset)
            {

                if (ConnnectedOBJ.GetComponent<SwitchScript>().activated)
                {
                    RemoveDoor();
                }

                if (!ConnnectedOBJ.GetComponent<SwitchScript>().activated)
                {
                    ActivateDoor();
                }

            }


            else
            {

                if (ConnnectedOBJ.GetComponent<SwitchScript>().activated)
                {
                    ActivateDoor();
                }

                if (!ConnnectedOBJ.GetComponent<SwitchScript>().activated)
                {
                    RemoveDoor();
                }
            }
        }
        //End Connected Switch communications
    }


    bool AnimHasPlayed = false;
    
    void RemoveDoor()
    {
        if (!AnimHasPlayed)
        {
            anim.Play("Activating Door Return");
            AnimHasPlayed = true;
        }

    
    }


    void ActivateDoor()
    {
        if (AnimHasPlayed)
        {

            anim.Play("Activating Door");
            AnimHasPlayed = false;

        }
   
    }
}
