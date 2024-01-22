using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject mapPanel;

    [Header(" Actions ")]
    public static Action onMapOpened;
    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
        LevelMapManager.onLevelButtonClicked += LevelButtonCallback;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        LevelMapManager.onLevelButtonClicked -= LevelButtonCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        // SetMenu();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;

            case GameState.Game:
                SetGame();
                break;

            case GameState.GameOver:
                SetGameOver();
                break;
        }
    }
    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mapPanel.SetActive(false);
    }
    private void SetGame()
    {
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        mapPanel.SetActive(false);
    }
    private void SetGameOver()
    {
        gameOverPanel.SetActive(true);
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
    }
    public void LevelButtonCallback()
    {
        GameManager.instance.SetGameState();
        SetGame();
    }
    public void NextButtonCallback()
    {
        SceneManager.LoadScene(0);
    }
    public void SettingButtonCallBack()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
    public void ShopButtonCallback() => shopPanel.SetActive(true);
    public void CloseShopPanel() => shopPanel.SetActive(false);
    public void OpenMap()
    {
        mapPanel.SetActive(true);
        onMapOpened?.Invoke();
    }
    public void CloseMap() => mapPanel.SetActive(false);
}
