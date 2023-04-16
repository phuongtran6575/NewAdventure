using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D boxcoll;
    [SerializeField]private AudioSource jumpSoundEffect;

    private float dirX = 0f;
    private int jumpCount = 0;
    private int maxJumpCount = 2;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState { idle, running, jumping, falling, doublejump};
    // Start is called before the first frame update
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite= GetComponent<SpriteRenderer>();
        boxcoll= GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        Jump();
        UpdateAnimationState();
        
        transform.rotation = Quaternion.Euler(0, 0, 0f);
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && (jumpCount < maxJumpCount || IsGrounded()))
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
    private void UpdateAnimationState()
    {
        MovementState movementState;
        if(dirX> 0f)
        {
            movementState = MovementState.running;
            sprite.flipX = false;

        }
        else if(dirX <0f)
        {
            movementState = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            movementState = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            if (jumpCount > 1)
            {
                movementState = MovementState.doublejump;
            }
            else
            {
                movementState = MovementState.jumping;
            }
                
        }
        else if(rb.velocity.y < -0.1f)
        {
            movementState = MovementState.falling;
        }
        
        
        animator.SetInteger("movementState", (int)movementState);
        
        
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
