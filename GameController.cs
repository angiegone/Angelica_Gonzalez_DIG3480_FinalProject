using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private int timeLeft = 60;

    public Text restartText;
    public Text gameOverText;
    public Text scoreText;
    public Text winText;
    public Text timeText;

    public AudioClip winSong;
    public AudioClip loseSong;
    public AudioSource musicSource;

    private int score;
    private bool gameOver;
    private bool restart;
    private bool alreadyPlayed;

   

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
    }

    void Update()
    {
        timeText.text = ("" + timeLeft);

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                SceneManager.LoadScene("Angelica_Gonzalez_Final");
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win!";
            gameOverText.text = "Created by Angelica Gonzalez";
            if (!alreadyPlayed)
            {
                musicSource.PlayOneShot(winSong);
                alreadyPlayed = true;
            }
            gameOver = true;
            restart = true;

        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over! - Created by Angelica Gonzalez";
        musicSource.PlayOneShot(loseSong);
        if(timeLeft < 0)
        gameOver = true;
    }

}
