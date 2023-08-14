using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossprojectile : MonoBehaviour
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
    public float normalDamage;
    public float unravelDamage;

    public bool isThrown = false;
    private float lifetime;
    private bool hit = false;
    private bool unravel = false;

    private Transform parent;
    private GameManager gameManager;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        if (isThrown)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            var direction = new Vector2(-transform.localScale.x, Mathf.Tan(throwAngle * Mathf.PI / 180));
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
        unravel = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        hit = true;
        if (unravel && other.gameObject.tag == "Player")
        {
            gameManager.takeDamage(unravelDamage);
            Debug.Log("unravel damage");
        }
        else if (other.gameObject.tag == "Player")
        {
            coll.enabled = false;
            anim.SetTrigger("explode");
            gameManager.takeDamage(normalDamage);
            //other.gameObject.GetComponent<>().; // call death of player
        }
        else if(other.gameObject.tag == "ground")
        {
            anim.SetTrigger("unravel");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            unravel = true;
        }
    }
    private void gone()
    {
        Destroy(gameObject);
    }
}
