using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    [SerializeField]
    private string startingScene = "Home Scene";


    private int goldAmount;
    private List<GoldSource> goldSources = new List<GoldSource>();

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
        // Reset gold if starting a new game
        if (scene.name == startingScene) // Replace "StartScene" with your actual starting scene name
        {
            goldAmount = 0;
        }
    }

    public void AddGoldSource(GoldSource source)
    {
        goldSources.Add(source);
    }

    public void RemoveGoldSource(GoldSource source)
    {
        goldSources.Remove(source);
    }

    public int GetTotalGoldAmount()
    {
        int totalGold = 0;
        foreach (GoldSource source in goldSources)
        {
            totalGold += source.GetGoldPerTick();
        }
        return totalGold;
    }

    public void UpdateGold(float deltaTime)
    {
        int goldEarned = Mathf.FloorToInt(GetTotalGoldAmount() * deltaTime);
        AddGold(goldEarned);
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
    }

    public void RemoveGold(int amount)
    {
        goldAmount -= amount;
        goldAmount = Mathf.Clamp(goldAmount, 0, int.MaxValue); // Ensure gold doesn't go negative
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
}

public class GoldSource
{
    public int baseGoldPerTick;

    public virtual int GetGoldPerTick()
    {
        return baseGoldPerTick;
    }
}

public class NormalPetRock : GoldSource
{
    [SerializeField]
    private int upgradeLevel;

    public int UpgradeLevel
    {
        get { return upgradeLevel; }
        set { upgradeLevel = value; }
    }

    public override int GetGoldPerTick()
    {
        return (int)Mathf.Floor(baseGoldPerTick * Mathf.Pow(upgradeLevel, 1.1f));
    }
}