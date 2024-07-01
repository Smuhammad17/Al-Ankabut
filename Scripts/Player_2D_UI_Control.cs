using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2D_UI_Control : MonoBehaviour
{
    public bool Cursorvisible = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Cursorvisible)
        {
            Cursor.visible = false;

        }
        
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
