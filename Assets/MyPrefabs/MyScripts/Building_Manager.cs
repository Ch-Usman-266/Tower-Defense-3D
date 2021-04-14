using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building_Manager : MonoBehaviour
{


    public GameObject warningText;
    public Animator warningTextAnim;

    public GameObject rewardText;
    public Animator rewardTextAnim;


    public Turrent_Container tContainer;


    public NodeUI_Sciprt nodeUI;

    public static Building_Manager instance;


    private Turrent_Container turrenToBuild;
    private Ground_Script selectedNode;

    [Header("Particle Effects")]

    public GameObject buildeffect;
    public GameObject upgradeeffect;

    [Header("Turrent Audios")]

    public AudioClip TurrentBuildSound;
    public AudioClip TurrentSellSound;
    public AudioClip TurrentUpgradingSound;

    [Header("Money and Button Audios")]

    public AudioClip ButtonPressedSound;
    public AudioClip MoneySpentSound;
    public AudioClip OutOfMoney;
    public AudioClip CoinsGained;

    [Header("Turrent bullet hit sounds")]

    public AudioClip bulletfiresound;
    public AudioClip missilefiresound;
    public AudioClip laserfiresound;


    [Header("WaveSpawn Sound")]
    public AudioClip wavespawn;

    public AudioClip WinGame;
    public AudioClip LoseGame;



    AudioSource audio;





    private void Awake()
    {
        audio = transform.GetComponent<AudioSource>();
        instance = this;
        warningTextAnim = warningText.GetComponent<Animator>();
        warningText.SetActive(false);
        rewardTextAnim = rewardText.GetComponent<Animator>();
        rewardText.SetActive(false);
    }


    
    public bool CanBuild { get { return turrenToBuild != null; } }
    public bool HasMoney { get { return Player_Stats.money >= turrenToBuild.cost; } }



    public void BuildTurretOn(Ground_Script node)
    {
        

        audio = transform.GetComponent<AudioSource>();

        if(Player_Stats.money < turrenToBuild.cost)
        {
            return;
        }

        ShowCoinsSubtract(turrenToBuild.cost);

        Player_Stats.money -= turrenToBuild.cost;

        GameObject turret = Instantiate(turrenToBuild.turrentPrefab, node.transform.GetChild(1).position, Quaternion.identity);
        GameObject particle = Instantiate(buildeffect, node.transform.GetChild(1).position, Quaternion.identity);


        node.turret = turret;
        node.turretOnNode = turrenToBuild;

        Destroy(particle, 2f);

        PlayTheAudio(TurrentBuildSound,0.5f);
        PlayTheAudio(MoneySpentSound, 0.5f);



    }

    public void UpgradeTurrentOn(Ground_Script node)
    {
        if (node.turretOnNode!=null)
        {
            if(node.turretOnNode.isupgraded)
            {
                ShowWarningMsg("Turret is Already Upgraded !!!");
                PlayTheAudio(OutOfMoney);
                return;
            }
            else
            {
                if (node.turretOnNode.isupgraded)
                {
                    ShowWarningMsg("Turret is Already Upgraded !!!");
                    PlayTheAudio(OutOfMoney);
                    return;
                }

                audio = transform.GetComponent<AudioSource>();


                if (Player_Stats.money < node.turretOnNode.upgradeprice)
                {
                    ShowWarningMsg("Not Enough Money to Upgrade !!!");
                    return;
                }

                Destroy(node.turret.gameObject);

                ShowCoinsSubtract(node.turretOnNode.upgradeprice);

                Player_Stats.money -= node.turretOnNode.upgradeprice;
                shop shopper = shop.instance;


                Vector3 pos = node.transform.GetChild(1).position;
                pos.y += 1f;


                GameObject turret = Instantiate(node.turretOnNode.upgradePrefab, node.transform.GetChild(1).position, Quaternion.identity);
                GameObject particle = Instantiate(upgradeeffect, pos, Quaternion.identity);

                node.turret = turret;

                
                node.turretOnNode = shopper.MatchPrefab(node.turretOnNode.upgradePrefab);



                //node.turretOnNode.turrentPrefab = node.turretOnNode.upgradePrefab;
                //node.turretOnNode.upgradePrefab = null;



                //if (node.turretOnNode.upgradePrefab.name.StartsWith("Missile"))
                //{
                //    node.turretOnNode.cost = 450;
                //    node.turretOnNode.sellprice = 80;
                //    node.turretOnNode.upgradeprice = 150;
                //    node.turretOnNode.isupgraded = true;

                //    print("Upgraded turret is" + node.turretOnNode.isupgraded);
                //}
                //else if (node.turretOnNode.upgradePrefab.name.StartsWith("Laser"))
                //{
                //    node.turretOnNode.cost = 600;
                //    node.turretOnNode.sellprice = 220;
                //    node.turretOnNode.upgradeprice = 200;
                //    node.turretOnNode.isupgraded = true;
                //}



                Destroy(particle, 2f);

                PlayTheAudio(TurrentUpgradingSound, 1f);
            }
        }


        
    }

    public void SellTurrentOn(Ground_Script node)
    {
        ShowCoinsAdd(node.turretOnNode.sellprice);
        Player_Stats.money += node.turretOnNode.sellprice;

        Destroy(node.turret.gameObject);
        node.turret = null;
        node.turretOnNode = null;
        PlayTheAudio(TurrentSellSound, 0.5f);
    }




    public void SelectNode(Ground_Script node)
    {
        

        if(selectedNode == node)
        {
            DeSelectNode();
            return;
        }

        selectedNode = node;
        turrenToBuild = null;
        nodeUI.SetTarget(selectedNode);
        SetUpgradeandSellPrice(node);
    }

    public void DeSelectNode()
    {
        //print("Selected");
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild (Turrent_Container _turrentContainer)
    {
        turrenToBuild = _turrentContainer;
        DeSelectNode();
    }

    public void PlayTheAudio(AudioClip tempaudio)
    {
        audio.PlayOneShot(tempaudio, 1f);
    }

    public void PlayTheAudio(AudioClip tempaudio, float audiolevel)
    {
        audio.PlayOneShot(tempaudio, audiolevel);
    }


    public void ShowOutOfCoins()
    {
        warningText.GetComponent<TextMeshProUGUI>().SetText("Not Enough Coins !!!");
        warningText.SetActive(true);
        warningTextAnim.SetTrigger("NoCoins");
    }

    public void ShowCannotBuildThere()
    {
        warningText.GetComponent<TextMeshProUGUI>().SetText("Can't Build there !!!");
        warningText.SetActive(true);
        warningTextAnim.SetTrigger("NoCoins");
    }

    public void ShowWarningMsg(string input)
    {
        warningText.GetComponent<TextMeshProUGUI>().SetText(input);
        warningText.SetActive(true);
        warningTextAnim.SetTrigger("NoCoins");
    }


    public void ShowCoinsAdd(int input)
    {
        string temp;
        temp = "+ " + input.ToString("000");

        rewardText.GetComponent<TextMeshProUGUI>().SetText(temp);
        rewardText.GetComponent<TextMeshProUGUI>().color = Color.green;



        rewardText.SetActive(true);
        rewardTextAnim.SetTrigger("DisplayReward");

        instance.PlayTheAudio(instance.CoinsGained, 0.5f);
    }

    public void ShowCoinsSubtract(int input)
    {
        string temp;
        temp = "- " + input.ToString("000");

        rewardText.GetComponent<TextMeshProUGUI>().SetText(temp);
        rewardText.GetComponent<TextMeshProUGUI>().color = Color.red;


        rewardText.SetActive(true);
        rewardTextAnim.SetTrigger("DisplayReward");
    }

    public void SetUpgradeandSellPrice(Ground_Script node)
    {
        nodeUI.SetUpgradePrice(node.turretOnNode.upgradeprice);
        nodeUI.SetSellPrice(node.turretOnNode.sellprice);
    }


    public void PauseBackGroundMusic()
    {
        audio.Pause();
    }

    public void PlayBackGroundMusic()
    {
        audio.Play();
    }


}
