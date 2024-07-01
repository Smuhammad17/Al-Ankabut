using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    public Sprite[] AnimSprites;
    public SpriteRenderer NPCRenderer;
    public float waitTime = 2;
    public bool isPlaying;

    private void Start()
    {
        NPCRenderer.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Player")


        if (!isPlaying)
            StartCoroutine("PlayAnimation");

    }



    IEnumerator PlayAnimation()
    {
        isPlaying = true;
        NPCRenderer.sprite = AnimSprites[0];
        yield return new WaitForSeconds(waitTime);
        NPCRenderer.sprite = AnimSprites[1];
        yield return new WaitForSeconds(waitTime);
        isPlaying = false;
    
    }


}
