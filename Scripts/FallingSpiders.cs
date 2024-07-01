using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FallingSpiders : MonoBehaviour
{
    public int value;
    public TMP_Text EquationText;
    public Vector2 StartLoc;

    ContractManager_2D CM2D;

    public float downspeed;
    public float movespeed = 3f;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        StartLoc = this.transform.position;

        downspeed = Random.Range(0.04f, 0.8f);
        direction = new Vector2(0, -downspeed);
        CM2D = GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractManager_2D>();
    }


    // Update is called once per frame
    void Update()
    {
        EquationText.text = value.ToString();
        this.GetComponent<Rigidbody2D>().AddForce(direction * Time.deltaTime);
        this.GetComponent<Rigidbody2D>().velocity = direction * movespeed;
    }

    public bool HitWithObj = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "2D_PlayerBullet" && !HitWithObj)
        {
            HitWithObj = true;

            if (value == CM2D.Contract.Results)
            {
                Destroy(collision.gameObject);
               

                if (CM2D.ContractsToDo > 0)
                {
                    CM2D.ContractsToDo--;
                    CM2D.GiveNewContract();
                    transform.position = StartLoc;
                    downspeed = Random.Range(0.04f, 0.8f);
                    direction = new Vector2(0, -downspeed);
                    StartCoroutine(ResetHitwithObj());
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Health>().HealthBar.GetComponent<Animation>().Play();
                    
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Health>().HealthBar.GetComponent<Animation>().Play("HealthBarIncrease");

                    
                }
            }

            else
            {
              
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Health>().Health--;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Health>().HealthBar.GetComponent<Animation>().Play();
                transform.position = StartLoc;
                downspeed = Random.Range(0.04f, 0.8f);
                direction = new Vector2(0, -downspeed);
                CM2D.GiveNewContract();
                StartCoroutine(ResetHitwithObj());
            }
        }


        if(collision.gameObject.tag == "PlayerArea2D" && !HitWithObj)
        {
            HitWithObj = true;

           
            transform.position = StartLoc;
            downspeed = Random.Range(0.04f, 0.8f);
            direction = new Vector2(0, -downspeed);

            StartCoroutine(ResetHitwithObj());
        }
    }

    IEnumerator ResetHitwithObj()
    {
        yield return new WaitForSeconds(0.5f);
        HitWithObj = false;
    }
}
