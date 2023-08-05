using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float SplashRange = 0f;

    [SerializeField]
    private float throwForce = 10f;

    [SerializeField]
    private float throwAngle = 45f;

    [SerializeField]
    private Vector3 launchOffset;

    [SerializeField]
    private float damage = 100f;

    public bool isThrown = false;

    private Rigidbody2D rb;
    private BoxCollider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        if (isThrown)
        {
            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * throwForce, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isThrown)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<enemydeath>().death();
        }
        Destroy(gameObject);
    }
}
