using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float originalSpeed;
    public float Speed
    {
        get
        {
            if (IsGrounded)
            {
                return originalSpeed / 2;
            }
            else
                return originalSpeed;
        }
    }

    private Rigidbody rb;

    private bool IsGrounded
    {
        get
        {
            if (rb.velocity.y == 0)
            {
                return true;
            }
            else
                return false;
        }
    }

    [Header("Remove Later")]
    public bool isGround;
    public float sped;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        isGround = IsGrounded;
        sped = Speed;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jumpPower = 0;

        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpPower = 50;
            }
        }

        Vector3 movement = new Vector3(moveHorizontal, jumpPower, moveVertical);

        rb.AddForce(movement * Speed);
    }
}
