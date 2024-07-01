using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public bool activated;

    Animation anim;

    public GameObject Text;

    private void Start()
    {
        anim = this.GetComponent<Animation>();
        activated = false;

        if (activated)
        {
            anim.Play("SwitchReturn");
            activated = false;
        }


        Text.SetActive(false);
    }

    public void Update()
    {
        Text.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    public bool buttonPressed;

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "TalkNPC")
        {

            Text.SetActive(true);




            if (Input.GetKey("e"))
            {
                if (!buttonPressed)
                {
                    

                    if (!activated)
                    {

                        buttonPressed = true;
                        activated = true;
                        anim.Play("SwitchPress");
                        StartCoroutine(ButtonPressedReset());
                    }

                    else if (activated)
                    {
                        activated = false;
                        buttonPressed = true;
                        anim.Play("SwitchReturn");

                        StartCoroutine(ButtonPressedReset());
                    }

                }


            }

        }

      
    }

    IEnumerator ButtonPressedReset()
    {
        yield return new WaitForSeconds(0.3f);
        buttonPressed = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TalkNPC")
        {
            Text.SetActive(false);
        }
    }


}
