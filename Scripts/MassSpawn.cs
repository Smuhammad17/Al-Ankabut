using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSpawn : MonoBehaviour
{
    public GameObject[] Sentries;
    public Transform[] SpawnPoints;
    public float SpawnSet;
    public float ReqSpawnSet;

    public void Update()
    {
        SpawnSet += 0.1f * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && PlayerPrefs.GetInt("Difficulty") != 0)
        {
            if (SpawnSet >= ReqSpawnSet)
            {
                float Rand = Random.Range(0, 3);

                if (Rand == 0)
                {
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                }

                else if (Rand == 1)
                {
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    SpawnSet = 0;
                }

                else if (Rand == 2)
                {
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    SpawnSet = 0;
                }

            }
        }
    }
}
