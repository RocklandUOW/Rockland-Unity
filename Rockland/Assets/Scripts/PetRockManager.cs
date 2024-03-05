using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRockManager : MonoBehaviour
{
    NormalPetRock currentRock;
    GoldManager goldManager;
 
    private void OnEnable()
    {
        currentRock = new NormalPetRock();
    }

    private void Start()
    {
        goldManager = (GoldManager)FindFirstObjectByType(typeof(GoldManager));

        goldManager.AddGoldSource(currentRock);
    }

    public void GenerateGold()
    {
        goldManager.AddGold(currentRock.GetGoldPerTap());
    }

    public NormalPetRock GetCurrentRock()
    {
        return currentRock;
    }

    public void LevelUpRock()
    {
        int i = Mathf.FloorToInt(currentRock.TotalGoldToNextLevel / 10);
        if (goldManager.CheckGold(i) == true)
        {
            currentRock.GoldToNextLevel -= i;
            if (currentRock.GoldToNextLevel <= 0)
            {
                currentRock.LevelUp();
            }
        }
    }
}
