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

	// Temporary functionality, rewrite later
	private void TakePicture(int maxSize)
	{
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create a Texture2D from the captured image
				Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				// Assign texture to a temporary quad and destroy it after 5 seconds
				GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
				quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
				quad.transform.forward = Camera.main.transform.forward;
				quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

				Material material = quad.GetComponent<Renderer>().material;
				if (!material.shader.isSupported) // happens when Standard shader is not included in the build
					material.shader = Shader.Find("Legacy Shaders/Diffuse");

				material.mainTexture = texture;

				Destroy(quad, 5f);

				// If a procedural texture is not destroyed manually, 
				// it will only be freed after a scene change
				Destroy(texture, 5f);
			}
		}, maxSize, true, NativeCamera.PreferredCamera.Rear);

		Debug.Log("Permission result: " + permission);
	}
}
