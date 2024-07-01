using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOpeningScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameOpening());
    }

  
    IEnumerator GameOpening()
    {
        yield return new WaitForSeconds(12);
        if (GameObject.FindGameObjectWithTag("Loading"))
        {
            GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("Tutorial 2.0");
        }


            else {
                    SceneManager.LoadScene("Tutorial 2.0");
                }
    }

}
