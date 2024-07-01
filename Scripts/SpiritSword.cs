using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSword : MonoBehaviour
{
    public Rigidbody rigid;
    public float rotationSpeed = 10f;
    public float RotationTime = 25f;
    public float forwardSpeed = 3f;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {

        rigid = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(SwordCycle());
    }

    // Update is called once per frame
    void Update()
    {
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
     
        if(GameObject.FindGameObjectsWithTag(this.gameObject.tag).Length > 15)
        {
            Destroy(this.gameObject);
        }


     

            rigid.AddForce(Player.forward * forwardSpeed);
            rigid.velocity = Player.forward * forwardSpeed;
        
       

        rigid.AddRelativeTorque(new Vector3(0, 1, 0) * rotationSpeed);

        float PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));

        if (PlayerDist > 12)
        {
            rigid.AddForce((Player.position - transform.position) * forwardSpeed *1.5f);
        }

    }

 public IEnumerator SwordCycle()
    {

        yield return new WaitForSeconds(RotationTime);
        Destroy(this.gameObject);
    }



}
