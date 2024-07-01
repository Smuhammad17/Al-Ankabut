using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundRect : MonoBehaviour
{

    public float movementSpeed = 160;


    private Vector3 Direction;

    public bool Horizontal;

    public Rigidbody rigid;

    private void Start()
    {

        if(!Horizontal)
        Direction = transform.forward;

        else
        {
            Direction = transform.right;
        }

        rigid.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        rigid.velocity = Direction;


        rigid.AddForce(Direction * movementSpeed);





    }

    void OnCollisionEnter(Collision other)
    {




        Direction = other.GetContact(0).normal;


    }
}

