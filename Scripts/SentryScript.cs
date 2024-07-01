using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.PostProcessing;

public class SentryScript : MonoBehaviour
{
   
    public bool TutorialSpider;
    public float movementSpeed = 30;
    public float MaxVelocity = 7;
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


    public GameObject MoneyAwardNotificationOBJ;

    //InstantiatedOBJ
    public GameObject Bullets;
    public GameObject RedBullet;
    public GameObject YellowBullet;
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
 
    public ParticleSystem Deatheffect;

    //Exp Payout For Kill
    public int Exp;

    public AudioSource DamageSound;
    public AudioClip[] DamageClips;
    public AudioSource FiringSound;


    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");

        Direction = transform.forward;
        rigid.GetComponent<Rigidbody>();
        Origin = transform.position;
        canAdvancedMove = false;
        StartCoroutine(AdvancedMovementCooldown());
  
    }

    public IEnumerator AdvancedMovementCooldown()
    {
        yield return new WaitForSeconds(2f);
        canAdvancedMove = true;
    }


    public bool canAdvancedMove = false;
    public bool AdvancedMovementPlaying = false;
    
    public IEnumerator AdvancedMovement()
    {
        if (canAdvancedMove)
        {
            canAdvancedMove = false;

            AdvancedMovementPlaying = true;
            int ranRange = Random.Range(1, 2);
            if (ranRange == 1)
            {


                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                Direction = (Player.transform.position - transform.position).normalized * 4;


            }

            else
            {
                Direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
            }

            if (rigid.velocity.magnitude > MaxVelocity)
            {
                rigid.velocity = rigid.velocity.normalized * MaxVelocity;
            }

            yield return new WaitForSeconds(5);
            AdvancedMovementPlaying = false;
            canAdvancedMove = true;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

       
        if (!AdvancedMovementPlaying && !ReturnDirect)
            StartCoroutine(AdvancedMovement());
        if (health > 0)
        {
            rigid.AddForce(Direction * movementSpeed);

            rigid.velocity = Direction / 8;

        }

        if (rigid.velocity.magnitude > MaxVelocity)
        {
            rigid.velocity = rigid.velocity.normalized * MaxVelocity;
        }

        //distance formula
        distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Origin.x, 2) + Mathf.Pow(transform.position.y - Origin.y, 2) + Mathf.Pow(transform.position.z - Origin.z, 2));


            //SHOOT PLAYER START
            
            SpiderMesh.transform.LookAt(Player.transform.position);
            PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));
            
            reloadTime += 0.1f * Time.deltaTime;
           
        if (!ReturnDirect)
            {
                if (PlayerDist <= shootRange)
                {
                    if (reloadTime >= requiredReload)
                    {
                        

                            int RedSpiderBulletChance = Random.Range(0, 2);
                            int YellowSpiderBulletChance = Random.Range(0, 2);

                            if (RedSpiderBulletChance == 1 && (this.gameObject.tag == "RedSpider" || this.gameObject.tag == "WanderingSpider"))
                            {
                                Instantiate(RedBullet, SpawnPoint.transform.position, Quaternion.identity);
                                reloadTime = 0;
                                if (!FiringSound.isPlaying)
                                    FiringSound.Play();
                            }


                            else if (YellowSpiderBulletChance == 1 && (this.gameObject.tag == "YellowSpider" || this.gameObject.tag == "WanderingSpider"))
                            {


                                Instantiate(YellowBullet, SpawnPoint.transform.position, Quaternion.identity);

                                reloadTime = 0;



                                if (!FiringSound.isPlaying)
                                    FiringSound.Play();
                            }


                            else if (RedSpiderBulletChance == 0 || YellowSpiderBulletChance == 0 || this.gameObject.tag != "RedSpider" || this.gameObject.tag != "YellowSpider")
                            {
                                for (int x = 0; x < Random.Range(3, 6); x++)
                                {
                                    Instantiate(Bullets, SpawnPoint.transform.position, Quaternion.identity);

                                }
                                reloadTime -= AmmoCost;

                                if (!FiringSound.isPlaying)
                                    FiringSound.Play();

                            }
                        
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
        //DON'T BE SO SERIOUS
        else
        {


        }

        //Return Directive END

        //Destroy enemy on player dist > 40
        if (PlayerDist >= 75 && !TutorialSpider)
        {
            
            Destroy(this.gameObject, 4f);
        }


    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "LegendaryWeapons")
        {
        
              if (PlayerPrefs.GetInt("Difficulty") > 1)
                {
                    health -= 2;

                }

                else
                {
                    health -= 4;

                }

                Deatheffect.Play();
                DamageSound.PlayOneShot(DamageSound.clip);

                shootRange += 10;
         
            if (health <= 0)
            {
                Deatheffect.Play();

                DamageSound.PlayOneShot(DamageClips[Random.Range(0, DamageClips.Length)]);
                SpiderMesh.gameObject.GetComponent<Animation>().Play();

                DestroySpider();


            }

        }
    }


    void OnCollisionEnter(Collision other)
    {


        Direction = other.GetContact(0).normal;

        if (other.gameObject.tag == "PlayerShot" || other.gameObject.tag == "Moveables")
        {
            if (PlayerPrefs.GetInt("Difficulty") > 1)
            {
                health -= 1;
     
            }

            else
            {
                health -= 2;
        
            }

            Deatheffect.Play();
            DamageSound.PlayOneShot(DamageSound.clip);
        
            shootRange += 10;
        }

        if (health <= 0)
        {
            Deatheffect.Play();

            DamageSound.PlayOneShot(DamageClips[Random.Range(0,DamageClips.Length)]);
            SpiderMesh.gameObject.GetComponent<Animation>().Play();

            DestroySpider();


        }


    }

    bool Destroying = false;

    void DestroySpider()
    {

 
        if (!Destroying)
        {

            if (this.gameObject.tag == "PurpleSpider")
            {
                Player.GetComponent<Advanced_Combat_Engagement>().Add_Multiplier_Cause_An_Enemy_Destroyed();
                Player.GetComponent<PlayerSounds>().RewardSound.Play();
            }

            if (this.gameObject.tag == "GreenSpider")
            {
                if(Player.GetComponent<Player>().Health < GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MaxHealth)
                    Player.GetComponent<Player>().Health += 1;
                Player.GetComponent<Player>().HealthParticles.Play();
                Player.GetComponent<Player>().HealthBar.GetComponent<Animation>().Play("HealthBarIncrease");
                Player.GetComponent<PlayerSounds>().RewardSound.Play();
            }

            Destroying = true;
            //Random SlowTime
            /*if (Random.Range(0, 2) == 1)
            {
                StartCoroutine(SlowTime());
            }
            */

            //Add Random Force to Push the spider into the air!
            rigid.AddForce(new Vector3(Random.Range(-30, 30), Random.Range(40,100), Random.Range(-30, 30)) * 15);
            rigid.AddTorque(new Vector3(Random.Range(-300, 300), 40, Random.Range(-300, 300)) * 15,ForceMode.Impulse);
            rigid.constraints = RigidbodyConstraints.None;
            rigid.useGravity = true;
            Destroy(this.gameObject, 1f);

           

            //Reward the Player in MONEY
            switch (this.tag)
            {
                

                case "BlueSpider":
                    Player.GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(10));
                 GameObject MANOBJ = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                    MANOBJ.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(10, "+"));
                    break;

                case "YellowSpider":
                    Player.GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(20));
                    GameObject MANOBJ2 = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                    MANOBJ2.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ2.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(20, "+"));
                    break;

                case "RedSpider":
                    Player.GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(30));
                    GameObject MANOBJ3 = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                    MANOBJ3.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ3.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(30, "+"));
                    break;

                case "PurpleSpider":
                    Player.GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(5));
                    GameObject MANOBJ4 = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                    MANOBJ4.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ4.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(5,"+"));
                    break;

                case "GreenSpider":
                    Player.GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(5));
                    GameObject MANOBJ5 = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                    MANOBJ5.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ5.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(5, "+"));
                    break;

                case "WanderingSpider":
                    Player.GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(50));
                    GameObject MANOBJ6 = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                    MANOBJ6.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ6.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(50, "+"));
                    break;
            }


            //Send signal that the player killed an enemy
            Player.GetComponent<Player>().enemydest = true;


            //Spawn Food or other times that the player can sell
            int RandomITEM = Random.Range(0, 4);

            switch (RandomITEM)
            {
                case 0:
                    Instantiate(Food, transform.position, Quaternion.identity);
                break;

                case 1:
                    Instantiate(Tools, transform.position, Quaternion.identity);
                break;
            }

        


        }



    }

    public GameObject Food;
    public GameObject Tools;
   
   /*
    public IEnumerator SlowTime()
    {
        
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = Time.timeScale * .01f;
        
        yield return new WaitForSeconds(0.9f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
     
    }
   */
}


