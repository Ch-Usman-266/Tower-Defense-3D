using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class shop : MonoBehaviour
{

    public static shop instance;


    private GameObject mainCanvas;

    GameObject Itemtemplate;
    GameObject tempObj;
    [SerializeField] Transform shopScrollViewer;



    [System.Serializable] class shopItem
    {


        public Turrent_Container turrentContainer;
        public Sprite turretImage;

    }

    [SerializeField] List<shopItem> shopItemsList;



    Building_Manager buildManager;

    Scene_Manager sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;


        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
       

        buildManager = Building_Manager.instance;
        sceneManager = Scene_Manager.instance;


        Itemtemplate = shopScrollViewer.GetChild(0).gameObject;


        for (int i = 0; i < shopItemsList.Count; i++)
        {
            tempObj = Instantiate(Itemtemplate, shopScrollViewer);

            tempObj.transform.GetChild(0).GetComponent<Image>().sprite = shopItemsList[i].turretImage;
            tempObj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(shopItemsList[i].turrentContainer.cost.ToString());

            Turrent_Container tempContainer = shopItemsList[i].turrentContainer;
            int tempnumber = i;

            tempObj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { SetOnClickListner(tempnumber, tempContainer); });

        }

        Destroy(Itemtemplate);


        transform.parent.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        sceneManager.PauseGame();

        mainCanvas.transform.GetChild(1).gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        sceneManager.ResumeGame();

        mainCanvas.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void CloseShop()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void OpenShop()
    {

        transform.parent.gameObject.SetActive(true);

        buildManager.PlayTheAudio(buildManager.MoneySpentSound);
    }


    public void SetOnClickListner(int index, Turrent_Container _turrentContainer)
    {
        GameObject[] shopitems = GameObject.FindGameObjectsWithTag("ShopItems");

        for (int i = 0; i < shopitems.Length; i++)
        {
            shopitems[i].transform.GetChild(2).GetComponent<Button>().interactable = true;
            shopitems[i].transform.GetChild(2).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Select");
        }

        shopitems[index].transform.GetChild(2).GetComponent<Button>().interactable = false;
        shopitems[index].transform.GetChild(2).GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Selected");

        //print("Index is" + index);


        buildManager.SelectTurretToBuild(_turrentContainer);
        buildManager.PlayTheAudio(buildManager.ButtonPressedSound);

    }


    public Turrent_Container MatchPrefab(GameObject temp)
    {
        Turrent_Container tempContainer = null;


        for (int i = 0; i < shopItemsList.Count; i++)
        {
            if (shopItemsList[i].turrentContainer.turrentPrefab == temp)
            {
                tempContainer = shopItemsList[i].turrentContainer;
                break;
            }

        }

        return tempContainer;
    }


}
