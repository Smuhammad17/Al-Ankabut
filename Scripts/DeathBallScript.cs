using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBallScript : MonoBehaviour
{
    public float movementSpeed = 15;
    public float maxVelocity;
    public Rigidbody rigid;
    public int health = 10;

    public ParticleSystem Deatheffect;
    public AudioSource DamageSound;

    //Effects
    public Animation Anim;



    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        User = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();     
   
    }






    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if(rigid.velocity.x <= maxVelocity || rigid.velocity.z <= maxVelocity)
            {
                rigid.AddForce((other.transform.position - this.transform.position) * movementSpeed);
            }

        
        
        }

        if (other.gameObject.tag == "LegendaryWeapons")
        {
            if (PlayerPrefs.GetInt("Difficulty") > 1)
            {
                health -= 2;
                Deatheffect.Play();
                DamageSound.PlayOneShot(DamageSound.clip);
            }

            else
            {
                health -= 4;
                Deatheffect.Play();
                DamageSound.PlayOneShot(DamageSound.clip);
            }


        

            if (health <= 0)
            {
            Die();
            Deatheffect.Play();
            DamageSound.PlayOneShot(DamageSound.clip);
            }

        }


     }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();

            rigid.AddForce(collision.GetContact(0).normal * movementSpeed, ForceMode.Impulse);
        }

            if (collision.gameObject.tag == "PlayerShot" || collision.gameObject.tag == "Moveables")
            {
                if (PlayerPrefs.GetInt("Difficulty") > 1)
                {
                    health -= 1;
                    Deatheffect.Play();
                    DamageSound.PlayOneShot(DamageSound.clip);
                }

                else
                {
                    health -= 2;
                    Deatheffect.Play();
                    DamageSound.PlayOneShot(DamageSound.clip);
                }


            }

            if (health <= 0)
            {
                Die();
                Deatheffect.Play();
                DamageSound.PlayOneShot(DamageSound.clip);
            }
        }
    

    public void Attack()
    {
        Anim.CrossFade("DeathBallAttack");
     


    }

    public GameObject MoneyAwardNotificationOBJ;
    bool Destroying = false;

    Player User;
    public void Die()
    {
        if (!Destroying)
        {
            Destroying = true;
            User.StartCoroutine(User.AddMoney(40));
            GameObject MANOBJ6 = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
            MANOBJ6.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ6.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(40, "+"));



            // Anim.CrossFade("DeathBallDeath");
            Destroy(this.gameObject, 2);
            Deatheffect.Play();
        }
    }

}
