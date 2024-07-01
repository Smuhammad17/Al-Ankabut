using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChest : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject MoneyAwardNotificationOBJ;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TalkNPC")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().RewardSound.Play();

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddMoney(10));

            GameObject MANOBJ = Instantiate(MoneyAwardNotificationOBJ, transform.position, Quaternion.identity);
            MANOBJ.GetComponent<MoneyAwardNotificationSystem>().StartCoroutine(MANOBJ.GetComponent<MoneyAwardNotificationSystem>().AwardMoneyNotify(10, "+"));

            Destroy(this.gameObject);

        }


    }
}
