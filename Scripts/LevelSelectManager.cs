using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectManager : MonoBehaviour
{



    public void Start()
    {

       

      

        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);

        else
        {
            if(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) != 1)
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        }
    }

}


