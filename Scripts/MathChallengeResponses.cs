using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MathChallengeResponses : MonoBehaviour
{
    public GameObject Option;
    public Text Options_Text;
    public int Option_value;
    public Button Option_Button;
    public bool Pressed;

    public Animation Option_Anim;
    public AnimationClip Positive_Anim;
    public AnimationClip Negative_Anim;

  

    public void ReturnAnswer()
    {

        Pressed = true;

    }

    public void Positive()
    {
        Option_Anim.clip = Positive_Anim;

        Option_Anim.Play();

    }

    public void Negative()
    {
        Option_Anim.clip = Negative_Anim;
 
        Option_Anim.Play();
  
    }
}
