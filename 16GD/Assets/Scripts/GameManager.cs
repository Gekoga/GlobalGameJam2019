using System;
using Enemy;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    // Events
    public event Action<AbstractEnemy> OnEnemyDeath;

    // Misc
    private int enemyKillCount;

    // Makes sure there is only one instance of the GameManager in the scene
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Used for initializing
    private void Start()
    {
        // Initiating
        if (Pickup.Instance != null)
        {
            Pickup.Instance.OnPickedUp += OnPickedUp;
        }
    }

    // When an enemy died
    public void EnemyDeath(AbstractEnemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
        enemyKillCount++;
        Debug.Log($"we killed {enemy.Name}, we have now slain {enemyKillCount} enemies");
        Destroy(enemy.gameObject);
    }

    // When an item is picked up
    private static void OnPickedUp(Pickup pickup)
    {
        Debug.Log($"picked up {pickup.gameObject.name}");
    }
}