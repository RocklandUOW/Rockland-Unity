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
    private Label goldNeededLabel;

    // Buttons
    private Button backButton;
    private Button petRockButton;
    private Button feedButton;
    private Button levelUpButton;

    // Buttons and labels string names
    [Header("Interactible names")]
    [Header("Labels")]
    [SerializeField] private string CoinLabel = "Coin-count";
    [SerializeField] private string RockNameLabel = "Rock-name";
    [SerializeField] private string RockTypeLabel = "Rock-type";
    [SerializeField] private string GoldPerSecondLabel = "Coin-per-second-text";
    [SerializeField] private string GoldPerClickLabel = "Coin-per-tap-text";
    [SerializeField] private string LevelLabel = "Lvl-text";
    [SerializeField] private string GoldNeededLabel = "Coins-needed-container";
    [Header("Buttons")]
    [SerializeField] private string BackButton = "Back-button";
    [SerializeField] private string PetRockButton = "Pet-button";
    [SerializeField] private string FeedButton = "Feed-button";
    [SerializeField] private string LevelUpButton = "Level-up-button";

    // Various managers
    GoldManager goldManager;


    private void OnEnable()
    {
        DeclareVariables();

        //SetButtons();
    }

    private void Start()
    {
        goldManager = (GoldManager)FindFirstObjectByType(typeof(GoldManager));
    }

    private void Update()
    {
        coinLabel.text = goldManager.GetGoldAmount().ToString();
        Debug.Log(goldManager.GetGoldAmount().ToString());
    }

    //private void SetButtons()
    //{
    //    petRockButton.clicked += AddGold();
    //}

    //private Action AddGold()
    //{
    //    throw new NotImplementedException();
    //}

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
        goldNeededLabel = root.Q<Label>(GoldNeededLabel);

        // Buttons
        backButton = root.Q<Button>(BackButton);
        petRockButton = root.Q<Button>(PetRockButton);
        feedButton = root.Q<Button>(FeedButton);
        levelUpButton = root.Q<Button>(LevelUpButton);
    }
}
