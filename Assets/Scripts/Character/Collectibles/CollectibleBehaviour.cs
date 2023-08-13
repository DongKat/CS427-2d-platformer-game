using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{

    private GameManager gameManager;

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
       gameManager = GameManager.instance;
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
                    // GameManager.Instance.addGrenade();
                    break;
                case CollectibleType.AmmoCrate:
                    gameManager.addAmmo();
                    // GameManager.Instance.addAmmo();
                    break;
                case CollectibleType.MedKit:
                    gameManager.addHealth();
                    // GameManager.Instance.addHealth();
                    break;
                case CollectibleType.Coin:
                    gameManager.addCoin(amount);
                    // GameManager.Instance.addCoin(amount);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
