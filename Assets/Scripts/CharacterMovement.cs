using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float walkSpeed = 3;
    public float runSpeed = 6; 
    private float currentSpeed;
    private bool isRunning = false;

    [HideInInspector] public Vector3 dir;
    private float horizontal, vertical;
    private CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    private Vector3 spherePos;

    [SerializeField] float jumpForce = 5f;
    private bool isJumping = false;
    private bool isLanding = false;

    [SerializeField] float gravity = -9.18f;
    private Vector3 velocity;
    private Animator animator;

    private bool isReloading = false;
    public float reloadTime = 1f;

    private PlayerHealth playerHealth;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            MovePlayer();
            CheckJump();
            Gravity();
            UpdateAnimatorParameters();

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }
    }

    void MovePlayer()
    {
        if (!isReloading)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            currentSpeed = isRunning ? runSpeed : walkSpeed;

            dir = transform.forward * vertical + transform.right * horizontal;

            controller.Move(dir.normalized * currentSpeed * Time.deltaTime);
        }
    }

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        isJumping = true;
        isLanding = false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0)
        {
            velocity.y = -2;
            isJumping = false;
            isLanding = true;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);
        
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        isReloading = false;
    }

    void UpdateAnimatorParameters()
    {
        animator.SetBool("Walking", (horizontal != 0 || vertical != 0) && !isRunning);
        animator.SetBool("Running", isRunning);
        animator.SetBool("Jumping", isJumping);
        animator.SetBool("Falling", !IsGrounded());
        animator.SetBool("Landing", isLanding);
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
        animator.SetBool("Reloading", isReloading);
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        float rayDistance = 0.2f; 

        RaycastHit hit;
        return Physics.Raycast(ray, out hit, rayDistance + groundYOffset, groundMask);
    }
}
