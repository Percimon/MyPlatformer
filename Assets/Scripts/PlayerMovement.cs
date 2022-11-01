using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private enum MovementState
    {
        IDLE,
        RUNNING,
        JUMPING,
        FALLING,
    }

    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (directionX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (directionX > 0f)
        {
            state = MovementState.RUNNING;
            sprite.flipX = false;
        }
        else if (directionX < 0f)
        {
            state = MovementState.RUNNING;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.IDLE;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.JUMPING;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.FALLING;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

}
