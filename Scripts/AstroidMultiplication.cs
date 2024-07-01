using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMultiplication : MonoBehaviour
{
    AstroidScript astroid;

    void Awake()
    {
        astroid = this.GetComponent<AstroidScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "2D_PlayerBullet")
        {
            if ((this.astroid.size) > astroid.minSize)
            {
                CreateSplit();
            
            }


            Destroy(collision.transform.gameObject);

            
          
        }

    }

    void CreateSplit()
    {
        //some variance so astroids don't spawn inside each other
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AstroidScript half = Instantiate(this.astroid, position, this.astroid.transform.rotation);
        half.size = this.astroid.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.astroid.Speed);

        half.value = Random.Range(-GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractManager_2D>().Contract.Range, GameObject.FindGameObjectWithTag("ContractManager").GetComponent<ContractManager_2D>().Contract.Range);
    }
}
