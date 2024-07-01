using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossScript : MonoBehaviour
{
    public float movementSpeed = 30;
    private Vector3 Direction;
    public Rigidbody rigid;
    public double distance;
    //Max distance till return directive
    public double maxDist;
    //float integer to decide direction after hitting a wall. 
    public int directFloat;
    //origin point
    private Vector3 Origin;
    //return directive
    public bool ReturnDirect;
    //LookinMovementDirection
    public GameObject SpiderMesh;

    public AudioSource DamageSound;
    public AudioClip[] DamageClip;


    //InstantiatedOBJ
    public GameObject Bullets;
    public GameObject Yellows;
    public Transform SpawnPoint;
    //player dist to sentry
    public float PlayerDist;
    public float shootRange;
    public float reloadTime;
    public float maxReloadTime;
    public float requiredReload;
    public float AmmoCost;

    //Health
    public int health;
    int maxhealth;
    public ParticleSystem Deatheffect;

    //Exp Payout For Kill
    public int Exp;


    public Image AlAnkabutLogo;
    public AudioSource DynamicEndMusic;
    

    // Start is called before the first frame update
    void Start()
    {
        Direction = transform.forward;
        rigid.GetComponent<Rigidbody>();
        Origin = transform.position;
        maxhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        float v = (float)((health - 0.01) / (maxhealth - 0.01));
        v = 1 - v;

        if (DynamicEndMusic.pitch <= 1 && v * 1.3f <= 1)
        {
            
            
            DynamicEndMusic.pitch = v * 1.3f;

        }

        else
        {
            DynamicEndMusic.pitch = 1;
        }

        Mathf.Clamp(DynamicEndMusic.pitch, v * 1.3f, 1);
       
        AlAnkabutLogo.fillAmount = v;

        rigid.AddForce(Direction * movementSpeed);
        rigid.velocity = Direction;
        //distance formula
        distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Origin.x, 2) + Mathf.Pow(transform.position.y - Origin.y, 2) + Mathf.Pow(transform.position.z - Origin.z, 2));
        //SHOOT PLAYER START
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        SpiderMesh.transform.LookAt(Player.transform.position);
        PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));

        reloadTime += 0.1f * Time.deltaTime;
        if (!ReturnDirect)
        {
            if (PlayerDist <= shootRange)
            {
                if (reloadTime >= requiredReload)
                {
                    Instantiate(Bullets, SpawnPoint.transform.position, Quaternion.identity);
                    Instantiate(Bullets, SpawnPoint.transform.position, Quaternion.identity);
                    Instantiate(Bullets, SpawnPoint.transform.position, Quaternion.identity);


                    Instantiate(Yellows, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(1,0,0)));
                    Instantiate(Yellows, SpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
             

                    reloadTime -= AmmoCost;
                }


            }

        }

        if (reloadTime >= maxReloadTime)
        {
            reloadTime = maxReloadTime;
        }
        //SHOOT PLAYER END



        //Return Directive START
        if (distance > maxDist)
        {
            ReturnDirect = true;

        }

        if (distance < 10)
        {
            ReturnDirect = false;
        }

        if (ReturnDirect)
        {


            transform.LookAt(Origin);
            Direction = transform.forward;

        }


    }




    void OnCollisionEnter(Collision other)
    {


        Direction = other.GetContact(0).normal;

        if (other.gameObject.tag == "PlayerShot" || other.gameObject.tag == "Moveables")
        {
            health -= 1;
            Deatheffect.Play();
            shootRange += 5;
            DamageSound.PlayOneShot(DamageClip[Random.Range(0,DamageClip.Length)]);
        }

        if (health <= 0)
        {
            Deatheffect.Play();


            SpiderMesh.gameObject.GetComponent<Animation>().Play();

            DestroySpider();


        }


    }

    void DestroySpider()
    {
        Destroy(this.gameObject, 1f);

        //Destroy(this.gameObject, 2f);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().myPlayer.Experience += Exp;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enemydest = true;
    }

}
