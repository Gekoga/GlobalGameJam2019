﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class RoomScript : MonoBehaviour
{
    public enum RoomType
    {
        Enemies,
        Pickup
    }
    public RoomType roomType;

    public Animator RoomAnimator
    {
        get
        {
            return gameObject.GetComponent<Animator>();
        }
    }
    public BoxCollider RoomCollider
    {
        get
        {
            return gameObject.GetComponent<BoxCollider>();
        }
    }

    public bool hideWall;
    public bool showWall;

    public Vector3 spawnCenter;
    private Vector3 SpawnCenter
    {
        get
        {
            return spawnCenter + transform.position;
        }
    }
    public Vector3 spawnArea;
    public GameObject enemyPrefab;
    public List<AbstractEnemy> AllEnemies = new List<AbstractEnemy>();

    public void Start()
    {
        switch (roomType)
        {
            case RoomType.Enemies:
                SpawnEnemies();
                showWall = true;

                StartCoroutine(EnemyCounter());
                break;
            case RoomType.Pickup:
                hideWall = true;
                break;
            default:
                break;
        }

        StartCoroutine(AnimationUpdate());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        GameObject enemy = Instantiate(enemyPrefab, RandomPointInBox(SpawnCenter, spawnArea), Quaternion.identity);
        AllEnemies.Add(enemy.GetComponent<AbstractEnemy>());

    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowWall());
        }
    }

    public IEnumerator EnemyCounter()
    {
        yield return new WaitUntil(() => AllEnemies.Count == 0);
        hideWall = true;
        showWall = false;
    }

    public IEnumerator ShowWall()
    {
        showWall = true;
        hideWall = false;
        yield break;
    }

    private IEnumerator AnimationUpdate()
    {
        do
        {
            RoomAnimator.SetBool("HideWalls", hideWall);
            RoomAnimator.SetBool("ShowWalls", showWall);
            yield return new WaitForEndOfFrame();

        } while (true);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(SpawnCenter, spawnArea);
    }

    //See when you enter
    //Start the animation when you enter
    //Check if you've beaten all the enemies
    //Start second animation
}
