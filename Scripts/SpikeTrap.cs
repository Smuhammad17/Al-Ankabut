using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public int switch_delay;
    public bool Spike_Switch;

    public bool spikes_active;

    public bool offset;

    public GameObject Spikes;
    public GameObject Player;


    private void Start()
    {
        if (offset)
        {
            this.GetComponent<Animation>().Play("Spike Trap");
        }

        Player = GameObject.FindGameObjectWithTag("Player");

    }

    float PlayerDist;

    // Update is called once per frame
    void Update()
    {
        PlayerDist = Vector3.Distance(this.transform.position, Player.transform.position);

        if (PlayerDist < 40)
        {

            if (!Spike_Switch)
            {
                if (!offset)
                    StartCoroutine(Activate_Disable_Spikes());

                else
                {
                    StartCoroutine(Disable_Activate_Spikes());
                }
            }
        }
    }



    IEnumerator Activate_Disable_Spikes ()
    {
       
        Spike_Switch = true;

       

        yield return new WaitForSeconds(switch_delay);
        spikes_active = true;

        this.GetComponent<Animation>().Play("Spike Trap");

        yield return new WaitForSeconds(switch_delay);

        spikes_active = false;
        Spike_Switch = false;
        this.GetComponent<Animation>().Play("SpikeTrap Close");

    }


    IEnumerator Disable_Activate_Spikes()
    {

        Spike_Switch = true;

        yield return new WaitForSeconds(switch_delay);
      

        this.GetComponent<Animation>().Play("SpikeTrap Close");

        yield return new WaitForSeconds(switch_delay);

        Spike_Switch = false;

        spikes_active = true;
        this.GetComponent<Animation>().Play("Spike Trap");

    }

}
