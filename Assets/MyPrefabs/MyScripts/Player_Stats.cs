using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    public static int money;
    public int startingMoney = 1000;

    public static int lives;
    public int startingLives = 20;

    public static int roundsSurvived;
    public static bool allWavesClear;

    public GameObject healthbar;
    public Gradient gradient;

    private Slider slider;

    public TextMeshProUGUI textMainCanvas;

    public Scene_Manager scene;

 void Start()
    {
        allWavesClear = false;

        scene = Scene_Manager.instance;

        roundsSurvived = 0;

        slider = healthbar.GetComponent<Slider>();


        money = startingMoney;
        lives = startingLives;

        SetMaxHealth(lives);
    }

 void FixedUpdate()
    {
        textMainCanvas.SetText(money.ToString("D5"));

        SetCurrentHealth(lives);
    }


    void SetMaxHealth(int health)
{
        slider.maxValue = health;
        slider.value = health;

        healthbar.transform.GetChild(0).GetComponent<Image>().color = gradient.Evaluate(1f);
}

void SetCurrentHealth(int health)
{
        slider.value = health;

        healthbar.transform.GetChild(0).GetComponent<Image>().color = gradient.Evaluate(slider.normalizedValue);
}



    public static void TakeDamage(int dmg)
    {
        Player_Stats.lives -= dmg;
    }

}
