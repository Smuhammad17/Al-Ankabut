using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretVisionScript : MonoBehaviour
{
    public GameObject LaserOBJ;
    public ONOFFCoroutine Timing = new ONOFFCoroutine();

    private bool isActive;
  
    public Collider LaserCollider;
    public Transform TurretBody;

    // Start is called before the first frame update
    void Start()
    {
        LaserOBJ.SetActive(false);
        LaserCollider.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        
            StartCoroutine("ActivateLaser");
        TurretBody.transform.Rotate(Vector3.forward * Time.deltaTime *3);

    }

   IEnumerator ActivateLaser()
    {
        
     float LaserTime = Timing.OnTime;
     float WaitTime = Timing.OffTime;
        if (!isActive)
        {
            isActive = true;
            LaserOBJ.SetActive(true);
            LaserCollider.enabled = true;

            yield return new WaitForSeconds(LaserTime);

            LaserOBJ.SetActive(false);
            LaserCollider.enabled = false;

            yield return new WaitForSeconds(WaitTime);
            isActive = false;
        }
    }
}
