using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground_Script : MonoBehaviour
{

    public Color hoverColor;
    public Color turrentHoverColor;
    private Color startColor;
    private Renderer rend;


    public GameObject turret;
    public Turrent_Container turretOnNode;

    Building_Manager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = transform.GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = Building_Manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Scene_Manager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if(turret == null)
        {
            if(buildManager.HasMoney)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = Color.red;
            }
            
        }
     
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManager.SelectNode(this);
 
        }
        else
        {
            if (!buildManager.CanBuild)
            {
                return;
            }
            if (!buildManager.HasMoney)
            {
                buildManager.PlayTheAudio(buildManager.OutOfMoney);
                buildManager.ShowOutOfCoins();
            }

            buildManager.BuildTurretOn(this);
        }
    }


    public void UpgradeTurrent()
    {

    }



}
