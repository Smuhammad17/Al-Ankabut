using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenuOptions : MonoBehaviour
{
    public GameObject[] OtherPanels;
    public GameObject LevelSelectPanel;


    private void Start()
    {
        LevelSelectPanel.GetComponent<Canvas>().enabled = false;
    }

    public void Activate_LevelSelect()
    {
        for(int x = 0; x < OtherPanels.Length; x++)
        {
            OtherPanels[x].GetComponent<Canvas>().enabled = false;
        }
        LevelSelectPanel.GetComponent<Canvas>().enabled = true;


    }

  public  void DeActivate_LevelSelect()
    {
        for (int x = 0; x < OtherPanels.Length; x++)
        {
            if(OtherPanels[x].gameObject.name != "ConfirmationScreen")
            OtherPanels[x].GetComponent<Canvas>().enabled = true;

        }
        LevelSelectPanel.GetComponent<Canvas>().enabled = false;


    }
}
