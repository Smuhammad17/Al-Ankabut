using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ReboundSphereScript : MonoBehaviour
{

    public float movementSpeed;
    public float RunSpeed;

    private Vector3 Direction;

    public Rigidbody rigid;
    public EnemyScript Enemy;

   

    // Start is called before the first frame update
    void Start()
    {
        Direction = transform.forward;
     
        rigid.GetComponent<Rigidbody>();
        Enemy = this.gameObject.GetComponent<EnemyScript>();
       // movementSpeed = (float)(movementSpeed + Enemy.GetComponent<EnemyScript>().EC.level * 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float PlayerDist;
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));

        rigid.velocity = Direction;

        if (!Enemy.EC.EnemyHit)
        {
            rigid.AddForce(Direction * movementSpeed);
           
        }
        else
        {
            rigid.AddForce(Direction * RunSpeed);
           
        }

       
    }

    void OnCollisionEnter(Collision other)
    {



        if (Enemy.EC.EnemyHit)
        {
            Direction = new Vector3(Mathf.Lerp(Mathf.Sin(other.GetContact(0).normal.x), Mathf.Cos(other.GetContact(0).normal.x),Time.deltaTime), 0, Mathf.Lerp(Mathf.Sin(other.GetContact(0).normal.z), Mathf.Cos(other.GetContact(0).normal.z), Time.deltaTime));
        }

        else
                {
                    Direction = other.GetContact(0).normal;
                }
      
    }

    




}
