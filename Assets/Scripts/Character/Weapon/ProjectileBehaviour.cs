using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Projectile fly direction")]
    public float moveSpeed = 7f;
    public float SplashRange = 5f;
    public float spinSpeed = 360f;
    public float damage = 100f;

    [Header("Projectile launch offset")]
    public Vector3 launchOffset;

    [Header("Is Grenade")]
    public bool isThrownable = false;

    [Header("Explosion effect")]
    public GameObject explosionEffect;

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        if (isThrownable)
        {
            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * moveSpeed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (isThrownable)
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // get object name
        var id = other.gameObject.name;

        // get object tag
        var tag = other.gameObject.tag;

        Debug.Log("Projectile hit " + tag + " with name " + name);

        if (isThrownable)
        {
            // Create explosion effect
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 0.2f);
            Destroy(gameObject);

            // Get all colliders in splash range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                transform.position,
                SplashRange
            );
            // Damage all enemies in splash range
            foreach (Collider2D nearbyObject in colliders)
            {
                if (nearbyObject.gameObject.tag == "enemy")
                {
                    nearbyObject.gameObject.GetComponent<enemydeath>().death();
                }
            }
        }
        else
        {
            // Damage enemy
            if (other.gameObject.tag == "enemy")
            {
                other.gameObject.GetComponent<enemydeath>().death();
            }
        }


        // Destroy projectile
        Destroy(gameObject);
    }
}
