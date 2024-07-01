using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlAnkGate : MonoBehaviour
{
   public GameObject AnimCam;
    public Animation ObjAnim;

    private void Start()
    {
      
        AnimCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerPrefs.GetInt("GateToAlAnk") == 1)
        {
            StartCoroutine(DestroyGate());
        }
        
    }

    IEnumerator DestroyGate()
    {
        bool OffCam = true;

        if (OffCam)
        {
            GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").transform.Find("FPSCam").gameObject.SetActive(false);

            AnimCam.SetActive(true);
            AnimCam.GetComponent<Camera>().enabled = true;
        }



        AnimCam.GetComponent<Animation>().Play();

       ObjAnim.Play();
        yield return new WaitForSeconds(8f);

        GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.Find("FPSCam").gameObject.SetActive(true);
        PlayerPrefs.SetInt("GateToAlAnk", 0);
        Destroy(this.gameObject);



    }
}
