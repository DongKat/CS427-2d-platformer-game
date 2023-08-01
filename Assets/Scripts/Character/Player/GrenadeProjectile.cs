using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float SplashRange = 0f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime + transform.up * moveSpeed * Time.deltaTime;   
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Play explode animation
        // 
        if (SplashRange > 0 )
        {
            var hitCollider = Physics2D.OverlapCircleAll(transform.position, SplashRange);
            foreach (var collider in hitCollider)
            {
                
            }
        }
        Destroy(gameObject);
    }
}
