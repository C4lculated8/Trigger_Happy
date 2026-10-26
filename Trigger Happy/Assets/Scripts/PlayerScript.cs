using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jump;

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
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        if (isSliding)
        {
            return;
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

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

        Flip();
    }

    void FixedUpdate()
    {
        if (isSliding)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private IEnumerator Slide()
    {
        canSlide = false;
        isSliding = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * slidePower, 0f);
        yield return new WaitForSeconds(slideTime);
        rb.gravityScale = originalGravity;
        isSliding = false;
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
    }
}
