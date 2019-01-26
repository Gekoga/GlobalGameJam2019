using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Singleton
    public static Pickup Instance;

    // Events
    public event Action<Pickup> OnPickedUp;
    public event Action<Pickup> OnCanPickup;
    public event Action<Pickup> OnCanNotPickup;

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

    // Fire OnCanPickUp when player enters collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InvokeOnCanPickUp();
        }
    }

    // Fire OnCanNotPickup when the player leaves the collider
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InvokeOnCanNotPickUp();
        }
    }

    #region Events

    // Fires Picked up event
    public void InvokeOnPickedUp()
    {
        OnPickedUp?.Invoke(this);

        Debug.Log($"picked up..");
    }

    private void InvokeOnCanPickUp()
    {
        OnCanPickup?.Invoke(this);
    }

    private void InvokeOnCanNotPickUp()
    {
        OnCanNotPickup?.Invoke(this);
    }
    
    #endregion
}