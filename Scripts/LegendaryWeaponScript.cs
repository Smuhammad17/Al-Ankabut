using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegendaryWeaponScript : MonoBehaviour
{

    public int AlRadPrice;
    public int AlHijarPrice;
    public int AlHidadPrice;
    public GameObject KhadimsThoughts;
    public Image ScreenICON;
    public GameObject OBJBase;

    public AudioSource LegendaryWeaponsMusic;
  

    public void Update()
    {
        
           OBJBase.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

          
    }

    bool purchased = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            Player PlayerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (!LegendaryWeaponsMusic.isPlaying)
            {
                PlayerMoney.LgndMusicPlaying = true;
                LegendaryWeaponsMusic.Play();
                
            }

          
            GameObject DiaCanvas;
            GameObject BackPanel;
            Text NPCText;
            Text NPCNameBox;

            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
            ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
            NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
            NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();
            NPCNameBox.text = "Legendary Weapons Station";
            
            if(!purchased)
            NPCText.text = "Want to buy a legendary weapon? Step right up! \n Press 1 to purchase ammo for <i><color=#b96ddc>Al-Ra'd</color></i> <color=aqua>(" + AlRadPrice.ToString()+ ")</color> \n Press 2 to purchase ammo for <i><color=yellow>Al-Hijar</color></i> <color=aqua>(" + AlHijarPrice.ToString() + ")</color> \n Press 3 to purchase ammo for <i><color=orange>Al-Hidad</color></i> <color=aqua>(" +AlHidadPrice.ToString() + ")</color> ";
            GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>().enabled = true;

            KhadimsThoughts = DiaCanvas.transform.Find("KhadimsThoughts").gameObject;

            KhadimsThoughts.SetActive(false);

            ScreenICON.enabled = false;

            if (Input.GetKeyDown("1"))
            {

                if (PlayerMoney.Money >= AlRadPrice && !purchased)
                {
                    purchased = true;
                    StartCoroutine(PurchaseWeapon(1));
                }

                else
                {
                    //INSUFICCIENT MONEY WEAPON
                    purchased = true;
                    StartCoroutine(PurchaseWeapon(4));
                }
            }

            if (Input.GetKeyDown("2"))
            {
                if (PlayerMoney.Money >= AlHijarPrice && !purchased)
                {
                    purchased = true;
                    StartCoroutine(PurchaseWeapon(2));
                }

                else
                {
                    //INSUFICCIENT MONEY WEAPON
                    purchased = true;
                    StartCoroutine(PurchaseWeapon(4));
                }
            }

            if (Input.GetKeyDown("3"))
            {
                if (PlayerMoney.Money >= AlHidadPrice && !purchased)
                {
                    purchased = true;
                    StartCoroutine(PurchaseWeapon(3));
                }

                else
                {
                    //INSUFICCIENT MONEY WEAPON
                    purchased = true;
                    StartCoroutine(PurchaseWeapon(4));
                }
            }

        


            



        }
    }

    public IEnumerator PurchaseWeapon(int weapon)
    {
        Player PlayerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject DiaCanvas;
        GameObject BackPanel;
        Text NPCText;
        Text NPCNameBox;

        DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        BackPanel = DiaCanvas.transform.Find("BackPanel").gameObject;
        ScreenICON = DiaCanvas.transform.Find("BackPanel").transform.Find("ICON").GetComponent<Image>();
        NPCText = BackPanel.transform.Find("UIDia").GetComponent<Text>();
        NPCNameBox = BackPanel.transform.Find("UIName").GetComponent<Text>();

        switch (weapon)
        {
            case 1:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(5);
                NPCText.text = "<color=#b96ddc>Legendary Weapon - Al-Ra'd Purchased (x5)</color>";
                StartCoroutine(PlayerMoney.SubtractMoney((int)AlRadPrice));
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().LegendaryWeaponAmmo += 5;
                break;

            case 2:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(6);
                NPCText.text = "<color=yellow>Legendary Weapon - Al-Hijar Purchased (x5)</color>";
                StartCoroutine(PlayerMoney.SubtractMoney(AlHijarPrice));
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().LegendaryWeaponAmmo += 5;
                break;

            case 3:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().SwitchWeapon(7);
                NPCText.text = "<color=orange>Legendary Weapon - Al-Hidad Purchased (x5)</color>";
                StartCoroutine(PlayerMoney.SubtractMoney((int)AlHidadPrice));
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat_Engagement>().LegendaryWeaponAmmo += 5;
                break;

            case 4:
                NPCText.text = "<color=orange>NOT ENOUGH MONEY!!!</color>";
                break;

        }

        if(weapon != 4)
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().RewardSound.Play();

        yield return new WaitForSeconds(2);
        purchased = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TalkNPC")
        {
            GameObject DiaCanvas;
            DiaCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
            DiaCanvas.GetComponent<Canvas>().enabled = false;
            StartCoroutine(StopMusic());
        }
    }

    IEnumerator StopMusic()
    {
        yield return new WaitForSeconds(2f);
        LegendaryWeaponsMusic.Stop();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().LgndMusicPlaying = false;
    }
}
