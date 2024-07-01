using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableKeyScript : MonoBehaviour
{

   public int keyID;
    
    public GameObject ObjText;
    public GameObject PlayerCam;

    public void Start()
    {
        ObjText.GetComponent<TextMesh>().text = keyID.ToString();
        ObjText.GetComponent<TextMesh>().fontSize = 193;

       PlayerCam = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").gameObject;
    }

    public void Update()
    {
      
        ObjText.gameObject.transform.LookAt(PlayerCam.transform);

        //ObjText.GetComponent<TextMesh>().text = keyID.ToString();
    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LockedDoor")
        {
           // Debug.Log("Met Door");

            if (other.gameObject.GetComponent<LockedDoorScript>().DoorID == keyID) {
                other.gameObject.GetComponent<LockedDoorScript>().StartCoroutine(other.gameObject.GetComponent<LockedDoorScript>().OpenDoor());
                this.GetComponent<Animation>().Play();
                Destroy(this.gameObject, 3);  


                    }
        }

       
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            ObjText.GetComponent<TextMesh>().text = "Hold Left Mouse Button to Pickup";
            ObjText.GetComponent<TextMesh>().fontSize = 150;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            ObjText.GetComponent<TextMesh>().text = keyID.ToString();
            ObjText.GetComponent<TextMesh>().fontSize = 193;
        }
    }




}
