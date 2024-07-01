using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript2D : MonoBehaviour
{

    public Vector2 direction;
    Rigidbody2D rgbd;

    public float Force;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = this.gameObject.GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
     
    }


 
}
