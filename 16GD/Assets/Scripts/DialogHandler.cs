using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(OnTriggerEvent))]
public class DialogHandler : MonoBehaviour
{
    // Components
    private OnTriggerEvent onTriggerEvent;
    private event Action<DialogHandler> OnDialogStarted; 

    // Used for initializing
    private void Start()
    {
        onTriggerEvent = GetComponent<OnTriggerEvent>();

        onTriggerEvent.OnTriggerEventEnter += OnTriggerEventEnter;
        onTriggerEvent.OnTriggerEventExit += OnTriggerEventExit;
    }

    // When someone enters the Trigger
    private void OnTriggerEventEnter(OnTriggerEvent newOnTriggerEvent, GameObject other)
    {
    }

    // When someone exits the trigger
    private void OnTriggerEventExit(OnTriggerEvent newOnTriggerEvent, GameObject other)
    {

    }
}