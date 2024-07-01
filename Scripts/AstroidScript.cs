using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AstroidScript : MonoBehaviour
{
    public int value;
    public TMP_Text ValueText;

    private SpriteRenderer sp;
    private CircleCollider2D col;
    private Rigidbody2D rigid;

    public Sprite[] Sprites;

    public float size = 1.0f;

    public float minSize = 2f;
    public float maxSize = 6.5f;

    public float Speed = 30f;
    public float minSpeed;
    public float maxSpeed;
    float lifetime = 30.0f;

    ContractManager_2D CM2D;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        Speed = Random.Range(minSpeed, maxSpeed);
        rigid = GetComponent<Rigidbody2D>();

        CM2D = GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractManager_2D>();
    }

    private void Start()
    {
        col.radius = 1 * size * 1.2f;

       

        //JUST FOR VISUALS, we'll give the asteroids a random trajectory 
        //this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);

        this.transform.localScale = Vector3.one * this.size;
        //sets mass to = size;
        rigid.mass = this.size;


        ValueText.text = value.ToString();
    }

    public void SetTrajectory(Vector3 Dir)
    {
        rigid.AddForce(Dir * Speed);

        Destroy(this.gameObject, lifetime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "2D_PlayerBullet")
        {
            if(value == CM2D.Contract.Results)
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);

                if (CM2D.ContractsToDo > 0)
                {
                    //CM2D.ContractsToDo--;
                    //CM2D.GiveNewContract();
                }
            }

            else
            {

            }
        }

       if(collision.gameObject.tag == "Player")
        {
            
        }
    }

}
