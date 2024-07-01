using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyGate : MonoBehaviour
{
   
    public ParticleSystem part;
    public Animation anim;
    public TMP_Text MoneyText;
    public GameObject UPlayer;

    public int price;
    public int notification_distance;
    public bool purchased = false;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = this.GetComponent<Animation>();
       
        MoneyText = this.transform.Find("MoneyText").GetComponent<TMP_Text>();
       
        UPlayer = GameObject.FindGameObjectWithTag("Player");


        MoneyText.text = "Price: " + price;
    }


    public bool destroying = false;


    public void Update()
    {
        if (purchased)
        {
            MoneyText.text = "<color=green>Purchased!</color>";
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "TalkNPC")
        {

       


            if (!Input.GetKey("e") && !purchased)
                MoneyText.text = "Press E to pay " + price + " to unlock the barrier";

            if (Input.GetKey("e") && !purchased)
            {
                if(UPlayer.GetComponent<Player>().Money < price)
                {
                    MoneyText.text = "<color=red>Insufficent funds</color>";
                }

                if (UPlayer.GetComponent<Player>().Money >= price)
                {
                    if (!destroying)
                    {
                        destroying = true;
                        UPlayer.GetComponent<Player>().StartCoroutine(UPlayer.GetComponent<Player>().SubtractMoney(price));
                        StartCoroutine(DestroyThisGate());

                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TalkNPC")
        {
            MoneyText.text = "Price: " + price;
        }
    }

   public IEnumerator DestroyThisGate()
    {
    
        purchased = true;
        MoneyText.text = "<color=green>Purchased!</color>";
        UPlayer.GetComponent<PlayerSounds>().RewardSound.Play();
        yield return new WaitForSeconds(2f);
        part.Play();
        anim.Play();
        Destroy(this.gameObject, 3f);
        
    }

}
