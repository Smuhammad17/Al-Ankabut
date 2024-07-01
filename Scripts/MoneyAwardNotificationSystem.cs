using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyAwardNotificationSystem : MonoBehaviour
{
    Rigidbody rigid;
    public Animation anim;
    public TMP_Text MoneyText;

    // Start is called before the first frame update
   public void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animation>();
        MoneyText = this.transform.Find("MoneyText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerViewport").position);
    }

    

    public IEnumerator AwardMoneyNotify(int amt, string sign)
    {
      
        yield return new WaitForSeconds(0.1f);
        anim.Play();
        MoneyText.text = sign + amt.ToString();
        rigid.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(20, 25), Random.Range(-2, 2)) * 15);
     
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
