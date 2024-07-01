using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Al_AnkabutPlayer : MonoBehaviour
{
  





    public PlayerClass myPlayer = new PlayerClass();

   



    //Physics and movement
    public Rigidbody rgbd;
    public Animator Anim;
   

    public float FixedMoveSpeed;
    public float BaseSpeed = 750;
    public float Movespeed = 750;
    public bool EnteredSpeedStrip = false;
    float SpeedTimer = 3;
    float MaxSpeedStrip = 3;
    public float SprintSpeed = 1000;

    public float Dashtime = 0.1f;


    //Signal that the player is talking to an NPC
    public bool Player_is_Talking = false;

    //Finale Cutscene
    public bool FinaleCutscene = false;

   
    public Vector3 mousePos;
    public Camera Cam;
   
    public SpriteRenderer PlayerSprite;
    private Vector3 MoveDir;

    
    //turn vars for freelook
    Vector2 rotation;
    public float lookspeed = 3;

    //Turn vars when 90 deg set
    // Maximum turn rate in degrees per second.
    public float turningRate = 60f;
    //Stop Player from Turning while turning
    private bool isTurning;
    // Rotation we should blend towards.
    private Quaternion _targetRotation = Quaternion.identity;

    public ParticleSystem Dashtrail;
    
    

    //SpawnPointSystem Variables
    public Transform CurrentSpawnPoint;

    //Start point for instances where a spawnpoint doesn't exist
    Vector3 Startpoint;


  


    public GameObject DiaCanvas;
    public Image ScreenICON;


    public bool TeleportCooldown;

    void Start()
    {

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);



        FixedMoveSpeed = Movespeed;
      



        rgbd.GetComponent<Rigidbody>();

     
        Cam = GameObject.FindGameObjectWithTag("PlayerViewport").GetComponent<Camera>();
       
        Dashtrail.Stop();




 

        //Set Default Camera State
        Cam.enabled = true;
    

        //Load Player Spawn Point
        if (PlayerPrefs.GetFloat("SpawnX") != transform.position.x && SceneManager.GetActiveScene().name == PlayerPrefs.GetString("Scene_Location"))
        {
            if (PlayerPrefs.GetFloat("SpawnX") != 0)
            {

                Vector3 SpawnCoord = new Vector3(PlayerPrefs.GetFloat("SpawnX") + 2, this.gameObject.transform.position.y, PlayerPrefs.GetFloat("SpawnZ") + 2);
                transform.position = SpawnCoord;

            }
        }


       



       

        //Dialogue Canvas and screen icon
        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;

        ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();

        //if there is a default spawn point
        if (GameObject.FindGameObjectWithTag("DefaultSpawnPoint"))
        {
            Startpoint = GameObject.FindGameObjectWithTag("DefaultSpawnPoint").transform.position;
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

        if (FinaleCutscene)
        {
            //Debug.Log("Should Be Here");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
          


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

             

            }

        }

        else
        {

            Dashtime++;

            if (!EnteredSpeedStrip)
                Movespeed = BaseSpeed;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Dashtrail.Stop();
         
        }



        //DashSound
        if (Input.GetKeyDown(KeyCode.LeftShift))
            this.GetComponent<PlayerSounds>().PlayerDamage.Play();

        if (Dashtime <= 0)
            Dashtime = 0;
        else if (Dashtime >= 0.1)
            Dashtime = 0.1f;




        //Keep fixed movement speed if dash isn't pressed
        if (Input.GetKeyUp(KeyCode.LeftShift) && !EnteredSpeedStrip)
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
        if (MoveDir.x < 0)
        {
            PlayerSprite.flipX = true;
        }

        if (MoveDir.x > 0)
        {
            PlayerSprite.flipX = false;
        }





    }





    void FixedUpdate()
    {


        if (!FinaleCutscene)
        {
           

                //PlayerMovement
                float Horizontal = Input.GetAxis("Horizontal");
                float Forward = Input.GetAxis("Vertical");


                MoveDir = new Vector3(Horizontal, 0, Forward);

                rgbd.AddRelativeForce(MoveDir * Movespeed);


                rotation.y += Input.GetAxis("Mouse X");

                //This causes the player to look in one direction at the start



                transform.eulerAngles = new Vector3(0, rotation.y * lookspeed, 0);


               


            }
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
                        
                        SceneManager.LoadScene(other.GetComponent<NextLevelToLoad>().LevelToLoad);
                    }

                }
            }

            else
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
                   
                    SceneManager.LoadScene("ToAl-Ankabut'sDomain");
                }

            }

        }

        



    




    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpawnPoint" || other.gameObject.tag == "Construct" || other.gameObject.tag == "ExitGallery" || other.gameObject.tag == "PrayerRug" || other.gameObject.tag == "ExitTutorialConstruct")
        {
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = false;


        }

    }

    // Call this when you want to turn the object smoothly.
    IEnumerator SetBlendedEulerAngles(Vector3 angles)
    {
        yield return new WaitForSeconds(0.01f);
        isTurning = true;
        _targetRotation = Quaternion.Euler(angles);
        yield return new WaitForSeconds(0.8f);
        isTurning = false;
    }



}
