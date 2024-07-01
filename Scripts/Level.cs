using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
   public Image LockSymbol;
    public string LevelToLoad;
  

   public bool Unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
       if(this.gameObject.name == "Arena")
        {
            Unlocked = true;
        }

        if (!PlayerPrefs.HasKey(LevelToLoad) && this.gameObject.name != "Arena")
            PlayerPrefs.SetInt(LevelToLoad, 0);

       
        if(PlayerPrefs.GetInt(LevelToLoad) == 1 && this.gameObject.name != "Arena")
        Unlocked = true;
        

        else
        {
            Unlocked = false;
        }


        if (Unlocked)
        LockSymbol.enabled = false;
        else
        {
            LockSymbol.enabled = true;
        }
        

    }
    

    public void LoadLevel()
    {
        if (Unlocked)
        GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene(LevelToLoad);
    }
}
