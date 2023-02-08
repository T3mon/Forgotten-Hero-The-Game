using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("slime hp: " + health);
    }

    private void Die()
    {
        animator.SetTrigger("defeated");
    }
    
    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }
    
}
