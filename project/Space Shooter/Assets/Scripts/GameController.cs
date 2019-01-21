using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public GameObject boss;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    private WaveTimer waveTimer;

    //cache
    private AudioManager audioManager;
    public string bossBgmName;
    public string bgmName;

    public int hazardCount;
    public int itemCount;
    public float spawnWait;
    public float itemSpawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text gameOverText;
    public Text restartText;
    public Text LevelText;
    public Text itemText;
    public Text eventText;
    
    private bool gameOver;
    private bool restart;
    private bool beforeBoss;
    public bool clearChapter;

    private int score;
    private int gameLevel;
    void Start()
    {
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! NO AUDIO MANAGER FOUND IN THE SCENE!");
        }
        StartCoroutine(SpawnWaves());
        clearChapter = false;
        beforeBoss = true;
        gameOver = false;
        restart = false;
        eventText.text = "";
        itemText.text = "Item : ";
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        gameLevel = 1;
        LevelText.text ="Level "+gameLevel ;
        waveTimer = GetComponent<WaveTimer>();

        UpdateScore();

   
    }

    void Update(){
        hazardCount *= gameLevel;
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        audioManager.PlaySound(bgmName);
        while (beforeBoss)
        {
            Debug.Log("SpawnWaves/while");
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];                
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
            else if (waveTimer.time == 0)
            {
                beforeBoss = false;
                StartCoroutine(BossBattle());
                break;
            }
        }
        yield return new WaitForSeconds(waveWait);

    }

    IEnumerator BossBattle()
    {
        audioManager.PlaySound("alert!");
        GetComponent<AudioSource>();
        eventText.text = "Alert!";
        yield return new WaitForSeconds(5f);
        eventText.text = "";
        audioManager.PlaySound(bossBgmName);
        Vector3 bossSpawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
        Quaternion bossSpawnRotation = Quaternion.identity;
        Instantiate(boss, boss.GetComponent<Transform>().position, boss.GetComponent<Transform>().rotation);
        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }
        else if (clearChapter)
        {
            eventText.text = "Congratulation! You clear the level 1";
            yield return new WaitForSeconds(5f);
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }
    }

    public void AddScore(int newScoreValue){
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }


    public void GameOver() {
        if(score <= 0)
        {
            gameOverText.text = "Are you kidding me?";
        }
        else if(score > 100 && score <= 200){
            gameOverText.text = "Game Over! do some practice dude~ lol";
        }
        else
        {
            gameOverText.text = "ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ";
        }
        gameOver = true;
    }
}
