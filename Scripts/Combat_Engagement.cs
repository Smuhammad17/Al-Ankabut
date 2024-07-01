using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Engagement : MonoBehaviour
{

    public Player PlayerScript;
    float FiringAnimCooldown = 0;
    public Animator Anim;
    public ParticleSystem UpgradeEffects;
    public enum FireType { AtomBlast, TripleFire, SwordOfTheSpirit, SabreoftheSureTruth, StarsofJustice, Al_Rad, Al_Hadid, Al_Hijar }
    public FireType PlayerFireType;

    //Instantiated Objects
    public GameObject AtomBlastOBJ;
    public GameObject SpiritswordsOBJ;
    public GameObject StarOfJusticeOBJ;



    //Legendary Weapon Ammo
   public int LegendaryWeaponAmmo = 0;
    //Legendary Weapon ICONS
    public Sprite[] LegendaryWeaponIcons;

    //Spawn Points
    public Transform SingularOBJ_SpawnPoint;

    public Transform[] MultiOBJ_SpawnPoints;


    public Advanced_Combat_Engagement ACE;

    // Start is called before the first frame update
    void Start()
    {
        ACE = this.GetComponent<Advanced_Combat_Engagement>();

        ThunderSpawn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PlayerScript.HandsFull && !PlayerScript.FinaleCutscene)
        {
            if(ACE.GetSkillMultiplier() > 0)
            {
             for(int x = 0; x < ACE.GetSkillMultiplier();x++)
                {
                    ATOMBlast(MultiOBJ_SpawnPoints[x]);
                }
            }
           
                switch (PlayerFireType)
                {

                    case FireType.AtomBlast:
                        ATOMBlast(SingularOBJ_SpawnPoint);
                        
                        break;

                    case FireType.TripleFire:
                        TriBlast();
                        break;

                    case FireType.SwordOfTheSpirit:
                        SpiritSlash();
                        break;

                    case FireType.SabreoftheSureTruth:
                        SabreOfTheSureTruth();
                        break;

                    case FireType.StarsofJustice:
                        StarsOfJustice();
                        break;

                    case FireType.Al_Rad:
                        StartCoroutine(Al_Rad());
                        break;

                case FireType.Al_Hijar:
                    StartCoroutine(Al_Hijar(MeteorSpawnPoint));
                    break;

                case FireType.Al_Hadid:
                    StartCoroutine(Al_Hadid(MeteorSpawnPoint));
                    break;

                }
            

            FiringAnimCooldown = 1;
            Anim.SetFloat("Firing", FiringAnimCooldown);
        }

        //Reduce Firing Anim Parameter per time
        if (FiringAnimCooldown > 0)
        {
            FiringAnimCooldown -= 0.9f * Time.deltaTime;
            Anim.SetFloat("Firing", FiringAnimCooldown);
        }

        if (PlayerFireType == FireType.Al_Hadid || PlayerFireType == FireType.Al_Hijar || PlayerFireType == FireType.Al_Rad)
            GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("Legendary Weapon Ammo Count").GetComponent<Text>().text = LegendaryWeaponAmmo.ToString();
        else
            GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("Legendary Weapon Ammo Count").GetComponent<Text>().text = " ";



        if (LegendaryWeaponAmmo <= 0)
        {
            LegendaryWeaponAmmo = 0;
        
        }


        switch (PlayerFireType)
        {

            case FireType.AtomBlast:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[0])
                {
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[0];
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                }
                break; 

            case FireType.SwordOfTheSpirit:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[1]) { 
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[1];
                GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                     }
        break;

            case FireType.SabreoftheSureTruth:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[2]) { 
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[2];
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                }
                break;

            case FireType.StarsofJustice:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[3]) { 
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[3];
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                }
                break;

            case FireType.Al_Rad:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[4]) { 
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[4];
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                }
                break;

            case FireType.Al_Hijar:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[5]) { 
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[5];
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                }
                break;

            case FireType.Al_Hadid:
                if (GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite != LegendaryWeaponIcons[6]) { 
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Image>().sprite = LegendaryWeaponIcons[6];
                    GameObject.FindGameObjectWithTag("LevelCanvas").transform.Find("Money and Legendary Weapons Hud").transform.Find("LegendaryWeaponICON").GetComponent<Animation>().Play();
                }
                break;

        }



    }



    void ATOMBlast(Transform spawnPoint)
    {

        Instantiate(AtomBlastOBJ, spawnPoint.transform.position, Quaternion.identity);

    }

    void TriBlast()
    {
        Instantiate(AtomBlastOBJ, new Vector3(SingularOBJ_SpawnPoint.transform.position.x, SingularOBJ_SpawnPoint.transform.position.y - 0.1f, SingularOBJ_SpawnPoint.transform.position.z), Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y + 15, Quaternion.identity.z));
        Instantiate(AtomBlastOBJ, new Vector3(SingularOBJ_SpawnPoint.transform.position.x, SingularOBJ_SpawnPoint.transform.position.y + 0.1f, SingularOBJ_SpawnPoint.transform.position.z), Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y - 15, Quaternion.identity.z));
        Instantiate(AtomBlastOBJ, SingularOBJ_SpawnPoint.transform.position, Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, Quaternion.identity.z));

    }


    void SpiritSlash()
    {
        Instantiate(SpiritswordsOBJ, SingularOBJ_SpawnPoint.transform.position, Quaternion.identity);
    }

    public GameObject Sabre;
    void SabreOfTheSureTruth()
    {

        bool UnsheathSword = false;
        if (Input.GetMouseButton(0) && !UnsheathSword)
        {
            UnsheathSword = true;
            Sabre.SetActive(true);

        }




    }

    void StarsOfJustice()
    {
        for(int x = 0; x < 2; x++)
        Instantiate(StarOfJusticeOBJ, MultiOBJ_SpawnPoints[x].transform.position, Quaternion.identity);
    }


    //LEGENDARY WEAPONS ---------------------------------------------------------

    //AL_Ra'd - The Thunder
    public GameObject ThunderSpawn;
    public ParticleSystem ThunderSpawnEffects;
    public bool ThunderActivated = false;
    public IEnumerator Al_Rad()
    {
        if (!ThunderActivated && LegendaryWeaponAmmo > 0)
        {
            ThunderActivated = true;
            ThunderSpawn.SetActive(true);
            ThunderSpawnEffects.Play();

            yield return new WaitForSeconds(3f);
            ThunderActivated = false;
            ThunderSpawn.SetActive(false);

            LegendaryWeaponAmmo--;
        }

        else if (!ThunderActivated && LegendaryWeaponAmmo == 0)
        {
            SwitchWeapon(1);
        }
    
       

    }

    //IRON METEOR - AL_Hadid
    public GameObject IronMeteor;
    public Transform MeteorSpawnPoint;
    bool meteorSent;
    public IEnumerator Al_Hadid(Transform spawnPoint)
    {
        if (!meteorSent && LegendaryWeaponAmmo > 0)
        {
            meteorSent = true;
            Instantiate(IronMeteor, spawnPoint.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
            meteorSent = false;

            LegendaryWeaponAmmo--;
        }


        else if (!meteorSent && LegendaryWeaponAmmo == 0)
        {
            SwitchWeapon(1);
        }
    }

    //Al Hijar - The Rock
    public GameObject AL_Hijar;
    bool rockSent;
    public IEnumerator Al_Hijar(Transform spawnPoint)
    {
        if (!rockSent && LegendaryWeaponAmmo > 0)
        {
            rockSent = true;
         for(int x = 0; x < Random.Range(6,13); x++)
            {
                Instantiate(AL_Hijar, (spawnPoint.transform.position + new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * Random.Range(10, 13)),Quaternion.identity);
            }

            yield return new WaitForSeconds(1f);
            rockSent = false;

            LegendaryWeaponAmmo--;
        }


        else if (!rockSent &&LegendaryWeaponAmmo == 0)
        {
            SwitchWeapon(1);
        }
    }

   



    public void SwitchWeapon(int Option)
    {
        switch (Option)
        {
            case 1:
                PlayerFireType = FireType.AtomBlast;
                break;
            case 2:
                PlayerFireType = FireType.SwordOfTheSpirit;
                break;
            case 3:
                PlayerFireType = FireType.SabreoftheSureTruth;
                break;

            case 4:
                PlayerFireType = FireType.StarsofJustice;
                break;

            case 5:
                PlayerFireType = FireType.Al_Rad;
                break;

            case 6:
                PlayerFireType = FireType.Al_Hijar;
                break;

            case 7:
                PlayerFireType = FireType.Al_Hadid;
                break;
        }
    }

}
