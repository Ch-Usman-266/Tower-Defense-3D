using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Selector_Script : MonoBehaviour
{

    public Scene_Fader sceneFader;


    public Button[] levelButtons;


    // Start is called before the first frame update


    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached",1);


        for (int i=0; i< levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
               
        }
    }



    public void LevelSelect(string level)
    {
        sceneFader.Fadeto(level);
    }
 
}
