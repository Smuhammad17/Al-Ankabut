using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSentryBullet : MonoBehaviour
{
     public Rigidbody rgbd;
    private float Force = 2;
    int randnum;
    int Spawns;
    int ExplosionForce;
    private void Start()
    {
        Spawns = 1;
       
        ExplosionForce = Random.Range(0,10);
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        rgbd = this.gameObject.GetComponent<Rigidbody>();
        Vector3 RedDirection = new Vector3((GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x), ExplosionForce, (GameObject.FindGameObjectWithTag("Player").transform.position.z - transform.position.z));
        rgbd.AddRelativeForce(RedDirection * Force, ForceMode.Impulse);
    }

    void FixedUpdate()
    {

        if(GameObject.FindGameObjectsWithTag("RedBullet").Length > 8)
        {
            for (int x = 0; x < GameObject.FindGameObjectsWithTag("RedBullet").Length; x++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("RedBullet")[x]);
            }
        }




        Destroy(this.gameObject, 10f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        randnum = Random.Range(0, 2);

        if (randnum == 1 && GameObject.FindGameObjectsWithTag("RedBullet").Length < 10 && Spawns >=1)
        {
            
           GameObject NewFriend = Instantiate(this.gameObject, this.gameObject.transform.position, Quaternion.identity);
            if(NewFriend.transform.localScale.x -0.3 > 0 )

       
            NewFriend.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            Spawns--;
            
        }

     
    }







}


