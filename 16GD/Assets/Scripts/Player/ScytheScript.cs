using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class ScytheScript : MonoBehaviour
{ 
    private Animator ScytheAnimator { get { return gameObject.GetComponent<Animator>(); } }

    public float scytheDamage;

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<AbstractEnemy>()?.TakeDamage(scytheDamage);
    }
}
