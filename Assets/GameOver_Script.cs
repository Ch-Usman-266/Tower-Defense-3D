using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver_Script : MonoBehaviour
{

    private TextMeshProUGUI roundsSurvived;

    public Scene_Fader sceneFader;

    // Start is called before the first frame update
    void OnEnable()
    {
        roundsSurvived = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        string temp = Player_Stats.roundsSurvived.ToString("00");
        roundsSurvived.SetText(temp);



    }


    public void Retry()
    {
        sceneFader.Fadeto(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.Fadeto("Main Menu");
    }

}
