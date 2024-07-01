using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour
{

    public Material[] Materials; 



void Start()
    {

        RenderSettings.skybox = Materials[Random.Range(0, Materials.Length)];

    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1*Time.deltaTime, 1*Time.deltaTime);
    }
}
