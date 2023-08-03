using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydeath : MonoBehaviour
{
    // Start is called before the first frame update
    private enemyPatrol enemy_patrol;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemy_patrol = GetComponentInParent<enemyPatrol>();
    }
    public void death()
    {
        enemy_patrol.enabled = false;
        anim.SetTrigger("death");
    }
    private void gone()
    {
        Destroy(gameObject);
    }
}