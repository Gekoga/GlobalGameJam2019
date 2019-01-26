using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState {IDLE,FORWARD,BACKWORD,LEFT,RIGHT,ATTACK}

public class MainCharacterAnimations : MonoBehaviour
{
    AnimState state = AnimState.IDLE;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
}
