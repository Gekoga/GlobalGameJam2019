﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerScript : MonoBehaviour
{
    // Components
    private Rigidbody rb;

    // Misc
    public float MouseSensitivity;
    public float moveSpeed;
    private float MoveSpeed
    {
        get
        {
            return moveSpeed / 10;
        }
    }
    public float JumpForce;

    public bool isGrouded;
    public bool canJump;
    private bool canPickUp;
    private float DistanceToTheGround
    {
        get
        {
            return GetComponentInChildren<Collider>().bounds.extents.y;
        }
    }

    public int Health = 100;

    public float test;

    void Start()
    {
        GameManager.Instance.playerReference = this;

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        if(Pickup.Instance == null)
        {
            return;
        }
        Pickup.Instance.OnCanNotPickup += OnCanNotPickUp;
        Pickup.Instance.OnCanPickup += OnCanPickUp;
        Pickup.Instance.OnPickedUp += OnPickedUp;
    }

    void FixedUpdate()
    {
        isGrouded = Physics.Raycast(transform.position, Vector3.down, DistanceToTheGround, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetAxis("Mouse X") == 0)
            rb.angularVelocity = Vector3.zero;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));
        rb.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));

        if (Input.GetKeyDown(KeyCode.Space) && isGrouded)
        {
            Vector3 jump = new Vector3(0.0f, JumpForce, 0.0f);
            rb.velocity = jump;
            rb.AddForce(jump, ForceMode.Impulse);
            isGrouded = false;
        }

        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (canPickUp)
        {
            Pickup.Instance.InvokeOnPickedUp();
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        LevelsScript.Instance.RespawnPlayer(gameObject);
        LevelsScript.Instance.CurrentQuestion.Start();
        Health = 100;
        Debug.Log("Player died");
    }

    #region Events

    private void OnPickedUp(Pickup pickup)
    {

    }

    private void OnCanPickUp(Pickup pickup)
    {
        canPickUp = true;
    }

    private void OnCanNotPickUp(Pickup pickup)
    {
        canPickUp = false;
    }

    #endregion
}
