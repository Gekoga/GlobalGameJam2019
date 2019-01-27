using UnityEngine;

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
            if (isGrouded)
            {
                return moveSpeed / 10;
            }
            else
                return (moveSpeed / 2) / 10;
        }
    }
    public float JumpForce;

    private bool isGrouded;
    private bool canPickUp;
    private float DistanceToTheGround
    {
        get
        {
            return GetComponentInChildren<Collider>().bounds.extents.y;
        }
    }

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
        isGrouded = Physics.Raycast(transform.position, Vector3.down, DistanceToTheGround + 0.1f);

        if (Input.GetAxis("Mouse X") == 0)
            rb.angularVelocity = Vector3.zero;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity, 0)));
        rb.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
        if (Input.GetKey("space") && isGrouded)
        {
            Vector3 jump = new Vector3(0.0f, JumpForce, 0.0f);
            rb.AddForce(jump, ForceMode.Impulse);
            isGrouded = false;
        }

        if(!Input.GetKeyDown(KeyCode.E)) return;
        if(canPickUp)
        {
            Pickup.Instance.InvokeOnPickedUp();
        }
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
