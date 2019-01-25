using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Singleton
    public static Pickup Instance;

    // Events
    public event Action<Pickup> PickedUp;

    private void Awake()
    {
        // Makes sure there is only one in the scene 
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fire OnPickedUp when player enters collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPickedUp();
        }
    }

    // Fires Picked up event
    private void OnPickedUp()
    {
        PickedUp?.Invoke(this);
    }
}