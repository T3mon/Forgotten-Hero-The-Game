using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    private Animator animator;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage, Vector2? knockbackFore = null)
    {
        health -= damage;
        if (health <= 0)
            Die();
            
        if (damage > 0)
        {
            animator.SetTrigger("hit");

            if (knockbackFore is not null && health > 0)
                rb.AddForce((Vector2)knockbackFore);
        }


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
