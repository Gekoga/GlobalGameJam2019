using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody rb;

    public float MouseSensitivity;
    public float MoveSpeed;
    public float JumpForce;

    private bool isGrouded;
    private float DistanceToTheGround
    {
        get
        {
            return GetComponent<Collider>().bounds.extents.y;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        isGrouded = Physics.Raycast(transform.position, Vector3.down, DistanceToTheGround + 0.1f);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));
        rb.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
        if (Input.GetKeyDown("space") && isGrouded)
            rb.AddForce(transform.up * JumpForce);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Use Pickup
        }
    }
}
