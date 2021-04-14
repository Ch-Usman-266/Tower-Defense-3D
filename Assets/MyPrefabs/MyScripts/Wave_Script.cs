using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave_Script : MonoBehaviour
{

    public enum SpawnState {SPAWNNING, WATITNG, COUNTING};


    [System.Serializable]
    public class wave
    {
        public GameObject enemy;
        public int count;
        public int rate;
        public int timeBetweenEnemies;
    }


    public wave[] waves_list;

    public Transform spawnPoint;


    public float timeBetweenWaves = 20f;


    private float waveCountDown = 0;
    private int nextWaveIndex = 0;
    private float searchCountDown = 1f;


    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI waveNumberText;

    private Building_Manager buildManager;

    void Start()
    {
        buildManager = Building_Manager.instance;
        waveCountDown = 5f;

        waveCountdownText = waveCountdownText.GetComponent<TextMeshProUGUI>();
        waveNumberText = waveNumberText.GetComponent<TextMeshProUGUI>();

    }

    public SpawnState state = SpawnState.COUNTING;

    void Update()
    {

        if(Player_Stats.allWavesClear)
        {
            return;
        }


        waveNumberText.SetText("Rounds Survived : " + (Player_Stats.roundsSurvived)+ "/" + (waves_list.Length - 1));

        if(state == SpawnState.WATITNG)
        {

        if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }


        if(waveCountDown <= 0f)
        {
            if(state != SpawnState.SPAWNNING)
            {

            }

            StartCoroutine(SpawnWave(waves_list[nextWaveIndex]));
            waveCountDown = timeBetweenWaves;
        }
        else
        {
            waveCountDown -= Time.deltaTime;
            waveCountdownText.SetText(Mathf.Floor(waveCountDown).ToString());
        }

       
    }

    void WaveCompleted()
    {
        

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWaveIndex >= waves_list.Length-1)
        {
            Player_Stats.allWavesClear = true;
        }
        else
        {
            nextWaveIndex++;
            Player_Stats.roundsSurvived++;
        }

       
    }



    IEnumerator SpawnWave(wave _wave)
    {
        state = SpawnState.SPAWNNING;

        buildManager.PlayTheAudio(buildManager.wavespawn, 0.3f);

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            yield return new WaitForSeconds(_wave.timeBetweenEnemies/_wave.rate);
        }
        state = SpawnState.WATITNG;
        yield break;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }

        }

        return true;

    }


}
