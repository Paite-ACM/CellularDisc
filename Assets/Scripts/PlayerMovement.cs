using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // movement
    public float movementSpeed;
    [SerializeField] private Transform orientation;

    private float horizontal;
    private float vertical;

    private Vector3 moveDir;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float groundDrag;

    // ground check
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    private bool grounded;

    // jumping
    public float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float jumpPadBoostModifier;
    private bool onJumpPad;
    private bool jumpReady;

    // keybinds
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    // slope handling
    [SerializeField] private float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    // Start is called before the first frame update
    void Start()
    {
        rb.freezeRotation = true;
        jumpReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        InputMovement();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void InputMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // jump input
        if (Input.GetKeyDown(jumpKey) && jumpReady && grounded)
        {
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer()
    {
        // claculate direction
        moveDir = orientation.forward * vertical + orientation.right * horizontal;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDir() * movementSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        if (grounded)
        {
            rb.AddForce(moveDir.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        // turn off gravity while on slope
        rb.useGravity = !OnSlope();
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    private void SpeedControl()
    {
        // limiting speed on a slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > movementSpeed)
            {
                rb.velocity = rb.velocity.normalized * movementSpeed;
            }
        }
        else
        {
            // limiting speed on the ground or in the air
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity
            if (flatVel.magnitude > movementSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * movementSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
       
    }

    private void Jump()
    {
        if (!onJumpPad)
        {
            exitingSlope = true;
            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            exitingSlope = true;
            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce((transform.up * jumpForce) * jumpPadBoostModifier, ForceMode.Impulse);
        }
        
    }

    private void ResetJump()
    {
        jumpReady = true;
        exitingSlope = false;
    }

    /// <summary>
    /// When the game ends, player movement will freeze and the player will not be able to jump
    /// </summary>
    public void StopPlayerMoving()
    {
        horizontal = 0f;
        vertical = 0f;
        movementSpeed = 0f;
        jumpReady = false;
    }

    
    private void OnEnable()
    {
        GameManager.GameEnded += StopPlayerMoving;
    }

    private void OnDisable()
    {
        GameManager.GameEnded -= StopPlayerMoving;
    }
}
