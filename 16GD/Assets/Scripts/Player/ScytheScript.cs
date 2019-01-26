using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class ScytheScript : MonoBehaviour
{
    public float scytheDamage;

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<AbstractEnemy>()?.TakeDamage(scytheDamage);
    }
}
