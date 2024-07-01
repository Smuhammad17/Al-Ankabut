using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCClass
{
    public string Name;

    [TextArea(3,10)]
    public string[] Dialogue;

    //if the NPC requests a specific object
    public GameObject TargetObj;
    


}

