using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydeath : MonoBehaviour
{
    // Start is called before the first frame update
    private enemyPatrol enemy_patrol;
    private Animator anim;
    [SerializeField] private Behaviour[] components;
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemy_patrol = GetComponentInParent<enemyPatrol>();
    }
    public void death()
    {
        anim.SetTrigger("death");
        foreach (Behaviour component in components)
            component.enabled = false;
    }
    private void gone()
    {
        Destroy(gameObject);
    }
}