using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{

    GameManager gameManager;
    public enum CollectibleType
    {
        Grenade,
        AmmoCrate,
        MedKit,
        Coin
    };

    [SerializeField]
    public CollectibleType collectibleType;

    [SerializeField]
    public int amount;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
        }
    }
}
