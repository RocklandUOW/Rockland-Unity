using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRockManager : MonoBehaviour
{
    NormalPetRock rock;
    GoldManager goldManager;
    private void OnEnable()
    {
        rock = new NormalPetRock();
        rock.BaseGoldPerTick = 5;
        rock.UpgradeLevel = 1;
        goldManager = (GoldManager)FindFirstObjectByType(typeof(GoldManager));
    }

    private void Start()
    {
        goldManager.AddGoldSource(rock);
    }
}
