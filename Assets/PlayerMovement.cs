using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX=0f;
    private SpriteRenderer sr;
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
        rb.velocity = new Vector2(dirX*7f, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
        }
        UpdateAnimationUpdate();

    }
    private void UpdateAnimationUpdate()
    {
        if(dirX>0f)
        {
            anim.SetBool("walking", true);
            sr.flipX=false;
        }
        else if(dirX<0f)
        {
            anim.SetBool("walking", true);
            sr.flipX=true;
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
}
