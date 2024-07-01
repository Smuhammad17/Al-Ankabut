using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D_MovementScript : MonoBehaviour
{
    public float movement_speed = 10;
    Rigidbody2D rigid;
    Animator Anim;

    public SpriteRenderer PlayerSprite;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
       
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(new Vector2(Input.GetAxis("Horizontal"),0) * movement_speed);
        rigid.velocity = new Vector2(Input.GetAxis("Horizontal"), 0);


        //Walking Animation
        //Player Animation
        if (Input.GetAxis("Horizontal") != 0)
        {
            Anim.SetBool("IsWalking", true);


        }

        else { Anim.SetBool("IsWalking", false); }


        //Flip Sprite
        if (Input.GetAxis("Horizontal") < 0)
        {
            PlayerSprite.flipX = true;
       
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            PlayerSprite.flipX = false;
        }

    }



}
