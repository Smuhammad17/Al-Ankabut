using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class POTHBClass
{
    public int ID;
    public bool Collected;
    public string Verse;

    [TextArea(3, 10)]
    public string[] Description;

    

}
