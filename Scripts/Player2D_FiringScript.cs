using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D_FiringScript : MonoBehaviour
{

    public GameObject Bullet_2D;
    public Vector2 BulletDirection;

    public Transform Firepoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      




        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }


    void Fire()
    {
        GameObject Bullet = Instantiate(Bullet_2D, this.gameObject.transform.position, Quaternion.identity);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Firepoint.up * Bullet.GetComponent<BulletScript2D>().Force, ForceMode2D.Impulse); ;

    }

}
