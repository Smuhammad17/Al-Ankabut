using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class ObjectiveScript : MonoBehaviour
{
    public GameObject ObjCanvas;
    public Text Area_ObjectiveLabel;
    public Text Area_ObjectivesText;

    public bool ActiveObjective;
    public ParticleSystem IncompleteQuests_NPC_Locator_Particles;
    public ParticleSystem ConstructPortal_Locator_Particles;
    public GameObject Construct_Portal;

    //3d waypoint system
    public Image Waypoint;
    public Transform target;
    public Text Target_Distance_Text;


 

    public void Start()
    {
        

        ObjCanvas = GameObject.FindGameObjectWithTag("ObjectiveCanvas").transform.Find("ObjectivesPanel").gameObject;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Viewport = Player.Find("PlayerViewport").GetComponent<Camera>();


        Waypoint = GameObject.FindGameObjectWithTag("ObjectiveCanvas").transform.Find("ObjectiveIcon").GetComponent<Image>();
        Target_Distance_Text = Waypoint.transform.Find("Distance Text").GetComponent<Text>();

    
        Instantiate(ConstructPortal_Locator_Particles, new Vector3(Construct_Portal.transform.position.x,Construct_Portal.transform.position.y + 4,Construct_Portal.transform.position.z), Quaternion.identity);

        if (GameObject.FindGameObjectWithTag("ExitConstruct") || GameObject.FindGameObjectWithTag("ExitTutorialConstruct"))
        {
            if(GameObject.FindGameObjectWithTag("ExitConstruct"))
            Construct_Portal = GameObject.FindGameObjectWithTag("ExitConstruct");

            else
            {
                if (GameObject.FindGameObjectWithTag("ExitTutorialConstruct"))
                {
                    Construct_Portal = GameObject.FindGameObjectWithTag("ExitTutorialConstruct");
                    //Construct_Portal.SetActive(false);
                }
            }
        }

        if (ActiveObjective)
        {
     


         


            ObjCanvas.SetActive(false);



            Area_ObjectiveLabel = ObjCanvas.transform.Find("UIName").GetComponent<Text>();
            Area_ObjectivesText = ObjCanvas.transform.Find("UIDia").GetComponent<Text>();
            Area_ObjectivesText.text = " ";


            InitialListObjectives();


            if (West_Bank_Objectives.Objectives.Length == 0)
            {
                target = Construct_Portal.transform;
            }
        }

      

        else
        {
            if (Construct_Portal)
            {
                target = Construct_Portal.transform;
             
                


            }
            //Find exit construct portal
            else if (Construct_Portal == null && GameObject.FindGameObjectWithTag("ExitTutorialConstruct"))
            {
                Construct_Portal = GameObject.FindGameObjectWithTag("ExitTutorialConstruct");
                target = Construct_Portal.transform;
            }

            else if (Construct_Portal == null && GameObject.FindGameObjectWithTag("ExitConstruct"))
            {
                Construct_Portal = GameObject.FindGameObjectWithTag("ExitConstruct");
                target = Construct_Portal.transform;
            }

            //No Active Objective, and no construct portal disable waypoint marker
            else
            {
                Waypoint = GameObject.FindGameObjectWithTag("ObjectiveCanvas").transform.Find("ObjectiveIcon").GetComponent<Image>();
                Waypoint.enabled = false;
                Target_Distance_Text = Waypoint.transform.Find("Distance Text").GetComponent<Text>();
                Target_Distance_Text.enabled = false;
            }


            if (Area_ObjectivesText)
                Area_ObjectivesText.text = " ";

            if (ActiveObjective)
                Area_ObjectiveLabel.text = West_Bank_Objectives.AreaLabel;
        }
  

        for(int x = 0; x < West_Bank_Objectives.Objectives.Length;x++)
        {
            Instantiate(IncompleteQuests_NPC_Locator_Particles, new Vector3(West_Bank_Objectives.Objectives[x].ConnectedNPC.transform.position.x, West_Bank_Objectives.Objectives[x].ConnectedNPC.transform.position.y + 4, West_Bank_Objectives.Objectives[x].ConnectedNPC.transform.position.z), Quaternion.identity);
        }

 
        
    }



    public bool singleAreaResAlert = false;
    public ObjectiveClass West_Bank_Objectives;
    bool toTalkAgain = false;
    
    bool PopupAnim = false;

    public Transform Player;
    public Camera Viewport;

    public void Update()
    {
        if (Player.GetComponent<Player>().Player_is_Talking)
        {
            Waypoint.enabled = false;
            Target_Distance_Text.enabled = false;
        }

        else
        {
            if (!Waypoint.enabled)
            {
                Waypoint.enabled = true;
                Target_Distance_Text.enabled = true;
            }
        }


        //Enable and Disable Canvas
        if (Input.GetKey(KeyCode.Space) && ActiveObjective && !GameObject.FindGameObjectWithTag("MathChallenge").GetComponent<MathChallenge>().GameActive)
        {
            ObjCanvas.SetActive(true);
            if (!PopupAnim)
            {
                ObjCanvas.GetComponent<Animation>().Play();
                PopupAnim = true;
            }
        }

        else if (ObjCanvas.activeSelf)
        {
            ObjCanvas.SetActive(false);
            if (Input.GetKey(KeyCode.Space))
            PopupAnim = false;
        }



        if (ActiveObjective )
        {

            //CheckFORCompletion();

            //Find closest objective 

           

            if (!West_Bank_Objectives.AllObjectivesComplete_Global)
            {
                findClosest();
            }

            else
            {
                target = Construct_Portal.gameObject.transform;
            }

            SetWaypoint();
        }
    
        if(target != null )
        {
            SetWaypoint();

        }


    }

    float minX;
    float maxX;

    float minY; 
    float maxY;

    Vector2 WaypointPos;

    void SetWaypoint()
    {
        //3d waypoint system
         minX = Waypoint.GetPixelAdjustedRect().width;
         maxX = Screen.width - minX;

         minY = Waypoint.GetPixelAdjustedRect().height;
         maxY = Screen.width - minY;



        WaypointPos = Viewport.WorldToScreenPoint(target.position);
        WaypointPos.x = Mathf.Clamp(WaypointPos.x, minX, maxX);
        //WaypointPos.y = Mathf.Clamp(WaypointPos.y, minY, maxY);

        Waypoint.transform.position = WaypointPos;

        if (Vector3.Dot((target.position - Viewport.transform.position), Viewport.transform.forward) < 0)
        {
            Waypoint.enabled = false;
            Target_Distance_Text.enabled = false;
        }

        else
        {
            if (!Player.GetComponent<Player>().Player_is_Talking)
            {
                Waypoint.enabled = true;
                Target_Distance_Text.enabled = true;
            }
        }


        //Waypoint Meter distance to target
        
        Target_Distance_Text.text = Mathf.RoundToInt(Vector3.Distance(target.position, Viewport.transform.position)).ToString();
    }


    float nearestDist;

    void findClosest()
    {
         nearestDist = float.MaxValue;

        for(int x = 0; x < West_Bank_Objectives.Objectives.Length; x++)
        {

            if (Vector3.Distance(West_Bank_Objectives.Objectives[x].ConnectedNPC.gameObject.transform.position, Player.transform.position) < nearestDist)
            {

                nearestDist = Vector3.Distance(West_Bank_Objectives.Objectives[x].ConnectedNPC.gameObject.transform.position, Player.transform.position);
                target = West_Bank_Objectives.Objectives[x].ConnectedNPC.gameObject.transform;
            }

           

        }


    }



    void InitialListObjectives()
    {
        //Clear the board if there's stuff on it
        if (Area_ObjectivesText)
            Area_ObjectivesText.text = " ";

        singleAreaResAlert = false;

        Area_ObjectiveLabel.text = West_Bank_Objectives.AreaLabel;
        for (int x = 0; x < West_Bank_Objectives.Objectives.Length; x++)
        {

            Area_ObjectivesText.text += West_Bank_Objectives.Objectives[x].ObjLabel;
            Area_ObjectivesText.text += West_Bank_Objectives.CheckComplete(West_Bank_Objectives.Objectives[x]);
            Area_ObjectivesText.text += "\n \n";
        }
    }

    string NewObjLabel;

    void CheckFORCompletion()
    {
        Area_ObjectivesText.text = " ";
        NewObjLabel = "";

        for (int x = 0; x < West_Bank_Objectives.Objectives.Length; x++)
        {
            West_Bank_Objectives.CheckComplete(West_Bank_Objectives.Objectives[x]);

      
            NewObjLabel += West_Bank_Objectives.Objectives[x].ObjLabel + "\n \n";
           
            
        }


        Area_ObjectivesText.text =  NewObjLabel;



    }

  




 
}