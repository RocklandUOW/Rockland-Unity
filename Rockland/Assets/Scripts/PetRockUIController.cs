using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class PetRockUIController : MonoBehaviour
{
    private VisualElement root;

    // Labels
    private Label coinLabel;
    private Label rockNameLabel;
    private Label rockTypeLabel;
    private Label goldPerSecondLabel;
    private Label goldPerClickLabel;
    private Label levelLabel;
    private Label afterLevelLabel;
    private Label goldNeededLabel;

    // Buttons
    private Button backButton;
    private Button petRockButton;
    private Button feedButton;
    private Button levelUpButton;

    // Progress bar
    private ProgressBar levelProgress;

    // Buttons and labels string names
    [Header("Interactible names")]
    [Header("Labels")]
    [SerializeField] private string CoinLabel = "Coin-count";
    [SerializeField] private string RockNameLabel = "Rock-name";
    [SerializeField] private string RockTypeLabel = "Rock-type";
    [SerializeField] private string GoldPerSecondLabel = "Coin-per-second-text";
    [SerializeField] private string GoldPerClickLabel = "Coin-per-tap-text";
    [SerializeField] private string LevelLabel = "Lvl-text";
    [SerializeField] private string AfterLevelLabel = "Level-next";
    [SerializeField] private string GoldNeededLabel = "Coins-needed-container";
    [Header("Buttons")]
    [SerializeField] private string BackButton = "Back-button";
    [SerializeField] private string PetRockButton = "Pet-button";
    [SerializeField] private string FeedButton = "Feed-button";
    [SerializeField] private string LevelUpButton = "Level-up-button";
    [Header("Progress bars")]
    [SerializeField] private string LevelProgress = "Level-progress-bar";

    // Various managers
    GoldManager goldManager;
    NormalPetRock currentRock;
    PetRockManager rockManager;

    private void OnEnable()
    {
        DeclareVariables();

        SetButtons();
    }

    private void Start()
    {
        goldManager = (GoldManager)FindFirstObjectByType(typeof(GoldManager));
        rockManager = (PetRockManager)FindFirstObjectByType(typeof(PetRockManager));
        currentRock = rockManager.GetCurrentRock();
    }

    private void Update()
    {
        if (rockNameLabel != null) { rockNameLabel.text = currentRock.Name; }
        if (rockTypeLabel != null) { rockTypeLabel.text = currentRock.RockType; }

        if (coinLabel != null) { coinLabel.text = goldManager.GetGoldAmount().ToString(); }

        if (goldPerClickLabel != null) { goldPerClickLabel.text = currentRock.GetGoldPerTap().ToString() + " / Tap"; }
        if (goldPerSecondLabel != null) { goldPerSecondLabel.text = currentRock.GetGoldPerTick().ToString() + " / Second"; }
        
        int i = Mathf.FloorToInt(currentRock.TotalGoldToNextLevel / 10);
        if (goldNeededLabel != null) { goldNeededLabel.text = i.ToString(); }

        if (levelLabel != null) { levelLabel.text = currentRock.UpgradeLevel.ToString(); }
        if (afterLevelLabel != null) { afterLevelLabel.text = (currentRock.UpgradeLevel + 1).ToString(); }

        if (levelProgress != null) { levelProgress.highValue = currentRock.TotalGoldToNextLevel; levelProgress.value = currentRock.TotalGoldToNextLevel - currentRock.GoldToNextLevel; }
  
    }

    private void SetButtons()
    {
        if (petRockButton != null) { petRockButton.clicked += () => { rockManager.GenerateGold(); }; }
        else { Debug.LogWarning("No rock button assigned!"); }

        if (levelUpButton != null) { levelUpButton.clicked += () => { rockManager.LevelUpRock(); }; }
        else { Debug.LogWarning("No level up button assigned!"); }
    }

    private void DeclareVariables()
    {
        // Adds functionality to buttons
        root = GetComponent<UIDocument>().rootVisualElement;

        // Labels
        coinLabel = root.Q<Label>(CoinLabel);
        rockNameLabel = root.Q<Label>(RockNameLabel);
        rockTypeLabel = root.Q<Label>(RockTypeLabel);
        goldPerSecondLabel = root.Q<Label>(GoldPerSecondLabel);
        goldPerClickLabel = root.Q<Label>(GoldPerClickLabel);
        levelLabel = root.Q<Label>(LevelLabel);
        afterLevelLabel = root.Q<Label>(AfterLevelLabel);
        goldNeededLabel = root.Q<Label>(GoldNeededLabel);

        // Buttons
        backButton = root.Q<Button>(BackButton);
        petRockButton = root.Q<Button>(PetRockButton);
        feedButton = root.Q<Button>(FeedButton);
        levelUpButton = root.Q<Button>(LevelUpButton);

        // Progress Bars
        levelProgress = root.Q<ProgressBar>(LevelProgress);
    }
}
