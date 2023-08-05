using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
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
    private float lifetime;
    private bool hit = false;

    private Transform parent;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        if (isThrown)
        {
            transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
            var direction = new Vector2(-transform.localScale.x, Mathf.Tan(throwAngle*Mathf.PI/180));
            GetComponent<Rigidbody2D>().AddForce(throwForce * direction, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);

    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            anim.SetTrigger("explode");
        }
        else if (!isThrown)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime * transform.localScale.x;
        }
    }
    public void _reset()
    {
        //coll.enabled = true;
        hit = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        hit = true;
        coll.enabled = false;
        if (other.gameObject.tag == "Player") 
        {
            anim.SetTrigger("explode");
            //other.gameObject.GetComponent<>().; // call death of player
        }
    }
    private void gone()
    {
        Destroy(gameObject);
    }
}
