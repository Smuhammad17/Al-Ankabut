using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamePortalScript : MonoBehaviour
{
    public string ConstructName;
    public string LevelToLink;

 
  
    [TextArea(4, 12)]
    public string ConstructDescription;

    public GameObject PressENotif;

    // Update is called once per frame
    void Start()
    {
        PressENotif.GetComponent<TextMesh>().text = ConstructName;

     
    }

    private void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TalkNPC")
        {
            PressENotif.GetComponent<TextMesh>().text = ConstructName;
        }
    }
    
}
 
 