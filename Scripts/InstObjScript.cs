using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstObjScript : MonoBehaviour
{
    public Rigidbody rgbd;
    public bool Unhit = true;
    private float Force = 11;
    public float maxDist;
    public ParticleSystem CollisonParticles;

    void FixedUpdate()
    {
        float PlayerDist;
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));
        
        if(PlayerDist > maxDist)
        {
            Destroy(this.gameObject);
        }

        rgbd = this.gameObject.GetComponent<Rigidbody>();
        Transform PlayerParent = GameObject.FindGameObjectWithTag("Player").transform;
        if (Unhit)
        {
            rgbd.AddRelativeForce(PlayerParent.transform.forward * Force, ForceMode.Impulse);
            
        }

        else
        {
            Destroy(this.gameObject, 0.05f);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != this.tag)
        CollisonParticles.Play();
    
        if(other.gameObject.tag != "PlayerShot")
        Unhit = false;
    }
}
