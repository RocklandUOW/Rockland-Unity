using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    // Network manager Instance
    [HideInInspector]
    public static NetworkManager Instance;

    // Handling of image recognition
    [SerializeField]
    private string imageRecognitionURL = "https://api.example.com/upload"; // Replace with the Rockland AI service 
    public Texture2D ImageToUpload; // Picture to be sent to AI service
    public string Response;

    private void Awake()
    {
        // Singleton pattern and DontDestroyOnLoad to have a persistent network manager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string ImageRecognition(Texture2D img)
    {
        ImageToUpload = img;
        StartCoroutine(uploadImage());
        return Response;
    }

    private IEnumerator uploadImage()
    {
        byte[] imageData;

        imageData = ImageConversion.EncodeToPNG(ImageToUpload);

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", imageData, "image.png", "image/png");

        using (UnityWebRequest request = UnityWebRequest.Post(imageRecognitionURL, wwwForm))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                // Parse the JSON response and use the data in your game logic
                Debug.Log("Response: " + jsonResponse);
                Response = jsonResponse;
            }
        }
    }
}
