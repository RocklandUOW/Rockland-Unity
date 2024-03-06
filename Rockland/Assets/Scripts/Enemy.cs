using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Clicker enemy")]
public class Enemy : ScriptableObject
{
    // Current enemy's stats
    public string enemyName;

    int totalHealth;
    float currentHealth;
    int goldReward;

    // Asks if the current enemy is a boss? If yes HP is massive and add a 1 minute timer to defeat it
    bool isBoss;

    // Boss image texture
    Sprite enemyTexture;

    // Generates appropriate health for the current stage
    public void generateStats(int stage)
    {
        totalHealth = Mathf.FloorToInt(Random.Range(5, 10)*Mathf.Pow(1.1f,stage-1));
        currentHealth = totalHealth;
        goldReward = Mathf.FloorToInt(Random.Range(1, 3)*Mathf.Pow(stage, 1.2f));
    }
}
