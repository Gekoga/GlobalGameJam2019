using System;
using UnityEngine;

public class OnTriggerEvent : MonoBehaviour
{
    // Misc
    public string TagNeeded = "Player";

    // Events
    public event Action<OnTriggerEvent, GameObject> OnTriggerEventEnter;
    public event Action<OnTriggerEvent, GameObject> OnTriggerEventExit;

    // When we enter the trigger
    public void OnTriggerEnter(Collider other)
    {   
        if(other.CompareTag(TagNeeded))
        {
            InvokeOnEventTriggerEnter(other.gameObject);
        }
    }

    // When we leave the trigger
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(TagNeeded))
        {
            InvokeOnEventTriggerExit(other.gameObject);
        }
    }

    // Fires the OnTriggerEventEnter event
    protected void InvokeOnEventTriggerEnter(GameObject other)
    {
        OnTriggerEventEnter?.Invoke(this, other);
    }

    // Fires the OnTriggerEventExit event
    protected void InvokeOnEventTriggerExit(GameObject other)
    {
        OnTriggerEventExit?.Invoke(this, other);
    }
}