using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMove;

    public float moveSpeed;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    public int jumpForce;

    private Rigidbody2D rb;

    public Animator animator;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private AudioManager audioManager;

    private bool isPlaying = false;

    public int extraJumps;
    private int extraJumpsValue;

    public bool facingRight;

    private void Awake()
    {
        extraJumpsValue = extraJumps;

        rb = GetComponent<Rigidbody2D>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            extraJumpsValue = extraJumps;
        }

        Movement();

        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && extraJumpsValue > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumpsValue--;
            audioManager.PlaySound("Jump");
        }
        else if (Input.GetButtonDown("Jump") && extraJumpsValue == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            audioManager.PlaySound("Jump");
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (isGrounded == true)
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
    }

    public void Movement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (facingRight == false && horizontalMove < 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontalMove > 0)
        {
            Flip();
        }

        if (!isPlaying)
        {
            if (isGrounded)
            {
                if (horizontalMove != 0)
                {
                    isPlaying = true;
                    audioManager.PlaySound("Walking");
                }
            }
        }

        if (!isGrounded)
        {
            isPlaying = false;
            audioManager.StopSound("Walking");
        }

        if (horizontalMove == 0)
        {

            isPlaying = false;
            audioManager.StopSound("Walking");
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
