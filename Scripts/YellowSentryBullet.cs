using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSentryBullet : MonoBehaviour
{
    public Rigidbody rgbd;
    private float Force = 2;
    int randnum;
    int ExplosionForce;

    private void Start()
    {
        rgbd = this.gameObject.GetComponent<Rigidbody>();
        ExplosionForce = Random.Range(0, 10);
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        rgbd = this.gameObject.GetComponent<Rigidbody>();
        Vector3 RedDirection = new Vector3((GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x), ExplosionForce, (GameObject.FindGameObjectWithTag("Player").transform.position.z - transform.position.z));
        rgbd.AddRelativeForce(RedDirection * Force, ForceMode.Impulse);



        InvokeRepeating("Duplicate", 1, 1);
    }

    void Duplicate()
    {

        if (GameObject.FindGameObjectsWithTag("YellowBullet").Length > 15)
        {
            for (int x = 0; x < GameObject.FindGameObjectsWithTag("YellowBullet").Length; x++)
                Destroy(GameObject.FindGameObjectsWithTag("YellowBullet")[x].gameObject);
        }
        else
        {

            Instantiate(this.gameObject, transform.position, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
       


        rgbd = this.gameObject.GetComponent<Rigidbody>();



     
            rgbd.AddRelativeForce(transform.forward * Force * 9);
            rgbd.velocity = transform.forward * Force * 9;

        


        Destroy(this.gameObject, 2.9f);


   

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "NonRectEnemy")
            Destroy(this.gameObject);
    }


}
