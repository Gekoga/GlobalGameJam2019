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

    private static readonly string HideWalls = "HideWalls";
    private static readonly string ShowWalls = "ShowWalls";

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

    private bool NoEnemiesLeft
    {
        get
        {
            if (GameManager.Instance.EnemiesLeft == 0)
                return true;
            else
                return false;
        }
    }

    public void Start()
    {
        if (roomType == RoomType.Enemies)
            SpawnEnemies();

        RoomAnimator.SetBool(HideWalls, true);
        RoomAnimator.SetBool(ShowWalls, false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomAnimator.SetBool(HideWalls, false);
            RoomAnimator.SetBool(ShowWalls, true);
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

        Invoke("WaitEnemies", 1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(SpawnCenter, spawnArea);
    }

    private void WaitEnemies()
    {
        StartCoroutine(WaitAfterEnemies());
    }

    private IEnumerator WaitAfterEnemies()
    {
        //Wait for a while to check if there are enemies, otherwise it will return 0
        yield return new WaitWhile(() => NoEnemiesLeft == false);

        RoomAnimator.SetBool(HideWalls, true);
        RoomAnimator.SetBool(ShowWalls, false);
    }

    //See when you enter
    //Start the animation when you enter
    //Check if you've beaten all the enemies
    //Start second animation
}
