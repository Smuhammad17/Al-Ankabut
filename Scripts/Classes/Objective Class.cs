using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ObjectiveClass 
{
    public string AreaLabel;
    public Objective[] Objectives;
    public bool AllObjectivesComplete_Global = false;



    public string CheckComplete(Objective ob)
    {
        ob.CheckCompletion();
            if (ob.ObjectiveComplete)
            {
                return "<color=yellow>(Complete!)</color>";
            }

        

        return null;

    }


    public void CheckAllComplete()
    {
        bool isComplete = true;
        for(int x = 0; x < Objectives.Length; x++)
        {
            if (!Objectives[x].ObjectiveComplete)
            {
                isComplete = false;
            }
        }

        AllObjectivesComplete_Global = isComplete;
    }

       
}

    [System.Serializable]
    public class Objective 
    {
        [TextArea(3,10)]
        public string ObjLabel;
        public Dynamic_NPC_Script ConnectedNPC;
        public bool ObjectiveComplete;

        public void CheckCompletion()
        {
            if (ConnectedNPC.Dynamic_NPC.Resurrected)
            {
                ObjectiveComplete = true;
            }
        }
    }

