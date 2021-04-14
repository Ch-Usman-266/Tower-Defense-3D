 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{

    public static Scene_Manager instance;

    public static bool gameIsOver;
    public static bool gameIsWon;


    public GameObject GameOverPanel;
    public GameObject GamePausedPanel;
    public GameObject GameWinPanel;


    private Building_Manager buildManager;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameIsOver = false;

        buildManager = Building_Manager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsOver)
        {
            return;
        }

        if(Player_Stats.lives <=0)
        {
            gameIsOver = true;
            EndGame();
        }

        if(Player_Stats.allWavesClear)
        {
            gameIsWon = true;
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            GamePausedPanel.GetComponent<Pause_Menu_Script>().Toggle();
        }

    }


    public void EndGame()
    {
        GameOverPanel.SetActive(true);
        buildManager.PauseBackGroundMusic();
        buildManager.PlayTheAudio(buildManager.LoseGame);
    }
    public void WinGame()
    {
        buildManager.PauseBackGroundMusic();
        GameWinPanel.SetActive(true);
        buildManager.PlayTheAudio(buildManager.WinGame);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
