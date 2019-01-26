using System.Collections;
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

    private bool hideWall;
    private bool showWall;

    private bool useAnimations;

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

    private int EnemiesAmount
    {
        get
        {
            return GameManager.Instance.EnemiesLeft;
        }
    }

    public void Start()
    {
        if (roomType == RoomType.Enemies)
            SpawnEnemies();

        hideWall = true;
        showWall = false;

        RoomAnimator.SetBool("HideWalls", hideWall);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hideWall = false;
            showWall = true;
            RoomAnimator.SetBool("HideWalls", hideWall);
            RoomAnimator.SetBool("ShowWalls", showWall);
        }
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < RoomManager.Instance.enemiesAmount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, RandomPointInBox(SpawnCenter, spawnArea), Quaternion.identity);
        }

        StartCoroutine(WaitAfterEnemies());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(SpawnCenter, spawnArea);
    }

    private IEnumerator WaitAfterEnemies()
    {
        //Wait for a while to check if there are enemies, otherwise it will return 0
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => EnemiesAmount == 0);
        Debug.Log("Test");

        hideWall = true;
        showWall = false;
        RoomAnimator.SetBool("ShowWalls", showWall);
        RoomAnimator.SetBool("HideWalls", hideWall);

    }

    //See when you enter
    //Start the animation when you enter
    //Check if you've beaten all the enemies
    //Start second animation
}
