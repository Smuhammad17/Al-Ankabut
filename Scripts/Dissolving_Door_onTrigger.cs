using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolving_Door_onTrigger : MonoBehaviour
{

    public GameObject ConnnectedOBJ;
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        //If Connected OBject is a Button
        if (ConnnectedOBJ.GetComponent<ButtonScript>())
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
        //end Button Communications


        //if Connected Object is a Switch
        if (ConnnectedOBJ.GetComponent<SwitchScript>())
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
        //End Connected Switch communications
    }


    bool AnimHasPlayed = false;
    void RemoveDoor()
    {
        if (!AnimHasPlayed)
        {
            anim.Play();
            AnimHasPlayed = true;
        }
        
        this.GetComponent<BoxCollider>().enabled = false;
    }

    void ActivateDoor()
    {
      
        anim.Rewind();
        anim.Play();
        AnimHasPlayed = false;
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
