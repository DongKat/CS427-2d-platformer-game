using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX=0f;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed=7f;
    [SerializeField] private float jumpForce=7f;
    private enum MovementState {idle, walking, jumping, falling};
    
    // Start is called before the first frame update
    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        
        rb=GetComponent<Rigidbody2D>();
        coll=GetComponent<BoxCollider2D>();
        anim=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        spawnManager.RespawnPlayer();
        dirX= Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*moveSpeed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState currentMovementState=MovementState.idle;
        if(dirX>0f)
        {
            currentMovementState=MovementState.walking;
            sr.flipX=false;
        }
        else if(dirX<0f)
        {
            currentMovementState=MovementState.walking;
            sr.flipX=true;
        }
        else
        {
            currentMovementState=MovementState.idle;
        }
        if(rb.velocity.y>0.0001f)
        {
            currentMovementState=MovementState.jumping;
        }
        else if(rb.velocity.y<-0.0001f)
        {
            currentMovementState=MovementState.falling;
        }   
        anim.SetInteger("currentMovementState", (int)currentMovementState);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    
}
