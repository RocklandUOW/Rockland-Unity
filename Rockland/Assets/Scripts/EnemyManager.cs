using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Current enemy's stats
    int totalHealth;
    int currentHealth;
    string enemyName;
    int goldReward;

    // Asks if the current enemy is a boss? If yes HP is massive and add a 1 minute timer to defeat it
    bool isBoss;

    // Boss image texture
    Texture2D enemyTexture;
}
