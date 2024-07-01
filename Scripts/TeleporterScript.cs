using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{

    public bool PoweredON;
   
    public Transform Paired_Teleporter_Transport_Location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TalkNPC")
        {
            if (PoweredON)
            {
                TeleportPlayer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Cooldown());
    }


   IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TeleportCooldown = true;
    }

    public void TeleportPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TeleportCooldown)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(Paired_Teleporter_Transport_Location.transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, Paired_Teleporter_Transport_Location.transform.position.z);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TeleportCooldown = false;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
