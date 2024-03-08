using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HomeUIController : MonoBehaviour
{
    private VisualElement root;

    private Label userNameLabel;
    private Label rockFactDescriptionLabel;
    private Label petRockNameLabel;
    private Label petRockTypeLabel;

    private Button dailyRockFactButton;
    private Button managePetRockButton;
    private Button burgerMenuButton;

    private ScrollView mainBodyContainerScroll;

    [Header("Interactable names")]
    [Header("Labels")]
    [SerializeField] private string UserName = "User-name";
    [SerializeField] private string RockFactDescription = "Daily-fact-description";
    [SerializeField] private string PetRockName = "Pet-name";
    [SerializeField] private string PetRockType = "Pet-type";
    [Header("Buttons")]
    [SerializeField] private string DailyRockFact = "Daily-rock-fact-container-button";
    [SerializeField] private string ManagePetRock = "Pet-container-button";
    [SerializeField] private string BurgerMenu = "Burger-menu-button";

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // Labels
        userNameLabel = root.Q<Label>(UserName);
        rockFactDescriptionLabel = root.Q<Label>(RockFactDescription);
        petRockNameLabel = root.Q<Label>(PetRockName);
        petRockTypeLabel = root.Q<Label>(PetRockType);

        // Buttons
        dailyRockFactButton = root.Q<Button>(DailyRockFact);
        burgerMenuButton = root.Q<Button>(BurgerMenu);
        managePetRockButton = root.Q<Button>(ManagePetRock);

        // Others
        mainBodyContainerScroll = root.Q<ScrollView>("Scroll-container");

        // Initialisations
        if (managePetRockButton != null) managePetRockButton.clicked += () => { SceneManager.LoadScene("PetRockScene"); };
        else Debug.LogError("Button initialisation failed -> Undefined reference: \"" + ManagePetRock + "\"");

        if (dailyRockFactButton != null) dailyRockFactButton.clicked += () => { Application.OpenURL("https://www.wikigempedia.com/opal.html"); };
        else Debug.LogError("Button initialisation failed -> Undefined reference: \"" + DailyRockFact + "\"");
    }

    // Update is called once per frame
    void Update()
    {
        userNameLabel.text = "John Doe";
        rockFactDescriptionLabel.text = "Opal is a hydrated amorphous form of silica. Due to its amorphous property, it is classified as a mineraloid, unlike crystalline forms of silica, which are considered minerals. Read more...";
        petRockNameLabel.text = "Johnson D.";
        petRockTypeLabel.text = "Opal";
    }
}
