using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractScript : MonoBehaviour
{


    public Contract Contract = new Contract();
    public bool HaveContract = false;
    public bool CompleteLevel = false;
    public Text ContractReadoutFV;
    public Text ContractReadoutSV;
    public Text ContractReadoutSign;

  
    public int ContractsToDo;

    public GameObject[] Enemies;

    public GameObject ExitConstruct;

    public bool ClearGate;
    public string PlayerPrefName_ToClearGate;


    void Start()
    {
        CompleteLevel = false;
        
        //Debug.Log("Player would have to do " + ContractsToDo + " Contracts");
        if (GameObject.FindGameObjectWithTag("ExitConstruct"))
        {
            ExitConstruct = GameObject.FindGameObjectWithTag("ExitConstruct");
            //Debug.Log("Found Construct Exit Disabling");
            ExitConstruct.SetActive(false);
        }
        else if (GameObject.FindGameObjectWithTag("MasterGrip"))
        {
            ExitConstruct = GameObject.FindGameObjectWithTag("MasterGrip");
           
            ExitConstruct.SetActive(false);
        }

        else if (GameObject.FindGameObjectWithTag("ExitTutorialConstruct"))
        {
            ExitConstruct = GameObject.FindGameObjectWithTag("ExitTutorialConstruct");
            
            ExitConstruct.SetActive(false);
        }

        if (!PlayerPrefs.HasKey(PlayerPrefName_ToClearGate))
        {
            PlayerPrefs.SetInt(PlayerPrefName_ToClearGate, 0);
        }
    }



    // Update is called once per frame
    void Update()
    {




        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Select Arithmetic Operator and print Readout Text
        switch (Contract.ArithmeticOp)
        {
            case Contract.Arithmetic.Add:
                ContractReadoutFV.text = Contract.FV.ToString();
                ContractReadoutSV.text = Contract.SV.ToString();
                ContractReadoutSign.text = "+";
                break;

            case Contract.Arithmetic.Subtract:
                ContractReadoutFV.text = Contract.FV.ToString();
                ContractReadoutSV.text = Contract.SV.ToString();
                ContractReadoutSign.text = "-";
                break;

            case Contract.Arithmetic.Multiply:
                ContractReadoutFV.text = Contract.FV.ToString();
                ContractReadoutSV.text = Contract.SV.ToString();
                ContractReadoutSign.text = "X";
                break;
            case Contract.Arithmetic.Divide:
                ContractReadoutFV.text = Contract.FV.ToString();
                ContractReadoutSV.text = Contract.SV.ToString();
                ContractReadoutSign.text = "/";
                break;
            case Contract.Arithmetic.Modulo:
                ContractReadoutFV.text = Contract.FV.ToString();
                ContractReadoutSV.text = Contract.SV.ToString();
                ContractReadoutSign.text = "%";
                break;

        }

        if (Enemies.Length == 1){

            CompleteLevel = true;
        }

        if (!HaveContract)
        {
            if (!CompleteLevel)
            {
                GiveNewContract();
                
                
            }
        }

        if(ContractsToDo <= 0)
        {
            CompleteLevel = true;
        }


        if (CompleteLevel)
        {
            CompletedAllContracts();
        }
    }


    void GiveNewContract()
    {
        Contract.GenerateContract();
        HaveContract = true;

        GameObject.FindGameObjectWithTag("ContractCanvas").GetComponent<Animation>().Play();


        //select random member of the enemies array
       int RandomEnemy;
        RandomEnemy = Random.Range(0,Enemies.Length);
       // Debug.Log(RandomEnemy);
        //Assign Contract Result to Random Enemy. 
        Enemies[RandomEnemy].transform.Find("EnemyValueText").GetComponent<TextMesh>().text = Contract.Results.ToString();
       
        for (int x = 0; x < Enemies.Length; x++)
        {
            //disable mesh renderer of all Objects until player approaches them
            Enemies[x].transform.Find("EnemyValueText").GetComponent<MeshRenderer>().enabled = false;
            //Assign enemy level text so that it doesn't overwrite run statements by being placed in update on enemy script
            Enemies[x].GetComponent<EnemyScript>().EC.EnemyLevelText.text = Enemies[x].GetComponent<EnemyScript>().EC.LevelName;

            if (x != RandomEnemy)
            {
                //Assign Numbers to All other enemies
                Enemies[x].GetComponent<EnemyScript>().EC.EnemyValue = Random.Range(-Contract.Range, Contract.Range);
           

            }

            else if(x == RandomEnemy)
            {
                Enemies[x].GetComponent<EnemyScript>().EC.EnemyValue = Contract.Results;
            }
        }
    }


    void CompletedAllContracts()
    {
        
        //Debug.Log("Completed Level!!!");

        ContractReadoutSV.text = " ";
        ContractReadoutFV.text = " ";

        //Spawn Exit
        ExitConstruct.SetActive(true);

        if(ClearGate)
        PlayerPrefs.SetInt(PlayerPrefName_ToClearGate, 1);
    }
}
