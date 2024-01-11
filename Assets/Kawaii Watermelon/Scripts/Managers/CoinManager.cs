using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [Header(" Variables ")]
    private int coins;
    private const string coinsKey = "coins";
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();
        UpdateCoinsText();

        MergeManager.onMergeProcessed += MergeProcessedCallback;
    }
    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCallback;
    }
    private void MergeProcessedCallback(FruitType fruitType, Vector2 fruitSpawnPos)
    {
        int coinsToAdd = (int)fruitType;
        AddCoins(coinsToAdd);
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        coins = Mathf.Max(0, coins);

        SaveData();
        UpdateCoinsText();
    }
    private void UpdateCoinsText()
    {
        CoinText[] coinTexts = Resources.FindObjectsOfTypeAll(typeof(CoinText)) as CoinText[];

        for (int i = 0; i < coinTexts.Length; i++)
            coinTexts[i].UpdateText(coins.ToString());
    }
    public bool CanPurchase(int price)
    {
        return coins >= price;
    }
    private void LoadData() => coins = PlayerPrefs.GetInt(coinsKey);
    private void SaveData() => PlayerPrefs.SetInt(coinsKey, coins);
}
