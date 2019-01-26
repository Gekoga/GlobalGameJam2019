using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDialoghandler : MonoBehaviour
{
    // Singleton
    public static PlayerDialoghandler Instance;

    // Components
    public TextMeshProUGUI DialogUi;
    public TextMeshProUGUI PressButtonUi;
    
    // Misc
    private readonly bool isInRange;
    private float t;
    public float LifeTime = 2f;
    public float Delay = 0.1f;

    // Initializing
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Used for initializing
    private void Start()
    {
        DialogUi = GetComponent<TextMeshProUGUI>();
        DialogUi.text = "...";
        PressButtonUi.text = string.Empty;
    }

    // Every frame
    private void Update()
    {
        if(t < LifeTime)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
            DialogUi.text = string.Empty;
        }
    }

    // Change the dialog
    public void ChangeDialog(string newText)
    {
        StartCoroutine(TextCoroutine(newText));
    }

    // Sets the PressButtonUi depending on the value
    public void SetPressButton(bool newValue)
    {
        PressButtonUi.text = newValue ? "Press E to continue" : string.Empty;
    }

    private IEnumerator TextCoroutine(string newText)
    {
        DialogUi.text = string.Empty;
        for(var i = 0; i < newText.Length; i++)
        {
            t = 0;
            yield return new WaitForSeconds(Delay);
            DialogUi.text += newText[i];
        }
    }
}