using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitConstructLookScript : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    
}
