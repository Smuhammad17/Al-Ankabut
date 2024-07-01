using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaithFieldCollision : MonoBehaviour
{
    //FaithField
    public float FaithFieldVal;
    public GameObject FaithFieldObj;
    public bool FaithFieldActive;

    public Image FaithFieldFillBar;
   
 

    public bool NeedToDestroy = false;
 
    GameObject Hcanv;
    GameObject HcanvTopLeftCorner;

    // Start is called before the first frame update
    void Start()
    {
       
        Hcanv = GameObject.FindGameObjectWithTag("LevelCanvas");
        FaithFieldVal = 100;
        FaithFieldObj.SetActive(false);
  
        FaithFieldFillBar = Hcanv.transform.Find("LevelCanvasBorder").transform.Find("FaithFieldFillBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        FaithFieldFillBar.fillAmount = (FaithFieldVal/100);
        
        
        if(FaithFieldVal > 100)
        {
            FaithFieldVal = 100;
        }


        else if (FaithFieldVal < 0)
        {
            FaithFieldVal = 0;
        }

        if(FaithFieldVal < 20)
        {
            FaithFieldActive = false;
        
        
      }


        if (FaithFieldVal > 80)
            FaithFieldFillBar.color = Color.green;

        else if (FaithFieldVal > 50)
            FaithFieldFillBar.color = Color.yellow;

        else 
        {
            FaithFieldFillBar.color = Color.red;
        }

        //Faith Field
       
        if (Input.GetMouseButtonDown(1) && FaithFieldVal > 80 && !FaithFieldActive)
        {
            FaithFieldActive = true;
            FaithFieldObj.GetComponent<Animation>().clip = FaithFieldObj.GetComponent<Animation>().GetClip("FaithField Build");
            FaithFieldObj.GetComponent<Animation>().Play();
        }

        if (FaithFieldActive)
        {
            FaithFieldVal -= 8f * Time.deltaTime;
            FaithFieldObj.SetActive(true);
            FaithFieldObj.GetComponent<SphereCollider>().enabled = true;
            NeedToDestroy = true;
        }

        else
        {
           
            FaithFieldVal += 0.5f * Time.deltaTime;
           if(NeedToDestroy)
            StartCoroutine(DestroyForceField());
        }
       

    

     


    }


    IEnumerator DestroyForceField()
    {
        if (NeedToDestroy)
        {
            FaithFieldObj.GetComponent<Animation>().clip = FaithFieldObj.GetComponent<Animation>().GetClip("FaithFieldDestroy");
            FaithFieldObj.GetComponent<Animation>().Play();
            
            
            FaithFieldObj.SetActive(false);
            //Dissolve shader doesn't work so I destroy the faith field without waiting
            FaithFieldObj.GetComponent<SphereCollider>().enabled = false;
            NeedToDestroy = false;
            yield return null;
        }
   }

  
}
