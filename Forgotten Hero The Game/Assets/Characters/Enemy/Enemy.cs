using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackFore = 6000f;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) // enemy attack
    {
        IDamageabe damageabe = other.collider.GetComponent<IDamageabe>();

        if (damageabe != null)
        {

            Vector2 direction = (Vector2)(other.gameObject.transform.position - transform.position).normalized;
            Vector2? knoback = knockbackFore > 0f ? direction * knockbackFore : null;
            damageabe.TakeDamage(damage, knoback);
        }
    }
}
