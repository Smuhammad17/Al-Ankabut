using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Contract : ArithmeticClass
{
    public int ExpPayout;
    public string Title;
    [TextArea]
    public string description;
    public ArithmeticClass ArthOp;


public enum Difficulty {Easy, Medium, Hard, Extreme};
    public Difficulty difficultysetting;
    public bool NegativeNumbers;
    public int Range = 12;
    public int ComplLevelReq;
    
    public int FV;
    public int SV;

  

    public int Results;

    public void GenerateContract()
    {

        if (NegativeNumbers)
        {

            switch (difficultysetting)
            {
                case Difficulty.Easy:
                    Range = 10;
                    FV = Random.Range(-Range, Range);
                    SV = Random.Range(-Range, Range);
                    break;
                case Difficulty.Medium:
                    Range = 12;
                    FV = Random.Range(-Range, Range);
                    SV = Random.Range(-Range, Range);
                    break;
                case Difficulty.Hard:
                    Range = 20;
                    FV = Random.Range(-Range, Range);
                    SV = Random.Range(-Range, Range);
                    break;
                case Difficulty.Extreme:
                    Range = 100;
                    FV = Random.Range(-Range, Range);
                    SV = Random.Range(-Range, Range);
                    break;
            }

        }

        else
        {
            switch (difficultysetting)
            {
                case Difficulty.Easy:
                    Range = 10;
                    FV = Random.Range(0, Range);
                    SV = Random.Range(0, Range);
                    break;
                case Difficulty.Medium:
                    Range = 12;
                    FV = Random.Range(0, Range);
                    SV = Random.Range(0, Range);
                    break;
                case Difficulty.Hard:
                    Range = 20;
                    FV = Random.Range(0, Range);
                    SV = Random.Range(0, Range);
                    break;
                case Difficulty.Extreme:
                    Range = 100;
                    FV = Random.Range(0, Range);
                    SV = Random.Range(0, Range);
                    break;
            }
        }


        Results = Evaluate(FV, SV);
        
    }


}

