using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{

    Race MyRace;

    bool Race_Active;
    int Current_Checkpoint;

    bool RaceComplete;

    public ParticleSystem NextObjective_Locater;

    private void Start()
    {
        Instantiate(NextObjective_Locater);
    }

    private void Update()
    {
        
    }


}
