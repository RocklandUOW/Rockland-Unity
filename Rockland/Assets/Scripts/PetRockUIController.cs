using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class PetRockUIController : MonoBehaviour
{
    private VisualElement root;

    // Buttons
    private Button backButton;
    private Button petRockButton;
    private Button feedButton;
    private Button levelUpButton;

    // Labels
    private Label coinLabel;
    private Label rockNameLabel;
    private Label rockTypeLabel;
    private Label goldPerSecondLabel;
    private Label goldPerClickLabel;
    private Label levelLabel;

    // Buttons and labels string names
    [Header("Interactible names")]
    [Header("Labels")]
    [SerializeField] private string CoinLabel;
    [SerializeField] private string RockNameLabel;
    [SerializeField] private string RockTypeLabel;
    [SerializeField] private string GoldPerSecondLabel;
    [SerializeField] private string GoldPerClickLabel;
    [SerializeField] private string LevelLabel;
    [Header("Buttons")]
    [SerializeField] private string BackButton;
    [SerializeField] private string PetRockButton;
    [SerializeField] private string FeedButton;
    [SerializeField] private string LevelUpButton;

    private void OnEnable()
    {
        DeclareVariables();

        SetButtons();
    }

    private void SetButtons()
    {
        petRockButton.clicked += AddGold();
    }

    private Action AddGold()
    {
        throw new NotImplementedException();
    }

    private void DeclareVariables()
    {
        // Adds functionality to buttons
        root = GetComponent<UIDocument>().rootVisualElement;

        // Buttons
        backButton = root.Q<Button>(BackButton);
        petRockButton = root.Q<Button>(PetRockButton);
        feedButton = root.Q<Button>(FeedButton);
        levelUpButton = root.Q<Button>(LevelUpButton);

        // Labels
        coinLabel = root.Q<Label>(CoinLabel);
        rockNameLabel = root.Q<Label>(RockNameLabel);
        rockTypeLabel = root.Q<Label>(RockTypeLabel);
        goldPerSecondLabel = root.Q<Label>(GoldPerSecondLabel);
        goldPerClickLabel = root.Q<Label>(GoldPerClickLabel);
        levelLabel = root.Q<Label>(LevelLabel);
    }
}
