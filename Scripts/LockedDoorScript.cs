using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorScript : MonoBehaviour
{
   public int DoorID;
    public GameObject ObjText;

    GameObject PlayerCam;

    public void Start()
    {
        PlayerCam = GameObject.FindGameObjectWithTag("PlayerViewport");

        if (ObjText.GetComponent<TextMesh>().text != DoorID.ToString())
            ObjText.GetComponent<TextMesh>().text = DoorID.ToString();
    }


   

    public IEnumerator OpenDoor()
    {

        this.gameObject.GetComponent<Animation>().Play();
        this.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 3);
        yield return null;
    }
}
