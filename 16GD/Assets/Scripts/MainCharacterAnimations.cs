using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterAnimations : MonoBehaviour
{
    bool shouldIdle = true;
    Animator animator;
    PlayerControllerScript playerControllerScript;
    float movementspeed;
    float currentSpeed;
    float timer = 0;
    bool grounded = true;
    Vector3 lastPosition;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControllerScript = GetComponentInParent<PlayerControllerScript>();
        movementspeed = playerControllerScript.moveSpeed;
    }

    void FixedUpdate()
    {
        currentSpeed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }

    private void Update()
    {
        timer -= Time.deltaTime; 
        animator.SetBool("Idle", shouldIdle);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("TrueGround", playerControllerScript.isGrouded);

        var hori = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        animator.SetFloat("InputH", hori);
        animator.SetFloat("InputV", vert);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            timer = 2.04f;
            animator.Play("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded )
        {
            animator.Play("Jump");
            grounded = false;
        }

        if (!grounded && playerControllerScript.isGrouded)
        {
            //timer = 0.7f;
            grounded = true;
        }


        if (timer <= 0)
            playerControllerScript.moveSpeed = movementspeed;
        else playerControllerScript.moveSpeed = 0;


        if (currentSpeed <= .01) 
        shouldIdle = true;
        else shouldIdle = false;
      
    }
}
