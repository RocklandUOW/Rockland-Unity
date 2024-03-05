using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginUIController : MonoBehaviour
{
    private VisualElement root;

    // Textfields for login and signups
    private TextField usernameField;
    private TextField passwordField;

    // Textfields for signups
    private TextField emailField;

    [Header("Text fields")]
    [SerializeField] private string UsernameField;
    [SerializeField] private string PasswordField;
    [SerializeField] private string EmailField;

    private void DeclareVariables()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        usernameField = root.Q<TextField>(UsernameField);
        passwordField = root.Q<TextField>(PasswordField);
        emailField = root.Q<TextField>(EmailField);
    }
}
