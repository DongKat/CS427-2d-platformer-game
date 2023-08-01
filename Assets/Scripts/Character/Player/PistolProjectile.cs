using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed = 7f;

    private Rigidbody2D rb;
    private CircleCollider2D coll;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;       

    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
