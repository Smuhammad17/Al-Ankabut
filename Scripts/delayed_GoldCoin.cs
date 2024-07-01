using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayed_GoldCoin : MonoBehaviour
{


 

    public GameObject MoneyAwardNotificationOBJ;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TalkNPC")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().RewardSound.Play();

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(1));


            Destroy(this.gameObject);

        }


    }
}
