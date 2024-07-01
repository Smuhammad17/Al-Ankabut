using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSquare : MonoBehaviour
{
    public GameObject[] Sentries;
    public GameObject[] HardSentries;
    
    public Transform[] SpawnPoints;
    public float SpawnSet;
    public float ReqSpawnSet;

    public void Update()
    {
        SpawnSet += 0.1f * Time.deltaTime;
       // Debug.Log(PlayerPrefs.GetInt("Difficulty"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && PlayerPrefs.GetInt("Difficulty") != 0)
        {
            if (SpawnSet >= ReqSpawnSet)
            {
                float Rand = Random.Range(0, 3);

                if (Rand == 0)
                {

                }
                else if (Rand == 1)
                {
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    SpawnSet = 0;
                }

                else if (Rand == 2 && PlayerPrefs.GetInt("Difficulty") != 1 && PlayerPrefs.GetInt("Difficulty") != 3)
                {
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    Instantiate(Sentries[Random.Range(0, Sentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    SpawnSet = 0;
                }

                else if (Rand == 2 && PlayerPrefs.GetInt("Difficulty") == 3)
                {
                    Instantiate(HardSentries[Random.Range(0, HardSentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    Instantiate(HardSentries[Random.Range(0, HardSentries.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, Quaternion.identity);
                    SpawnSet = 0;
                }

            }
            }
    }
}
