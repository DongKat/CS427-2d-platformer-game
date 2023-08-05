using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public enum CollectibleType
    {
        Grenade,
        AmmoCrate,
        MedKit,
        Coin
    };

    public CollectibleType collectibleType;

    public int amount = 1;


    // Start is called before the first frame update
    void Start() { }

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