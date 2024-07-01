using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPConstructBlockRotate : MonoBehaviour
{
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxis("Vertical") > 0 || (Input.GetAxis("Horizontal") > 0))
        transform.Rotate(Vector3.up);

        else if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0 )
            { transform.Rotate(-Vector3.up); };
        
    }
}
