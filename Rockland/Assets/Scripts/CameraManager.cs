using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public void takePicture()
    {
        Debug.Log("Taking picture");
        if (NativeCamera.IsCameraBusy())
        {
            Debug.Log("Camera busy, try later");
            return;
        }

        else
        {
			TakePicture(512);
        }
    }

	NetworkManager networkManager;
    private void Start()
    {
		networkManager = FindFirstObjectByType<NetworkManager>();
    }

    private void TakePicture(int maxSize)
	{
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create a Texture2D from the captured image
				Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize, false);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}
				print($"Texture readable: {texture.isReadable}");
				networkManager.ImageRecognition(texture);
			}
		}, maxSize, true, NativeCamera.PreferredCamera.Rear);

		Debug.Log("Permission result: " + permission);
	}
}
