using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTextRotation_2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<RectTransform>().rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
