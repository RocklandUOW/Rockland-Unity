using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class UI : MonoBehaviour
{
    private VisualElement root;
    private Button takePicture;
    private Button favouriteRock;
    private Label money;

    [SerializeField]
    private CameraManager cam;
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        takePicture = root.Q<Button>("Take-picture");
        favouriteRock = root.Q<Button>("Favourite-rock-btn");
        money = root.Q<Label>("Money-Label");

        takePicture.clicked += () => cam.takePicture();
    }

    public void updateMoney(int currentMoney)
    {
        money.text = currentMoney.ToString();
    }
}
