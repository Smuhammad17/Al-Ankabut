using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberOfTheSureTruth : MonoBehaviour
{
    Rigidbody Rigid;
    HingeJoint HJ;

  
    Vector3 StartRot;


    // Start is called before the first frame update
    void Start()
    {
        Rigid = this.GetComponent<Rigidbody>();
        HJ = this.GetComponent<HingeJoint>();
        HJ.connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        StartRot = new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z);
        
    }




    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("SwordTrue", 0f);
            
        }

        //transform.rotation = Quaternion.Euler(0, -90, 0);
    }
}
