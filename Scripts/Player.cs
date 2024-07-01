using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    //select wether player is in hub or game state;
    public enum GameStates { Hub, Game };
    public GameStates CurrentState;

    //Game gamestate levels
    public Text RequiredLevelToCompletion;



    public PlayerClass myPlayer = new PlayerClass();
   
    //Player Health
    public int Health;
    public int MaxHealth = 10;
    public GameObject HealthBar;
    public Sprite[] HealthBarSprites;

    //display Player level and Name
    public GameObject Hcanv;
    public GameObject HcanvTopLeftCorner;
    public Slider LevelBar; 


    //Physics and movement
    public Rigidbody rgbd;
    public Animator Anim;
    float FiringAnimCooldown = 0;

    public float FixedMoveSpeed;
    public float BaseSpeed = 750;
    public float Movespeed = 750;
    public bool EnteredSpeedStrip = false;
    float SpeedTimer = 3;
    float MaxSpeedStrip = 3;
    public float SprintSpeed = 1000;
 
    public float Dashtime = 0.1f;




    //Combat
    public enum FireType { AtomBlast, TripleFire, SwordOfTheSpirit, SabreoftheSureTruth}
    public FireType PlayerFireType;
    public ParticleSystem UpgradeEffects;
    public ParticleSystem HealthParticles;
    public ParticleSystem SpeedLines;

    //Signal that the player is talking to an NPC
    public bool Player_is_Talking = false;

    //Finale Cutscene
    public bool FinaleCutscene = false;

    //Money and Treasure
    public int Money;
    public GameObject MoneyHud;
    public Text MoneyText;

    public void Upgrade()
    {
        StartCoroutine(UpgradeCoroutine(PlayerFireType));
    }

    IEnumerator UpgradeCoroutine(FireType CurrentType)
    {


        PlayerFireType = FireType.TripleFire;
        UpgradeEffects.Play();
        yield return new WaitForSeconds(10);
        UpgradeEffects.Stop();
        PlayerFireType = CurrentType;
    }

    public Vector3 mousePos;
    public Camera Cam;
    public Camera FPSCam;
    public SpriteRenderer PlayerSprite; 
    private Vector3 MoveDir;

    public bool HandsFull;
    public bool CanPickup;
    public Transform PickupObj;


    //turn vars for freelook
    Vector3 rotation;
    public float lookspeed = 3;


    public ParticleSystem Dashtrail;
    //Death Variables
  

    public GameObject DeathCanvas;
    bool isdead = false;


    //Find Contract Manager
    GameObject CM;

    //hub world enemy killed signal notification
    public bool enemydest = false;

    //SpawnPointSystem Variables
    public Transform CurrentSpawnPoint;

    //Start point for instances where a spawnpoint doesn't exist
    Vector3 Startpoint;


    int GateToAlAnk = 0;


    //Pages Of The Holy Book
   public int POTHBCount;
    
    Text POTHBDesc;
    Text POTHBTag;


    public GameObject DiaCanvas;
    public Image ScreenICON;

    public bool LgndMusicPlaying;
    public bool TeleportCooldown;

    void Start()
    {
        

        PlayerPrefs.SetString("Saved_CurrentLevel", SceneManager.GetActiveScene().name);

     

        FixedMoveSpeed = Movespeed;
        if(GameObject.FindGameObjectWithTag("ContractManager"))
        CM = GameObject.FindGameObjectWithTag("ContractManager");
        if(GameObject.FindGameObjectWithTag("LevelCanvas"))
        Hcanv = GameObject.FindGameObjectWithTag("LevelCanvas");
        HcanvTopLeftCorner = Hcanv.transform.Find("LevelCanvasBorder").gameObject;


     

        ////Set Level on Start
        //Text UILevelName = Hcanv.transform.Find("PersistentLevelName").GetComponent<Text>();
        //UILevelName.text = myPlayer.LevelName;

        rgbd.GetComponent<Rigidbody>();
 
        Health = 10;
        
        //This has to use Find because they both have the same tag and I'm too scared to change it now... Using Findbytag produces a race condition!! :)
        Cam = GameObject.Find("PlayerViewport").GetComponent<Camera>();
        FPSCam = GameObject.Find("FPSCam").GetComponent<Camera>();
        Dashtrail.Stop();

        CanPickup = false;

    
        if (CM != null)
        {
            CurrentState = GameStates.Game;
        }

        else
        {
            CurrentState = GameStates.Hub;
        }

        //only if in Game state
        if (CurrentState == GameStates.Game)
        {
            RequiredLevelToCompletion = GameObject.FindGameObjectWithTag("ContractCanvas").transform.Find("ScorePanel").transform.Find("ScoreText").GetComponent<Text>();
        }

        //Set Default Camera State
        Cam.enabled = true;
        FPSCam.enabled = false;

        //Load Player Spawn Point
        if (PlayerPrefs.GetFloat("SpawnX") != transform.position.x && SceneManager.GetActiveScene().name == PlayerPrefs.GetString("Scene_Location"))
        {
            if (PlayerPrefs.GetFloat("SpawnX") != 0)
            {

                Vector3 SpawnCoord = new Vector3(PlayerPrefs.GetFloat("SpawnX") + 2, this.gameObject.transform.position.y, PlayerPrefs.GetFloat("SpawnZ") + 2);
                transform.position = SpawnCoord;

                //Load players money
                if (PlayerPrefs.HasKey("SavedMoney"))
                  Money = PlayerPrefs.GetInt("SavedMoney");

            }
        }


        //Money and Treasure

        MoneyHud = Hcanv.transform.Find("Money and Legendary Weapons Hud").gameObject;
        MoneyText = MoneyHud.transform.Find("Money Count").GetComponent<Text>();
        if (!PlayerPrefs.HasKey("SavedMoney"))
            Money = 0;





        
       //Death canvas

        DeathCanvas = GameObject.FindGameObjectWithTag("DeathCanvas");
        DeathCanvas.GetComponent<Canvas>().enabled = false;

    //Dialogue Canvas and screen icon
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;

        ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();

        //if there is a default spawn point
        if (GameObject.FindGameObjectWithTag("DefaultSpawnPoint")){
            Startpoint = GameObject.FindGameObjectWithTag("DefaultSpawnPoint").transform.position;
        }


        //Pages of the Holy Book

        POTHBTag = HcanvTopLeftCorner.transform.Find("POTB XP").GetComponent<Text>();
        POTHBDesc = HcanvTopLeftCorner.transform.Find("POTB Tag").GetComponent<Text>();

        POTHBCount = 0;

        if (GameObject.FindGameObjectsWithTag("POTB").Length != 0)
        {
          
            POTHBDesc.enabled = true;
            POTHBTag.enabled = true;

            SetPOTHB(POTHBCount);
           

        }

        else
        {
            POTHBDesc.enabled = false;
            POTHBTag.enabled = false;
        }

    }


    void Update()
    {

       
        PlayerSprite.enabled = !FinaleCutscene;

        if (!FinaleCutscene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
          

            //Hcanv.GetComponent<Canvas>().enabled = true;
        }

        if(FinaleCutscene)
        {
            //Debug.Log("Should Be Here");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Hcanv.GetComponent<Canvas>().enabled = false;
           
         
        }

        /*
        if (Input.GetKey("b"))
        {
            PlayerPrefs.DeleteAll();
        }
       */

        //Dodge and Dash
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (!EnteredSpeedStrip)
            {

                Dashtime -= 1 * Time.deltaTime;

                if (Dashtime >= 0.06f)
                {
                    Movespeed = SprintSpeed * 3;

                    if (!Dashtrail.isPlaying)
                        Dashtrail.Play();
                }



                else if (Dashtime <= 0)
                {
                    Movespeed = SprintSpeed;
                    Dashtrail.Stop();

                }

                //UI Sprint Effect
                if (!SpeedLines.isPlaying)
                    SpeedLines.Play();

            }

        }

        else
        {

            Dashtime++;

            if (!EnteredSpeedStrip)
                Movespeed = BaseSpeed;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            Dashtrail.Stop();
            SpeedLines.Stop();
        }

      

        //DashSound
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            this.GetComponent<PlayerSounds>().PlayerDamage.Play();

        if (Dashtime <= 0)
            Dashtime = 0;
        else if (Dashtime >= 0.1)
            Dashtime = 0.1f;




        //Keep fixed movement speed if dash isn't pressed
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) && !EnteredSpeedStrip)
        {
            if (Movespeed > FixedMoveSpeed)
            {
                Movespeed -= 10 * Time.deltaTime;
            }

            else if (Movespeed > FixedMoveSpeed + 30)
            {
                Movespeed -= 30 * Time.deltaTime;
            }

            else if (Movespeed < FixedMoveSpeed)
            {
                Movespeed += 10 * Time.deltaTime;
            }

            else if (Movespeed < FixedMoveSpeed + 30)
            {
                Movespeed += 30 * Time.deltaTime;
            }

        }

        //Player Animation
        if (MoveDir.x != 0 || MoveDir.z != 0)
        {
            Anim.SetBool("IsWalking", true);
            
          
        }

        else { Anim.SetBool("IsWalking", false); }

        //Flip Sprite
        if(MoveDir.x  < 0)
        {
          PlayerSprite.flipX = true;
        }

        if (MoveDir.x > 0)
        {
           PlayerSprite.flipX = false;
        }

   
  

        //So Script won't call for these items in the hub area
        if (CurrentState == GameStates.Game)
        {
            //No Location text if not in hub area
            //GameObject.FindGameObjectWithTag("LT").GetComponent<Text>().text = " ";
            RequiredLevelToCompletion.text = "Contracts Remaining: " + (CM.GetComponent<ContractScript>().ContractsToDo.ToString());

            if (CM.GetComponent<ContractScript>().ContractsToDo == 0)
            {
                RequiredLevelToCompletion.text = "Contracts Complete! Find The Exit";
            }
        }

        //Only do in Hub state
        else if (CurrentState == GameStates.Hub)
        {



        }

       
     
     

        

        //Health Bar
        HealthBar = HcanvTopLeftCorner.transform.Find("HealthBar").gameObject;

       

        //Set Health Bar sprites
        if (Health > 0)
        {
            HealthBar.GetComponent<Image>().sprite = HealthBarSprites[Health - 1];
   
        }

        if (Health < 1 && !isdead)
        {
            isdead = true;
            StartCoroutine(Die());
            
        }

     
        

        //if player has killed an enemy
        if (enemydest)
        {
            
            

            //Begin Multiplier
            this.GetComponent<Advanced_Combat_Engagement>().Add_Multiplier_Cause_An_Enemy_Destroyed();
            
            enemydest = false;
        }


        //FPS Cam functionality
        
        if (Input.GetKey(KeyCode.Space) && !FinaleCutscene && !GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive && !Player_is_Talking)
        {
            Cam.enabled = false;
            FPSCam.enabled = true;
            if (GameObject.FindGameObjectWithTag("KT"))
            GameObject.FindGameObjectWithTag("KT").SetActive(false);
        }

        else if (!FinaleCutscene && Input.GetKeyUp(KeyCode.Space))
        {
            Cam.enabled = true;
            FPSCam.enabled = false;
        }
        

        //Drop Object
        if (Input.GetMouseButtonUp(0) && HandsFull && !FinaleCutscene)
        {

            HandsFull = false;
            if (PickupObj)
            {
                PickupObj.GetComponent<Rigidbody>().useGravity = true;
                PickupObj.parent = null;
                HandsFull = false;
                PickupObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            
        }


        //SET MONEY AND TREASURE
        MoneyText.text = Money.ToString();



        

       
    }






    void FixedUpdate()
    {
       

        if (!FinaleCutscene)
        {
            if (!isdead)
            {

                //PlayerMovement
                float Horizontal = Input.GetAxis("Horizontal");
                float Forward = Input.GetAxis("Vertical");
                

                MoveDir = new Vector3(Horizontal, 0, Forward);
              
                rgbd.AddRelativeForce(MoveDir * Movespeed);



                rotation.y = Input.GetAxis("Mouse X");

                //This causes the player to look in one direction at the start
                
                transform.eulerAngles = transform.rotation.eulerAngles +  new Vector3(0, rotation.y * lookspeed, 0);
              


                //Pickup object script

                if (HandsFull && PickupObj != null)
                {
                    PickupObj.transform.position = myPlayer.instSpawnPoint.transform.position;

                }


            }
        }
    }

 
    


    public void OnTriggerEnter(Collider other)
    {
      
        //Speed Strip Response
        if (other.gameObject.tag == "SpeedStrip" && !EnteredSpeedStrip)
        {
           
            EnteredSpeedStrip = true;
           // Debug.Log("Begin Speed Strip");
            Movespeed = Movespeed * 3f;

            StartCoroutine(ResetEnteredSpeedStrip());
        }

    

    }

    public void SetPOTHB(int count)
    {
        POTHBCount = count;
        //Update Pages of the Holy Book.
        POTHBTag.text = POTHBCount + "/" + GameObject.FindGameObjectsWithTag("POTB").Length;
    }

    IEnumerator ResetEnteredSpeedStrip()
    {
        UpgradeEffects.Play();
        yield return new WaitForSeconds(4);
        UpgradeEffects.Stop();
        EnteredSpeedStrip = false;
       // Debug.Log("Ready Speed Strip");
    }

    public bool canBeHit = true;
    IEnumerator DamageCooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(1.5f);

        //Damage Animatino
        Anim.SetBool("Taking_Damage", false);

        canBeHit = true;
    }

    void OnCollisionEnter(Collision other)
    {
        

        if (other.gameObject.tag == "NonRectEnemy" || other.gameObject.tag == "RedBullet" || other.gameObject.tag == "YellowBullet")
        {
            if (!this.GetComponent<FaithFieldCollision>().FaithFieldActive)
            {
                //toss player if they touch an enemy
                rgbd.AddForce(other.GetContact(0).normal * myPlayer.ReboundForce / 2, ForceMode.Impulse);
                //rgbd.AddForce(new Vector3(0, 1, 0),ForceMode.Impulse);

              //  Debug.Log("Hit Player: " + Health);

                if (canBeHit)
                {
                    Health -= 1;

                    //Damage Animation
                    Anim.SetBool("Taking_Damage", true);


                    StartCoroutine(DamageCooldown());
                }
                HealthBar.GetComponent<Animation>().Play();
            }
            else if (other.gameObject.GetComponent<Rigidbody>())
            {
                //toss enemy if they touch a player with forcefields
                other.gameObject.GetComponent<Rigidbody>().AddForce(other.GetContact(0).normal * myPlayer.ReboundForce / 2, ForceMode.Impulse);
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (!this.GetComponent<FaithFieldCollision>().FaithFieldActive)
            {
                rgbd.AddForce(other.GetContact(0).normal * myPlayer.ReboundForce / 2, ForceMode.Impulse);
                //rgbd.AddForce(new Vector3(0,1, 0), ForceMode.Impulse);
                if (other.gameObject.GetComponent<EnemyScript>().EC.EnemyValue != CM.GetComponent<ContractScript>().Contract.Results && other.gameObject.GetComponent<EnemyScript>().InPlay)
                {
                    if (canBeHit)
                    {
                        Health -= 1;

                        //Damage Animation
                        Anim.SetBool("Taking_Damage", true);


                        StartCoroutine(DamageCooldown());
                    }

                    HealthBar.GetComponent<Animation>().Play();
                }

            }

            else
            {
                //toss enemy if they touch a player with forcefields
                other.gameObject.GetComponent<Rigidbody>().AddForce(other.GetContact(0).normal * myPlayer.ReboundForce / 2, ForceMode.Impulse);
            }
        }

        ////Player takes damage from touching spiders
        //if (other.gameObject.tag == "GreenSpider" || other.gameObject.tag == "BlueSpider" || other.gameObject.tag == "YellowSpider" || other.gameObject.tag == "RedSpider" || other.gameObject.tag == "PurpleSpider" || other.gameObject.tag == "WanderingSpider" || other.gameObject.tag == "Thief Spider")
        //{
        //    if (!this.GetComponent<FaithFieldCollision>().FaithFieldActive)
        //    {
        //        rgbd.AddForce(other.GetContact(0).normal * myPlayer.ReboundForce / 2, ForceMode.Impulse);
        //        //rgbd.AddForce(new Vector3(0,1, 0), ForceMode.Impulse);
                
        //        if (canBeHit)
        //        {
        //            Health -= 1;

        //            //Damage Animation
        //            Anim.SetBool("Taking_Damage", true);



        //            StartCoroutine(DamageCooldown());
        //        }

        //        HealthBar.GetComponent<Animation>().Play();


        //    }

        //    else
        //    {
        //        //toss enemy if they touch a player with forcefields
        //        other.gameObject.GetComponent<Rigidbody>().AddForce(other.GetContact(0).normal * myPlayer.ReboundForce / 2, ForceMode.Impulse);
        //    }

        //}



    }


    public void OnTriggerStay(Collider other)
    {
        //if Enter a Construct Trigger
        if (other.gameObject.tag == "Construct")
        {
            MinigamePortalScript Construct = other.GetComponent<MinigamePortalScript>();

            Construct.PressENotif.SetActive(true);
            Construct.PressENotif.GetComponent<TextMesh>().text = "Press E to Enter the Construct";

            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");

            GameObject KhadimsThoughts;
            KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;
            KhadimsThoughts.SetActive(false);


           
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;

            BackPanel.transform.Find("Dialogue Count").GetComponent<Text>().enabled = false;

            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = Construct.ConstructName;
            NPCText.text = Construct.ConstructDescription;
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;

            if (Input.GetKey("e"))
            {
               
                    if (GameObject.FindGameObjectWithTag("Loading"))
                    {
                       GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene(Construct.LevelToLink);
                    }
                    else
                    {
                       // Debug.Log("No Loading Assets Found, reverting to simple load");
                        SceneManager.LoadScene(Construct.LevelToLink);
                    }
                   
               

               
            }
        }
        
        if (other.gameObject.tag == "ExitConstruct" || other.gameObject.tag == "ExitGallery")
        {

            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            GameObject KhadimsThoughts;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");

            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;

            KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;
            KhadimsThoughts.SetActive(false);



            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "<color=green>Level Complete!</color>";
            NPCText.text = "Press E to leave the area";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;

            if (Input.GetKey("e"))
            {
                if (GameObject.FindGameObjectWithTag("Loading"))
                {
                  GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene(other.GetComponent<NextLevelToLoad>().LevelToLoad);
                  
                }
                else
                {
                  //  Debug.Log("No Loading Assets Found, reverting to simple load");
                    SceneManager.LoadScene(other.GetComponent<NextLevelToLoad>().LevelToLoad);
                }
                
            }

        }


        if (other.gameObject.tag == "ExitTutorialConstruct")
        {
            if (SceneManager.GetActiveScene().name != "Tutorial")
                
            {
                GameObject DiaCanvas;
                GameObject BackPanel;
                Text NPCText;
                Text NPCNameBox;

                DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
                BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
                NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
                NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
                NPCNameBox.text = "<color=green>Level Complete!</color>";
                NPCText.text = "Press E to leave the area";
                GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

                ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
                ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;

                if (Input.GetKey("e"))
                {
                    if (GameObject.FindGameObjectWithTag("Loading"))
                    {
                        GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene(other.GetComponent<NextLevelToLoad>().LevelToLoad);

                    }
                    else
                    {
                       // Debug.Log("No Loading Assets Found, reverting to simple load");
                        SceneManager.LoadScene(other.GetComponent<NextLevelToLoad>().LevelToLoad);
                    }

                }
            }

            else {


                GameObject DiaCanvas;
                GameObject BackPanel;
                Text NPCText;
                Text NPCNameBox;

                DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
                BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
                NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
                NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
                NPCNameBox.text = "<color=green>Level Complete!</color>";
                NPCText.text = "Press E to Enter the West Bank";
                GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;



                if (Input.GetKey("e"))
                {
                    if (GameObject.FindGameObjectWithTag("Loading"))
                    {
                        GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("TheWestBank");

                    }
                    else
                    {
                      //  Debug.Log("No Loading Assets Found, reverting to simple load");
                        SceneManager.LoadScene("TheWestBank");
                    }

                }

            }
        }


            if (other.gameObject.tag == "MasterGrip")
        {

            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "<color=yellow>Collect the Master Grip!</color>";
            NPCText.text = "Press E to return to the Cities.";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;

            if (Input.GetKey("e"))
            {
              

                if (GameObject.FindGameObjectWithTag("Loading"))
                {
                    
                    GameObject.FindGameObjectWithTag("Loading").GetComponent<LevelManager>().LoadScene("ToAl-Ankabut'sDomain");

                }
                else
                {
                  //  Debug.Log("No Loading Assets Found, reverting to simple load");
                    SceneManager.LoadScene("ToAl-Ankabut'sDomain");
                }

            }

        }

        //if enter a key zone allow user to pick up key
        if (other.gameObject.tag == "MoveableKey" || other.gameObject.tag == "Pickup" || other.gameObject.tag == "CalculatorPin" || other.gameObject.tag == "Food" || other.gameObject.tag == "Tool")
        {
            CanPickup = true;
            

            if (Input.GetMouseButton(0) && !HandsFull && CanPickup)
                
            {
                HandsFull = true;
                PickupObj = other.gameObject.transform;
                PickupObj.GetComponent<Rigidbody>().useGravity = false; 
            }

            

        

        }

        //Prayer Rugs
        if(other.gameObject.tag == "PrayerRug")
        {
            bool canpray = true;

            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            ScreenICON.sprite = ScreenICON.GetComponent<DefaultICON>().Default_ICON;


            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;

            BackPanel.transform.Find("Dialogue Count").GetComponent<Text>().enabled = false;

            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Prayer Rug";
            NPCText.text = "Press E to Heal";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            if (Input.GetKey("e"))
            {
                if (canpray)
                {
                    canpray = false;
                    if (Health < MaxHealth)
                        Health += 1;
                    HealthParticles.Play();
                    HealthBar.GetComponent<Animation>().Play("HealthBarIncrease");
                    if (!this.GetComponent<PlayerSounds>().RewardSound.isPlaying)
                        this.GetComponent<PlayerSounds>().RewardSound.Play();
                }

            }

            if (Input.GetKeyUp("e"))
            {
                canpray = true;
            }

        }


       
    
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpawnPoint" || other.gameObject.tag == "Construct" || other.gameObject.tag == "ExitGallery" || other.gameObject.tag == "PrayerRug" || other.gameObject.tag == "ExitTutorialConstruct")
        {
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;

        
        }

        if(other.gameObject.tag == "MoveableKey" || other.gameObject.tag == "Pickup")
        {
            CanPickup = false;
           
        }



    }

    //// Call this when you want to turn the object smoothly.
    //IEnumerator SetBlendedEulerAngles(Vector3 angles)
    //{
    //    yield return new WaitForSeconds(0.01f);
    //    isTurning = true;
    //    _targetRotation = Quaternion.Euler(angles);
    //    yield return new WaitForSeconds(0.8f);
    //    isTurning = false;
    //}








    IEnumerator Die()
    {
 

       DeathCanvas.GetComponent<Canvas>().enabled = true;

        GameObject MoneyLossText = DeathCanvas.transform.Find("MoneyLossText").gameObject;
        MoneyLossText.GetComponent<Text>().enabled = true;

        if (Money < 100)
        {
            Money -= Mathf.RoundToInt(Money / 2);
            MoneyLossText.GetComponent<Text>().text = "Money Lost: " + Mathf.RoundToInt(Money / 2).ToString();
        }

        else
        {
            Money -= 50;
            MoneyLossText.GetComponent<Text>().text = "Money Lost: " + 50;
        }

     
       




        DeathCanvas.GetComponent<Animation>().Play();

        /*
        GameObject DiaCanvas;
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        DiaCanvas.GetComponent<Canvas>().enabled = false;
        */

       

        GameObject LevCanvas;
        LevCanvas = GameObject.FindGameObjectWithTag("LevelCanvas");
        LevCanvas.GetComponent<Canvas>().enabled = false;

        yield return new WaitForSeconds(6);
       
            Vector3 SpawnCoord = new Vector3(PlayerPrefs.GetFloat("SpawnX") + 2, this.gameObject.transform.position.y, PlayerPrefs.GetFloat("SpawnZ") + 2);
        //If player has a spawn point
        if (SpawnCoord != null && SceneManager.GetSceneByName("HubWorld").isLoaded || SceneManager.GetSceneByName("TheWestBank").isLoaded || SceneManager.GetSceneByName("Tutorial").isLoaded && SceneManager.GetActiveScene().name == PlayerPrefs.GetString("Scene_Location"))
        {
            transform.position = SpawnCoord;
        }
        
        else
        {
            transform.position = Startpoint;
        }

            DeathCanvas.GetComponent<Canvas>().enabled = false;
        MoneyLossText.GetComponent<Text>().enabled = false;
        LevCanvas.GetComponent<Canvas>().enabled = true;
          //  DiaCanvas.GetComponent<Canvas>().enabled = true;
            isdead = false;

            Health = MaxHealth;
        
    }


    // REWARD and TREASURE ----------------------------------------------------------------------------------------------------------------------------

    public IEnumerator AddMoney(int amount)
    {
        
        while(amount > 0)
        {
            Money++;
            MoneyText.GetComponent<Animation>().Play("MoneyAdd");
            amount--;
            yield return null;
        }
     
    }

    public IEnumerator SubtractMoney(int amount)
    {
        while(amount > 0)
        {
            Money--;
            MoneyText.GetComponent<Animation>().Play("MoneySub");
            amount--;
            yield return null;
        }
    }





}
