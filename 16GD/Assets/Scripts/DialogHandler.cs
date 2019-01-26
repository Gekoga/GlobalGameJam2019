using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OnTriggerEvent))]
public class DialogHandler : MonoBehaviour
{
    // Components
    private OnTriggerEvent onTriggerEvent;

    // Misc
    public string Dialog;
    private bool isActive;

    // Used for initializing
    private void Start()
    {
        onTriggerEvent = GetComponent<OnTriggerEvent>();

        onTriggerEvent.OnTriggerEventEnter += OnTriggerEventEnter;
        onTriggerEvent.OnTriggerEventExit += OnTriggerEventExit;
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }
        if(Input.GetKey(KeyCode.E))
        {
            PlayerDialoghandler.Instance.ChangeDialog(Dialog);
            StartCoroutine(Timer());
        }
    }

    // When someone enters the Trigger
    private void OnTriggerEventEnter(OnTriggerEvent newOnTriggerEvent, GameObject other)
    {
        PlayerDialoghandler.Instance.SetPressButton(true);
        isActive = true;
    }

    // When someone exits the trigger
    private void OnTriggerEventExit(OnTriggerEvent newOnTriggerEvent, GameObject other)
    {
        PlayerDialoghandler.Instance.SetPressButton(false);
        isActive = false;
    }

    private IEnumerator Timer()
    {
        isActive = false;
        yield return new WaitForSeconds(2f);
        isActive = true;
    }
}