using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsScript : MonoBehaviour
{
    public static LevelsScript Instance;

    public QuestionsScript[] Questions;
    public int indexQuestion;
    public QuestionsScript CurrentQuestion
    {
        get
        {
            if (Questions.Length > indexQuestion)
            {
                return Questions[indexQuestion];
            }
            else
            {
                return Questions[Questions.Length -1];
            }
        }
    }

    public int enemyAmount;
    public GameObject enemyPrefab;


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
        StartQuestion();
    }

    public void StartQuestion()
    {
        for (int i = 0; i < Questions.Length; i++)
        {
            Questions[i].gameObject.SetActive(false);
        }
        if (indexQuestion != 0)
        {
            Questions[indexQuestion - 1].gameObject.SetActive(true);
        }
        CurrentQuestion.gameObject.SetActive(true);

        if (CurrentQuestion.roomTypes == QuestionsScript.RoomTypes.Enemy)
            CurrentQuestion.SpawnEnemies(enemyAmount);

        CurrentQuestion.Start();
    }

    public void EndQuestion()
    {
        if (Questions.Length == indexQuestion + 1)
        {
            Debug.Log("You've won the game");
            GameManager.Instance.InvokeOnLevelCompleted();
            return;
        }

        indexQuestion++;
        StartQuestion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = CurrentQuestion.RespawnLocation;
    }
}
