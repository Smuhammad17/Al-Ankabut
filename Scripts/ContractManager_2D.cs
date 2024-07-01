using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractManager_2D : MonoBehaviour
{


    public Contract Contract = new Contract();
    public bool HaveContract = false;
    public bool CompleteLevel = false;
    public Text ContractReadoutFV;
    public Text ContractReadoutSV;
    public Text ContractReadoutSign;


    public int ContractsToDo;

    public GameObject[] Enemies;

 


    void Start()
    {
        CompleteLevel = false;


    }



    // Update is called once per frame
    void Update()
    {




        Enemies = GameObject.FindGameObjectsWithTag("FallingSpiders");

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

        if (Enemies.Length == 1)
        {

            CompleteLevel = true;
        }

        if (!HaveContract)
        {
            if (!CompleteLevel)
            {
                GiveNewContract();


            }
        }

        if (ContractsToDo <= 0)
        {
            CompleteLevel = true;
        }


        if (CompleteLevel)
        {
            CompletedAllContracts();
        }
    }


   public void GiveNewContract()
    {
        Contract.GenerateContract();
        HaveContract = true;

        GameObject.FindGameObjectWithTag("ContractCanvas").GetComponent<Animation>().Play();


        //select random member of the enemies array
        int RandomEnemy;
        RandomEnemy = Random.Range(0, Enemies.Length);
        
        

        for (int x = 0; x < Enemies.Length; x++)
        {
                   

            if (x != RandomEnemy)
            {
                //Assign Numbers to All other enemies
                Enemies[x].GetComponent<FallingSpiders>().value = Random.Range(-Contract.Range, Contract.Range);


            }

            else if (x == RandomEnemy)
            {
                Enemies[x].GetComponent<FallingSpiders>().value = Contract.Results;
            }
        }
    }


    void CompletedAllContracts()
    {

        //Debug.Log("Completed Level!!!");

        ContractReadoutSV.text = " ";
        ContractReadoutFV.text = " ";

       
    }

}


