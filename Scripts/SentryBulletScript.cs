using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBulletScript : MonoBehaviour
{
    public Rigidbody rgbd;
    private float Force = 2;
    int randnum;

    private void Start()
    {
        randnum = Random.Range(0, 2);
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }

    void FixedUpdate()
    {
        rgbd = this.gameObject.GetComponent<Rigidbody>();

        

        if (randnum == 0)
        {
            rgbd.AddRelativeForce((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * Force * 7);
            rgbd.velocity = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * Force * 7;
        }

        else if (randnum == 1)
        {
          
            rgbd.AddRelativeForce(transform.forward * Force * 9);
            rgbd.velocity = transform.forward * Force * 9;

        }

        
        Destroy(this.gameObject, 2.9f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "NonRectEnemy" )
        Destroy(this.gameObject);
    }







}
