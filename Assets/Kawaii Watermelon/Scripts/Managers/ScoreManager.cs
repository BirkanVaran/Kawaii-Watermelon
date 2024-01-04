using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI gameScroeText;
    [SerializeField] private TextMeshProUGUI menuBestScroeText;

    [Header(" Settings ")]
    [SerializeField] private float scoreMultiplier;
    private int score;
    private int bestScore;

    [Header(" Data ")]
    private const string bestScoreKey = "bestScoreKey";

    private void Awake()
    {
        LoadData();
        MergeManager.onMergeProcessed += MergeProcessedCallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
        UpdateBestScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        gameScroeText.text = score.ToString();
    }
    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameOver:
                CalculateBestScore();
                break;
        }
    }

    private void MergeProcessedCallback(FruitType fruitType, Vector2 unused)
    {
        int scoreToAdd = (int)fruitType;
        score += (int)(scoreToAdd * scoreMultiplier);
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        gameScroeText.text = score.ToString();
    }
    private void UpdateBestScoreText()
    {
        menuBestScroeText.text = bestScore.ToString();
    }
    private void CalculateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            SaveData();
        }
    }
    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, bestScore);
    }
}
