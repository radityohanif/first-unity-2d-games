#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject block;
    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;
    public GameObject tapText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;
    private bool gameStarted = false;
    private GameData gameData;
    private PlayerData playerData;

    void Start()
    {
        gameData = new GameData();
        playerData = gameData.GetPlayerData();
        SetHighScoreText(playerData.highScore);
        PlayerPrefs.SetInt("highScore", playerData.highScore);
        PlayerPrefs.SetInt("score", score);
    }

    void Update()
    {
        // Bagian kode ini hanya akan berjalan satu kali jika game sudah dimulai
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();
            gameStarted = true;
            tapText.SetActive(false);
        }
    }

    void SetHighScoreText(int value)
    {
        highScoreText.text = "HighScore : " + Util.FormatNumber(value);
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 1f, spawnRate);
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX); // Generate bilangan random dari 2 bilangan berikut ini
        Instantiate(block, spawnPos, Quaternion.identity); // Generate block baru
        score++; // Menambahkan nilai variable score
        PlayerPrefs.SetInt("score", score);
        scoreText.text = score.ToString(); // Menampilkan nilai score pada ui game
    }
}
#endif
