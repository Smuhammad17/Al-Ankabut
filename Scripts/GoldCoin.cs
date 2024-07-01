using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject MoneyAwardNotificationOBJ;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TalkNPC")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().RewardSound.pitch = Random.Range(0.90f, 1);


            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().RewardSound.Play();
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(1));
            
            GameObject MANOBJ = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
            MANOBJ.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(1, "+"));

            Destroy(this.gameObject);

        }
         

    }

}
