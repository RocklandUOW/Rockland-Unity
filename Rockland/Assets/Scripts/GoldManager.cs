using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    [SerializeField]
    private string startingScene = "Home Scene";

    private float actualGoldAmount;
    private int goldAmount;
    private List<GoldSource> goldSources = new List<GoldSource>();

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
        // Reset gold if starting a new game
        if (scene.name == startingScene) // Replace "StartScene" with your actual starting scene name
        {
            goldAmount = 0;
        }
    }

    private void Update()
    {
        UpdateGold(Time.deltaTime);
        goldAmount = (int) actualGoldAmount;
        if (DebugMode) { Debug.Log("Total amount of Gold: " + goldAmount); Debug.Log("Float amount of Gold: " + actualGoldAmount); }
        
    }

    public void AddGoldSource(GoldSource source)
    {
        goldSources.Add(source);
        if (DebugMode) { Debug.Log("Succesfully added source!"); }
    }

    public void RemoveGoldSource(GoldSource source)
    {
        goldSources.Remove(source);
    }

    public float GetTotalGoldAmount()
    {
        float totalGold = 0;
        foreach (GoldSource source in goldSources)
        {
            if (DebugMode) { Debug.Log("Gold Per Tick: " + source.GetGoldPerTick()); }
            totalGold += source.GetGoldPerTick();
        }
        if (DebugMode) { Debug.Log("Total Gold Per Tick " + totalGold); }
        return totalGold;
    }

    public void UpdateGold(float deltaTime)
    {
        float goldEarned = GetTotalGoldAmount() * deltaTime;
        AddGold(goldEarned);
    }

    public void AddGold(float amount)
    {
        actualGoldAmount += amount;
    }

    public bool CheckGold(int amount)
    {
        if (amount <= actualGoldAmount)
        {
            RemoveGold(amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveGold(int amount)
    {
        actualGoldAmount -= amount;
        goldAmount = Mathf.Clamp(goldAmount, 0, int.MaxValue); // Ensure gold doesn't go negative
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }
}

public class GoldSource
{
    public int BaseGoldPerTick = 5;

    public virtual int GetGoldPerTick()
    {
        return BaseGoldPerTick;
    }
}

public class NormalPetRock : GoldSource
{
    public string Name = "Jeff";
    public string RockType = "Andesite";
    public int UpgradeLevel = 1;
    public int TotalGoldToNextLevel = 10;
    public int GoldToNextLevel = 10;

    public void LevelUp()
    {
        UpgradeLevel += 1;
        TotalGoldToNextLevel = Mathf.FloorToInt(10*(10*Mathf.Pow(UpgradeLevel, 2)));
        GoldToNextLevel = TotalGoldToNextLevel;
        Debug.Log("Gold to next level: " + TotalGoldToNextLevel);
        Debug.Log("Gold per 10 to next level: " + GoldToNextLevel);
    }

    public virtual int GetGoldPerTap()
    {
        return Mathf.FloorToInt(BaseGoldPerTick * UpgradeLevel);
    }

    public override int GetGoldPerTick()
    {
        return Mathf.FloorToInt((BaseGoldPerTick * UpgradeLevel)/2);
    }
}