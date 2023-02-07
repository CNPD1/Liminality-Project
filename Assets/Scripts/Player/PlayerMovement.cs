using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    //Constants
    [SerializeField] float maxStamina;
    [SerializeField] float staminaDrain;

    [SerializeField] float walkSpeed;
    [SerializeField] float friction;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;

    Vector2 input;
    Vector3 moveDir;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;
    }

    void Update()
    {
        CheckGround();

        InputHandler();
        rb.drag = friction;

    }

    void FixedUpdate()
    {
        SpeedControl();
        MovePlayer();
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.2f, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight + 0.2f));
    }

    void InputHandler()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDir = gameObject.transform.forward * input.y + gameObject.transform.right * input.x;

        rb.AddForce(moveDir * walkSpeed * 10f, ForceMode.Force);
    }

    void SpeedControl()
    {
        rb.velocity = new Vector3(
            Mathf.Clamp(rb.velocity.x, -walkSpeed * 2, walkSpeed * 2),
            rb.velocity.y,
            Mathf.Clamp(rb.velocity.z, -walkSpeed * 2, walkSpeed * 2)
        );
    }
}
