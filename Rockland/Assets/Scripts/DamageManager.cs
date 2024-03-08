using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DamageManager : MonoBehaviour
{
    // Singleton instance
    public static DamageManager Instance;

    // For initialization of values
    [SerializeField]
    private string startingScene = "Home Scene";

    // List of to keep track of all passive damage sources
    private List<DamageSource> damageSources = new List<DamageSource>();

    // For debug logs
    [SerializeField]
    private bool DebugMode = true;

    private void Awake()
    {
        // Singleton instance
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == startingScene)
        {
            // Initialize values for first time launches (network manager functions)
        }
    }

}

public class DamageSource
{
    public int DMGPerTick = 1;

    public virtual int GetDMGPerTick()
    {
        return DMGPerTick;
    }
}