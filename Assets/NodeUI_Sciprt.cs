using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeUI_Sciprt : MonoBehaviour
{
    public Ground_Script nodeTarget;

    Building_Manager buildManager;

    TextMeshProUGUI Upgradebuttonprice;


    void Start()
    {
        buildManager = Building_Manager.instance;
    }

    //private void OnEnable()
    //{

    //    print(transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).name);
    //    print(transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).name);

    //}

    public void SetTarget(Ground_Script _node)
    {

        nodeTarget = _node;

        transform.position = nodeTarget.transform.GetChild(1).position;

        transform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void SetUpgradePrice(int input)
    {
        string temp ="$ " +  input.ToString("000");
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(temp);
    }
    public void SetSellPrice(int input)
    {
        string temp = "$ " + input.ToString("000");
        transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().SetText(temp);
    }

    public void Upgrade()
    {
        buildManager.UpgradeTurrentOn(nodeTarget);
        buildManager.DeSelectNode();
        
    }

    public void Sell()
    {
        buildManager.SellTurrentOn(nodeTarget);
        buildManager.DeSelectNode();
    }

}
