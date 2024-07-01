using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThiefSpiderScript : MonoBehaviour
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
    public GameObject  WhiteBullet;
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


    //Thief Spider Specific
    public int StolenMoney;
    public GameObject GoldCoinInst;
    public TMP_Text StolenMoneyText;


    GameObject UPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Direction = transform.forward;
        rigid.GetComponent<Rigidbody>();
        Origin = transform.position;

       UPlayer = GameObject.FindGameObjectWithTag("Player");
    }



    public bool AdvancedMovementPlaying = false;
    public IEnumerator AdvancedMovement()
    {
        AdvancedMovementPlaying = true;
        int ranRange = Random.Range(1, 2);
        if (ranRange == 1)
        {


 
            Direction = (UPlayer.transform.position - transform.position).normalized * 4;

          
        }

        else
        {
            Direction = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
        }

        if(rigid.velocity.magnitude > MaxVelocity)
        {
            rigid.velocity = rigid.velocity.normalized * MaxVelocity;
        }

        yield return new WaitForSeconds(5);
        AdvancedMovementPlaying = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

        //Stolen money text
        StolenMoneyText.text = "+" + StolenMoney.ToString();
        StolenMoneyText.transform.LookAt(UPlayer.transform.position);

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
   
        SpiderMesh.transform.LookAt(UPlayer.transform.position);
        PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - UPlayer.transform.position.x, 2) + Mathf.Pow(transform.position.y - UPlayer.transform.position.y, 2) + Mathf.Pow(transform.position.z - UPlayer.transform.position.z, 2));

        reloadTime += 0.1f * Time.deltaTime;
        if (!ReturnDirect)
        {
            if (PlayerDist <= shootRange)
            {
                if (reloadTime >= requiredReload)
                {

                    int YellowSpiderBulletChance = Random.Range(0, 2);

                   


                     if (YellowSpiderBulletChance == 1 && (this.gameObject.tag == "YellowSpider" || this.gameObject.tag == "WanderingSpider" || this.gameObject.tag == "Thief Spider"))
                    {
                        Instantiate(WhiteBullet, SpawnPoint.transform.position, Quaternion.identity);
                        reloadTime = 0;

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
        if (PlayerDist >= 175 && !TutorialSpider)
        {

            Destroy(this.gameObject, 4f);
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

            DamageSound.PlayOneShot(DamageClips[Random.Range(0, DamageClips.Length)]);
           

            DestroySpider();


        }

        //Steal money!

        if(other.gameObject.tag == "Player")
        {

            if (UPlayer.GetComponent<Player>().Money > 0 && UPlayer.GetComponent<Player>().Money < 6)
            {



                StolenMoney++;
                UPlayer.GetComponent<Player>().StartCoroutine(UPlayer.GetComponent<Player>().SubtractMoney(1));


                GameObject MANOBJ = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                MANOBJ.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(1, "-"));
            }

            if (UPlayer.GetComponent<Player>().Money > 5)
            {

                

                StolenMoney+=5;
               UPlayer.GetComponent<Player>().StartCoroutine(UPlayer.GetComponent<Player>().SubtractMoney(5));


                GameObject MANOBJ = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
                MANOBJ.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(5, "-"));
            }
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "LegendaryWeapons")
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


    bool Destroying = false;

    void DestroySpider()
    {


        if (!Destroying)
        {

            if (this.gameObject.tag == "PurpleSpider")
            {
                UPlayer.GetComponent<Advanced_Combat_Engagement>().Add_Multiplier_Cause_An_Enemy_Destroyed();
                UPlayer.GetComponent<PlayerSounds>().RewardSound.Play();
            }

            if (this.gameObject.tag == "GreenSpider")
            {
                if (UPlayer.GetComponent<Player>().Health < UPlayer.GetComponent<Player>().MaxHealth)
                    UPlayer.GetComponent<Player>().Health += 1;
                UPlayer.GetComponent<Player>().HealthParticles.Play();
                UPlayer.GetComponent<Player>().HealthBar.GetComponent<Animation>().Play("HealthBarIncrease");
                UPlayer.GetComponent<PlayerSounds>().RewardSound.Play();
            }

            Destroying = true;
            //Random SlowTime
            /*if (Random.Range(0, 2) == 1)
            {
                StartCoroutine(SlowTime());
            }
            */

            //Add Random Force to Push the spider into the air!
            rigid.AddForce(new Vector3(Random.Range(-30, 30), Random.Range(40, 100), Random.Range(-30, 30)) * 15);
            rigid.AddTorque(new Vector3(Random.Range(-300, 300), 40, Random.Range(-300, 300)) * 15, ForceMode.Impulse);
            rigid.constraints = RigidbodyConstraints.None;
            rigid.useGravity = true;
            Destroy(this.gameObject, 1f);



            //Reward the Player in MONEY
          
            for(int x = 0; x < StolenMoney; x++)
            {
             Instantiate(GoldCoinInst, (transform.position + new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * Random.Range(10, 13) + new Vector3(0,1f,0)), Quaternion.identity);
             
            }


            //Send signal that the player killed an enemy
            UPlayer.GetComponent<Player>().enemydest = true;






        }
    }

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
