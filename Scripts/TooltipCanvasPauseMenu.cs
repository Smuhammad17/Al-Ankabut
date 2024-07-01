using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipCanvasPauseMenu : MonoBehaviour
{

    public GameObject[] Screens;
    public int x = 1;
    GameObject UI;


    // Start is called before the first frame update
    void Start()
    {
        UI = this.gameObject;

        UI.SetActive(false);

       

        
    }


    bool keypressed;

    // Update is called once per frame
    void Update()
    {




        if (UI.activeSelf && Input.GetKey("e") && !keypressed && x < Screens.Length)
        {
            keypressed = true;
            x++;
        }

        if (UI.activeSelf &&  Input.GetKey("q") && !keypressed && x > 0)
        {
            keypressed = true;
            x--;
        }

        if (UI.activeSelf &&  Input.GetKeyUp("e") || Input.GetKeyUp("q") && keypressed)
        {
            keypressed = false;
        }


        for (int y = 0; y <= Screens.Length - 1; y++)
        {
            if (y != x)
            {
                Screens[y].gameObject.SetActive(false);
            }

            else
            {
                Screens[y].gameObject.SetActive(true);
            }
        }


        if (UI.activeSelf &&  x < 0)
        {
            x = 0;
        }

        else if (UI.activeSelf &&  x > Screens.Length - 1)
        {
            Exit();
            x = 0;

        }
    }

    public void Exit()
    {

        UI.SetActive(false); 
        
    }

    public void OpenTooltip()
    {
        UI.SetActive(true);
        x = 0;


    }
}
