using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class QuestionsScript : MonoBehaviour
{
    public enum RoomTypes
    {
        Enemy,
        PickUp
    }
    public RoomTypes roomTypes;

    private Animator RoomAnimator
    {
        get
        {
            return gameObject.GetComponent<Animator>();
        }
    }
    private BoxCollider RoomCollider
    {
        get
        {
            return gameObject.GetComponent<BoxCollider>();
        }
    }

    public Vector3 spawnCenter;
    private Vector3 SpawnCenter
    {
        get
        {
            return spawnCenter + transform.position;
        }
    }
    public Vector3 spawnArea;

    public Transform playerSpawnPoint;
    public Vector3 RespawnLocation
    {
        get
        {
            return playerSpawnPoint.position;
        }
    }

    public int killedEnemies;
    private bool AllEnemiesDead
    {
        get
        {
            if (LevelsScript.Instance.enemyAmount == killedEnemies)
            {
                return true;
            }
            else
                return false;
        }
    }

    private static readonly string HideWalls = "HideWalls";
    private static readonly string ShowWalls = "ShowWalls";

    public void Start()
    {
        RoomAnimator.SetBool(HideWalls, true);
        RoomAnimator.SetBool(ShowWalls, false);
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AllEnemiesDead)
                return;
            RoomAnimator.SetBool(HideWalls, false);
            RoomAnimator.SetBool(ShowWalls, true);
        }
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
           (UnityEngine.Random.value - 0.5f) * size.x,
           (UnityEngine.Random.value - 0.5f) * size.y,
           (UnityEngine.Random.value - 0.5f) * size.z
        );
    }

    public void SpawnEnemies(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject enemy = Instantiate(LevelsScript.Instance.enemyPrefab, RandomPointInBox(SpawnCenter, spawnArea), Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(SpawnCenter, spawnArea);
    }
}
