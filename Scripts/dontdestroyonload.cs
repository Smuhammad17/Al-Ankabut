using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontdestroyonload : MonoBehaviour
{
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
        DontDestroyOnLoad(this.gameObject);
    }

 

}
