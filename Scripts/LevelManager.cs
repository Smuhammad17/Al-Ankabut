using System.Collections;

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject loadingscreen;
    public Image Slider;

    public bool IsLoading;



    public string SAVED_CurrentLevel;

    


    public void Start()
    {
        loadingscreen.SetActive(false);
        Slider.enabled = false;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            loadingscreen.SetActive(false);
            Slider.enabled = false;
        }

        
        SAVED_CurrentLevel = PlayerPrefs.GetString("Saved_CurrentLevel");
        // Debug.Log(SAVED_CurrentLevel);


        if (GameObject.FindGameObjectsWithTag("Loading").Length >= 2)
        {
            // Debug.Log(" Loader Length: " + GameObject.FindGameObjectsWithTag("Loading").Length);
            Destroy(GameObject.FindGameObjectsWithTag("Loading")[0]);
        }
    }

 


 


    public void ContinueGame()
    {
       // Debug.Log(SAVED_CurrentLevel);

        if(SAVED_CurrentLevel != "")
        LoadScene(SAVED_CurrentLevel);
        
        else
        {
            PlayerPrefs.DeleteAll();
            LoadScene("TheWanderingSpider");
        }
    }




    public void LoadScene(string name)
    {
        if(!IsLoading)
        StartCoroutine(LoadAsyncronously(name));
       
    

        if(name != "MainMenu")
        PlayerPrefs.SetString("Saved_CurrentLevel", name);
    }

    IEnumerator LoadAsyncronously(string name)
    {
        IsLoading = true;
        
       
        DontDestroyOnLoad(this.gameObject);
        loadingscreen.GetComponent<Animation>().Play("Loading Screen pop up animation");
        loadingscreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        Slider.enabled = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Slider.fillAmount = progress;

            yield return null;
        };



        StartCoroutine(EndLoad());
        
    }

    IEnumerator EndLoad()
    {


        Slider.fillAmount += 0.4f * Time.deltaTime;
        yield return new WaitForSeconds(1f);
    
        Slider.GetComponent<Animation>().Play();
        loadingscreen.GetComponent<Animation>().Play("Loading Screen background animation");
        yield return new WaitForSeconds(3f);
        loadingscreen.SetActive(false);
        IsLoading = false;
        if (GameObject.FindGameObjectsWithTag("Loading").Length >= 2)
        {
          //  Debug.Log(" Loader Length: " + GameObject.FindGameObjectsWithTag("Loading").Length);
            Destroy(this.gameObject);
        }
    }
}
