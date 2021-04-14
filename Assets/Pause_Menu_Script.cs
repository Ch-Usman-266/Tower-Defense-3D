using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Script : MonoBehaviour
{


    // Update is called once per frame


    public Scene_Fader sceneFader;

    private Building_Manager buildManager;


    public void Start()
    {
        buildManager = Building_Manager.instance;
    }

    public void Toggle()
    {
        transform.gameObject.SetActive(!transform.gameObject.activeSelf);

        if(transform.gameObject.activeSelf)
        {
            Time.timeScale = 0f;
            buildManager.PauseBackGroundMusic();
        }
        else
        {
            Time.timeScale = 1f;
            buildManager.PlayBackGroundMusic();
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.Fadeto(SceneManager.GetActiveScene().name);
    }

    public void Menu ()
    {
        Toggle();
        sceneFader.Fadeto("Main Menu");
    }
}
