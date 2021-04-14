using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string LevelToFade = "Level Selector";

    public Scene_Fader scene_Fader;

    public void Play()
    {
        scene_Fader.Fadeto(LevelToFade);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
