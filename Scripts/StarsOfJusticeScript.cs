using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsOfJusticeScript : MonoBehaviour
{
    public Rigidbody rgbd;
    public bool Unhit = true;
    private float Force = 11;
    public float maxDist;
    public ParticleSystem CollisonParticles;
    public Transform ClosestObj;

    public List<GameObject> Spiders = new List<GameObject>();

    private void Start()
    {
        
        foreach(GameObject gSpider in GameObject.FindGameObjectsWithTag("GreenSpider"))
        {
            Spiders.Add(gSpider);

        }

        foreach (GameObject rSpider in GameObject.FindGameObjectsWithTag("RedSpider"))
        {
            Spiders.Add(rSpider);

        }

        foreach (GameObject bSpider in GameObject.FindGameObjectsWithTag("BlueSpider"))
        {
            Spiders.Add(bSpider);

        }

        foreach (GameObject ySpider in GameObject.FindGameObjectsWithTag("YellowSpider"))
        {
            Spiders.Add(ySpider);

        }

        foreach (GameObject wSpider in GameObject.FindGameObjectsWithTag("WanderingSpider"))
        {
            Spiders.Add(wSpider);

        }

        foreach (GameObject pSpider in GameObject.FindGameObjectsWithTag("PurpleSpider"))
        {
            Spiders.Add(pSpider);

        }

        foreach (GameObject pSpider in GameObject.FindGameObjectsWithTag("Thief Spider"))
        {
            Spiders.Add(pSpider);

        }


        findClosest();

       transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));

    }

   void findClosest()
    {
        float nearestDist = float.MaxValue;

        foreach(GameObject Obj in Spiders)
        {
            if( Vector3.Distance(this.transform.position, Obj.transform.position) < nearestDist)
            {
                nearestDist = Vector3.Distance(this.transform.position, Obj.transform.position);
                ClosestObj = Obj.transform;
            }

        }


    }



    void FixedUpdate()
    {
        float PlayerDist;
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerDist = Vector3.Distance(this.transform.position, Player.transform.position);

        //Delete if there's too many of this gameobject
        if (GameObject.FindGameObjectsWithTag(this.gameObject.tag).Length > 20)
        {
            Destroy(this.gameObject);
        }

        rgbd = this.gameObject.GetComponent<Rigidbody>();
      
        if (Unhit && ClosestObj != null)
        {
            rgbd.AddRelativeForce((ClosestObj.transform.position - transform.position).normalized * Force);
            rgbd.velocity = (ClosestObj.transform.position - transform.position).normalized * Force;

        }

        else
        {
            Destroy(this.gameObject, 9f);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != this.tag)
            CollisonParticles.Play();

        if (other.gameObject.tag != "PlayerShot")
            Unhit = false;
    }


}
