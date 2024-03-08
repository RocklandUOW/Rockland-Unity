using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]

public class LoginUIController : MonoBehaviour
{
    private VisualElement root;

    private VisualElement loginPanel;
    private VisualElement loadingPanel;
    private TextField usernameField;
    private TextField passwordField;
    private Label usernamePlaceholder;
    private Label passwordPlaceholder;
    private Button signInButton;
    private Button createAccountButton;

    // control variables
    private string loginStatus = "idle";

    [Header("URL")]
    [SerializeField] private string passCheckURL = "http://10.0.0.159:8000/check_password/";

    // Buttons and labels string names
    [Header("Interactible names")]
    [Header("Visual Elements")]
    [SerializeField] private string loginPanelName = "LoginPanel";
    [SerializeField] private string loadingPanelName = "LoadingPanel";

    [Header("TextField")]
    [SerializeField] private string usernameFieldName = "UsernameField";
    [SerializeField] private string passwordFieldName = "PasswordField";

    [Header("Label")]
    [SerializeField] private string usernamePlaceholderName = "UsernamePlaceholder";
    [SerializeField] private string passwordPlaceholderName = "PasswordPlaceholder";

    [Header("Buttons")]
    [SerializeField] private string signInButtonName = "SignInButton";
    [SerializeField] private string createAccountButtonName = "CreateAccountButton";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFields();
        manageScreen();
    }

    private void OnEnable()
    {
        DeclareVariables();
        SetButtons();
    }

    private void CheckFields()
    {
        if (usernameField.value == "")
        {
            usernamePlaceholder.text = "Username";
        }
        else
        {
            usernamePlaceholder.text = "";
        }

        if (passwordField.value == "")
        {
            passwordPlaceholder.text = "Password";
        }
        else
        {
            passwordPlaceholder.text = "";
        }

    }

    private void SetButtons()
    {
        if (signInButton != null) { signInButton.clicked += () => { StartCoroutine(SignIn(usernameField.value, passwordField.value)); }; }
        else { Debug.LogWarning("No rock button assigned!"); }

        if (createAccountButton != null) { createAccountButton.clicked += () => { Debug.Log("create Account Anjing"); }; }
        else { Debug.LogWarning("No level up button assigned!"); }
    }

    private void DeclareVariables()
    {
        // Adds functionality to buttons
        root = GetComponent<UIDocument>().rootVisualElement;

        // visual elements
        loginPanel = root.Q<VisualElement>(loginPanelName);
        loadingPanel = root.Q<VisualElement>(loadingPanelName);

        // TextFields
        usernameField = root.Q<TextField>(usernameFieldName);
        passwordField = root.Q<TextField>(passwordFieldName);

        // Labels
        usernamePlaceholder = root.Q<Label>(usernamePlaceholderName);
        passwordPlaceholder = root.Q<Label>(passwordPlaceholderName);

        // Buttons
        signInButton = root.Q<Button>(signInButtonName);
        createAccountButton = root.Q<Button>(createAccountButtonName);
    }

    class PasswordCheckData
    {
        public string username;
        public string password;
    }

    class PasswordCheckResponse
    {
        public string message;
    }

    private void manageScreen()
    {
        if (loginStatus == "processing")
        {
            loadingPanel.style.display = DisplayStyle.Flex;
            loginPanel.style.display = DisplayStyle.None;
        }
        else if (loginStatus == "idle")
        {
            loadingPanel.style.display = DisplayStyle.None;
            loginPanel.style.display = DisplayStyle.Flex;
        }
    }

    IEnumerator SignIn(string username, string password)
    {
        loginStatus = "processing";
        PasswordCheckData bodyData = new PasswordCheckData { username = usernameField.value, password = passwordField.value };
        string jsonData = JsonUtility.ToJson(bodyData);

        var request = new UnityWebRequest(passCheckURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("accept", "application/json; charset=UTF-8");
        request.SetRequestHeader("content-type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError)
        {
            PasswordCheckResponse res = JsonUtility.FromJson<PasswordCheckResponse>(request.downloadHandler.text);
            Debug.Log(res.message);
            if (res.message == "match")
            {
                loginStatus = "idle";
                Debug.Log("berhasil login goblog");
                // do something here
            }
            else
            {
                loginStatus = "idle";
                Debug.Log("gagal login dek");
                // do other things here
            }
                
        }
        else
        {
            loginStatus = "idle";
            Debug.Log("Error Anjing");
            Debug.Log(request.error);
        }
    }
}


