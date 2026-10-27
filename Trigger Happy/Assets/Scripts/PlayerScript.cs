using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jump;
    public float runLeniency;

    private float horizontal;
    private bool facingRight = true;

    [Header("CoyoteTime")]

    public float coyoteTime;
    private float coyoteTimeCounter;

    [Header("Jump Buffer")]
    public float jumpBufferTime;
    private float jumpBufferCounter;

    [Header("Slide")]
    public float slidePower;
    public float slideTime;
    public float slideCooldown;

    private bool canSlide = true;
    private bool isSliding;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform roofCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject gun;

    void Update()
    {
        if (isSliding)
        {
            BoxCollider2D collider = boxCollider;
            collider.size = new Vector2(0.54f, 0.42f);
            collider.offset = new Vector2(0f, -0.24f);
            boxCollider.size = collider.size;
            boxCollider.offset = collider.offset;
        }
        else
        {
            BoxCollider2D collider = boxCollider;
            collider.size = new Vector2(0.67f, 1.26f);
            collider.offset = new Vector2(0f, 0f);
            boxCollider.size = collider.size;
            boxCollider.offset = collider.offset;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > runLeniency || horizontal < -runLeniency)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);

            animator.SetBool("isJumping", true);

            jumpBufferCounter = 0f;
        }

        if(Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if(Input.GetKeyDown(KeyCode.S) && canSlide && coyoteTimeCounter > 0f)
        {
            StartCoroutine(Slide());
        }

        if(rb.linearVelocity.y < 0.01f)
        {
            animator.SetBool("isJumping", false);
        }

        Flip();
    }

    void FixedUpdate()
    {
        if (!isSliding)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    private void Flip()
    {
        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 gunScale = gun.transform.localScale;
            Vector3 localScale = transform.localScale;
            gunScale.x *= -1;
            localScale.x *= -1;
            gun.transform.localScale = gunScale;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsUnderGround()
    {
        return Physics2D.OverlapCircle(roofCheck.position, 0.2f, groundLayer);
    }

    private IEnumerator Slide()
    {
        animator.SetBool("sliding", true);
        canSlide = false;
        isSliding = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x + (transform.localScale.x * slidePower), 0f);
        yield return new WaitForSeconds(slideTime);
        while (IsUnderGround())
        {
            animator.SetBool("sliding", true);
            yield return null;
        }
        rb.gravityScale = originalGravity;
        isSliding = false;
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
        animator.SetBool("sliding", false);
    }
}