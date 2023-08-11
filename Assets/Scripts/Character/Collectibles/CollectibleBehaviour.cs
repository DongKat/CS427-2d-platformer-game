using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public GameManager gameManager;
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
    public int amount = 1;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch(collectibleType)
            {
                case CollectibleType.Grenade:
                    gameManager.addGrenade();
                    break;
                case CollectibleType.AmmoCrate:
                    gameManager.addAmmo();
                    break;
                case CollectibleType.MedKit:
                    gameManager.addHealth();
                    break;
                case CollectibleType.Coin:
                    gameManager.addCoin(amount);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
