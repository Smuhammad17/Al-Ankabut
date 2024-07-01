using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public bool activated;
 
    Animation anim;

    private void Start()
    {
        anim = this.GetComponent<Animation>();
        activated = false;
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.tag == "TalkNPC" || other.tag == "Moveables")
        {
            this.GetComponent<AudioSource>().Play();
        }
    }

        private void OnTriggerStay(Collider other)
    {
        if (!activated && other.tag == "TalkNPC" || other.tag == "Moveables")
        {
            if (other.tag == "Moveables")
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            activated = true;
            anim.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag == "TalkNPC" || other.tag == "Moveables")
        {
            activated = false;
            anim.Rewind();
        }
    }



}
