using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyScript : MonoBehaviour
{

    //Temporary Enemy Contract Test
    

    public Enemy EC = new Enemy();

   public Material Mat1;
   public Material Mat2;

    public float maxPlayerDistToDisplayValue;

    public bool InPlay = true;

    private Contract.Difficulty GameDifficulty;

    public AudioSource SoundPlayer;
    public AudioClip[] AudioClips;

    void Start()
    {

        GameDifficulty = GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractScript>().Contract.difficultysetting;
        //Select a Level for the enemy depending on game difficulty
        if (GameDifficulty == Contract.Difficulty.Easy)
        {
            EC.level = Random.Range(1, 3);
        }

        if (GameDifficulty == Contract.Difficulty.Medium)
        {
            EC.level = Random.Range(1, 6);
        }

        if (GameDifficulty == Contract.Difficulty.Hard)
        {
            EC.level = Random.Range(1, 10);
        }

        if (GameDifficulty == Contract.Difficulty.Extreme)
        {
            EC.level = Random.Range(1, 12);
        }

        

    }


    void Update()
    {

        EC.EnemyValueText.text = EC.EnemyValue.ToString();

        //Rotate text towards player so it's readable
        GameObject PlayerCam = GameObject.FindGameObjectWithTag("PlayerViewport");
        this.gameObject.transform.LookAt(PlayerCam.transform);





        float PlayerDist; 
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerDist = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2) + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));

        //Reveal the number of Rebound spheres when close to them
        if(PlayerDist < maxPlayerDistToDisplayValue)
        {
            EC.EnemyValueText.GetComponent<MeshRenderer>().enabled = true;
            EC.EnemyLevelText.GetComponent<MeshRenderer>().enabled = true;
        }

        else
        {
            EC.EnemyLevelText.GetComponent<MeshRenderer>().enabled = false;
        }

        //Enemy Running away
        if (EC.EnemyHit)
        {     
            //The rebound script takes control here to move object away from player and make it run
            this.gameObject.GetComponent<MeshRenderer>().material = Mat2;

        
       
        }

        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = Mat1;
        }



    }



    void OnCollisionEnter(Collision other)
    {


       
        
            if (other.gameObject.tag == "PlayerShot" && !EC.EnemyHit || other.gameObject.tag == "Player" && !EC.EnemyHit)
            {
                //Player hits object for the first time, Object begins to run away!
                if (EC.EnemyValue == GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractScript>().Contract.Results)
                {
                    EC.EnemyHit = true;
                    //Run away Statement
                    this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    EC.EnemyLevelText.text = EC.EnemyRunningStatements[Random.Range(0, EC.EnemyRunningStatements.Length)];
                //Play Correct Answer Sound
                SoundPlayer.PlayOneShot(AudioClips[0]);

            }
            }






            //If Enemy is hit by Player's Instantiated Obj or the player after running away
            else if (other.gameObject.tag == "PlayerShot" && EC.EnemyHit || other.gameObject.tag == "Player" && EC.EnemyHit)
            {
                //If the enemy is the correct answer or if player hits object with same value as answer
                if (EC.EnemyValue == GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractScript>().Contract.Results)
                {
                //Play Correct Answer Sound
                SoundPlayer.PlayOneShot(AudioClips[0]);



                ////Give Player XP here

                //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().myPlayer.Experience += GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractScript>().Contract.ExpPayout;
                //    Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().myPlayer.Experience);
              
                ////Save Player XP
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SavePlayerExp(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().myPlayer.Experience);





                    //Generate a new contract
                    //Possibly turn this into an Coroutine to give the player time to re adjust and for any animations to play
                    GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractScript>().HaveContract = false;

                    GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractScript>().ContractsToDo -= 1;

                    StartCoroutine(Cooldown());
                }
                //wrong question
                else
                {
                   // Debug.Log("Wrong answer sorry");

                //Play wrong answer sound
                SoundPlayer.PlayOneShot(AudioClips[1]);
                }
            }


        


    }

    IEnumerator Cooldown()
    {
        InPlay = false;
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        InPlay = true;
        EC.EnemyHit = false;
    }
}
