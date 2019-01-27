using System;
using Enemy;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    // Misc
    public int EnemyKillCount { get; private set; }
    public int EnemiesInGame;

    // Events
    public event Action<AbstractEnemy> OnEnemyDeath;
    public event Action<GameManager> OnLevelCompleted;

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
        EnemyKillCount++;
        if(EnemyKillCount >= EnemiesInGame)
        {
            LevelsScript.Instance.EndQuestion();
            //RoomManager.Instance.onStageComplete();
            //InvokeOnLevelCompleted();
        }
        Debug.Log($"we killed {enemy.Name}, we have now slain {EnemyKillCount} enemies");
        Destroy(enemy.gameObject);
    }

    // When an item is picked up
    private static void OnPickedUp(Pickup pickup)
    {
        Debug.Log($"picked up {pickup.gameObject.name}");
    }

    // Invokes the OnLevelCompleted event action
    public void InvokeOnLevelCompleted()
    {
        OnLevelCompleted?.Invoke(this);
        Debug.Log("We completed the level");
    }
}