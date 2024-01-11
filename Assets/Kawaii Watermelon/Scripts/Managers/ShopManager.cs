using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SkinButton skinButtonPrefab;
    [SerializeField] private Transform skinButtonParents;
    [SerializeField] private GameObject purchaseButton;
    [SerializeField] private TextMeshProUGUI skinLabelText;
    [SerializeField] private TextMeshProUGUI skinPriceText;

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDataSOs;
    private bool[] unlockedStates;
    private const string skinButtonKey = "SkinButton_";
    private const string lastSelectedSkinKey = "LastSelectedSkin";

    [Header(" Variables ")]
    private int lastSelectedSkin;

    [Header(" Actions ")]
    public static Action<SkinDataSO> onSkinSelected;

    private void Awake()
    {
        unlockedStates = new bool[skinDataSOs.Length];
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        LoadData();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PurchaseButtonCallback()
    {
        CoinManager.instance.AddCoins(-skinDataSOs[lastSelectedSkin].GetPrice());

        // Check if we have enough coins

        unlockedStates[lastSelectedSkin] = true;
        SaveData();

        // Calling the method to trigger the event and hide the purchase button
        SkinButtonClickedCallback(lastSelectedSkin);
    }
    private void Initialize()
    {
        // Hide the purchase button
        purchaseButton.SetActive(false);

        for (int i = 0; i < skinDataSOs.Length; i++)
        {
            SkinButton skinButtonInstance = Instantiate(skinButtonPrefab, skinButtonParents);
            skinButtonInstance.Configure(skinDataSOs[i].GetObjectPrefabs()[5].GetSprite());
            if (i == 0)
                skinButtonInstance.Select();

            int j = i;
            skinButtonInstance.GetButton().onClick.AddListener(() => SkinButtonClickedCallback(j));
        }
    }
    private void SkinButtonClickedCallback(int skinButtonIndex, bool sholdSaveLastSkin = true)
    {
        lastSelectedSkin = skinButtonIndex;
        for (int i = 0; i < skinButtonParents.childCount; i++)
        {
            SkinButton currentSkinButton = skinButtonParents.GetChild(i).GetComponent<SkinButton>();
            if (i == skinButtonIndex)
                currentSkinButton.Select();
            else
                currentSkinButton.Unselect();
        }
        if (IsSkinUnlocked(skinButtonIndex))
        {
            onSkinSelected?.Invoke(skinDataSOs[skinButtonIndex]);
            if (sholdSaveLastSkin)
                SaveLastSelectedSkin();
        }
        ManagePurchaseButtonVisibility(skinButtonIndex);
        UpdateSkinLabel(skinButtonIndex);
    }
    private void UpdateSkinLabel(int skinButtonIndex)
    {
        skinLabelText.text = skinDataSOs[skinButtonIndex].GetName();
    }
    private void ManagePurchaseButtonVisibility(int skinButtonIndex)
    {
        bool canPurchase = CoinManager.instance.CanPurchase(skinDataSOs[lastSelectedSkin].GetPrice());
        purchaseButton.GetComponent<Button>().interactable = canPurchase;
       
        purchaseButton.SetActive(!unlockedStates[skinButtonIndex]);

        //if (unlockedStates[skinButtonIndex])
        //    purchaseButton.SetActive(false);
        //else
        //    purchaseButton.SetActive(true);

        skinPriceText.text = skinDataSOs[skinButtonIndex].GetPrice().ToString();

    }
    private bool IsSkinUnlocked(int skinButtonIndex)
    {
        return unlockedStates[skinButtonIndex];
    }
    private void LoadData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockedValue = PlayerPrefs.GetInt(skinButtonKey + i);
            if (i == 0)
                unlockedValue = 1;

            if (unlockedValue == 1)
                unlockedStates[i] = true;
        }
        LoadLastSelectedSkin();
    }
    private void SaveData()
    {
        for (int i = 0; i < unlockedStates.Length; i++)
        {
            int unlockedValue = unlockedStates[i] ? 1 : 0;
            PlayerPrefs.SetInt(skinButtonKey + i, unlockedValue);

        }
    }
    private void LoadLastSelectedSkin()
    {
        int lastSelectedSkinIndex = PlayerPrefs.GetInt(lastSelectedSkinKey);
        SkinButtonClickedCallback(lastSelectedSkinIndex, false); //Sending false to prevent saving while loadin the data
    }
    private void SaveLastSelectedSkin()
    {
        PlayerPrefs.SetInt(lastSelectedSkinKey, lastSelectedSkin);
    }
}