using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationGateScrip : MonoBehaviour
{
    public string LocationString;

    public void Start()
    {
        GameObject.FindGameObjectWithTag("LT").GetComponent<Text>().text = LocationString;
        GameObject.FindGameObjectWithTag("LT").GetComponent<Animation>().Play();
    }

   

}
