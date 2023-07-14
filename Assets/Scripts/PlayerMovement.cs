using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX=0f;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed=7f;
    [SerializeField] private float jumpForce=7f;
    private enum MovementState {idle, walking, jumping, falling};
    
    // Start is called before the first frame update
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX= Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*moveSpeed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space))
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
            anim.SetBool("walking", false);
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
    
}
