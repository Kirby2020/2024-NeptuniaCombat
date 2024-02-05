using System;
using System.ComponentModel.Design;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private Animator animator;


    private Vector2 deadzone = new Vector2(0.1f, 0.1f);

    private const float WALK_SPEED = 100f;
    private const float ROTATE_SPEED = 8f;
    private const float JUMP_FORCE = 5f;

    private Vector2 moveDirection;
    private bool isGrounded = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var inputJump = Input.GetAxisRaw("Jump");

        animator.SetFloat(AnimatorParameters.MOVE_X, Mathf.Abs(inputX));

        moveDirection = new Vector2(inputX, 0).normalized;

        if (inputJump > deadzone.y && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.05f, groundLayer);

        rb.velocity = new Vector3(moveDirection.x * WALK_SPEED * Time.deltaTime, rb.velocity.y, 0);
        RotatePlayer();

        animator.SetBool(AnimatorParameters.IS_JUMPING, !isGrounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, JUMP_FORCE, 0);
    }

    private void RotatePlayer()
    {
        if (MathF.Abs(moveDirection.x) <= 0.1f)
        {
            return;
        }

        Vector3 targetDirection = moveDirection.x > 0 ? new Vector3(1, 0, 0) : new Vector3(-0.99f, 0, 0);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, ROTATE_SPEED * Time.deltaTime);
    }
}
