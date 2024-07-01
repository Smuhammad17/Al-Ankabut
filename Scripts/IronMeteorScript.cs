using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMeteorScript : MonoBehaviour
{
    public Rigidbody rgbd;
    public bool Unhit = true;
    private float Force = 11;
    public float maxDist;
    public ParticleSystem CollisonParticles;

    void FixedUpdate()
    {
       

        rgbd = this.gameObject.GetComponent<Rigidbody>();
        Transform PlayerParent = GameObject.FindGameObjectWithTag("Player").transform;
        if (Unhit)
        {
            rgbd.AddRelativeForce(PlayerParent.transform.forward * Force, ForceMode.Impulse);
            rgbd.AddRelativeForce(Vector3.down * Force / 2, ForceMode.Impulse);
        }

        else
        {
            Destroy(this.gameObject, 2f);
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
