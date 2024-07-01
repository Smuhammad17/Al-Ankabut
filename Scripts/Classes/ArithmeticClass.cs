using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArithmeticClass
{
    public enum Arithmetic { Add, Subtract, Multiply, Divide, Modulo /*Round, Absolute*/ };

    private int selectArithmeticOp;
    public Arithmetic ArithmeticOp;

    public int Evaluate (int FirstValue, int SecondValue)
    {
        int Result = 0;
        selectArithmeticOp = Random.Range(1, 4);
        //Debug.Log(selectArithmeticOp);

        switch (selectArithmeticOp)
        {
            case 1:
                ArithmeticOp = Arithmetic.Add;
                break;
            case 2:
                ArithmeticOp = Arithmetic.Subtract;
                break;
            case 3:
                ArithmeticOp = Arithmetic.Multiply;
                break;
           /* case 4:
                ArithmeticOp = Arithmetic.Divide;
                break;
            case 5:
                ArithmeticOp = Arithmetic.Modulo;
                break;
           */
        }

        switch (ArithmeticOp)
        {
            case Arithmetic.Add:
                Result = FirstValue + SecondValue;
                break;
            case Arithmetic.Subtract:
                Result = FirstValue - SecondValue;
                break;
            case Arithmetic.Multiply:
                Result = FirstValue * SecondValue;
                break;
                /*
            case Arithmetic.Divide:
                Result = Mathf.RoundToInt(FirstValue / SecondValue);
                break;
            case Arithmetic.Modulo:
                Result = FirstValue % SecondValue;
                break;
           /*
            case Arithmetic.Round:
                Result = Mathf.Abs(FirstValue);
                break;
            case Arithmetic.Absolute:
                Result = Mathf.Abs(FirstValue);
                break;
           */
        }


        return Result;
    }
}
