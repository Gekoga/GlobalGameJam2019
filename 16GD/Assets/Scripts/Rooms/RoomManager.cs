using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public List<GameObject> Platforms = new List<GameObject>();
    public int visiblePlatformAmount;

    private int totalStagesAmount;
    public int currentStage;

    public delegate void OnStageComplete();
    public OnStageComplete onStageComplete;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    public void Start()
    {
        totalStagesAmount = Mathf.CeilToInt(Platforms.Count / visiblePlatformAmount);
        if (visiblePlatformAmount * totalStagesAmount < Platforms.Count)
            totalStagesAmount += 1;

        onStageComplete += ShowMore;

        for (int i = 0; i < Platforms.Count; i++)
        {
            Platforms[i].SetActive(false);
        }

        for (int i = 0; i < visiblePlatformAmount; i++)
        {
            Platforms[i].SetActive(true);
        }
    }

    public void ShowMore()
    {
        currentStage++;

        if (currentStage == totalStagesAmount)
        {
            GameManager.Instance.InvokeOnLevelCompleted();
            return;
        }

        for (int i = 0; i < visiblePlatformAmount - 1; i++)
        {
            Platforms[i].SetActive(false);
        }
        Platforms.RemoveRange(0, visiblePlatformAmount);

        for (int i = 0; i < visiblePlatformAmount; i++)
        {
            Platforms[i].SetActive(true);
        }

    }
}
