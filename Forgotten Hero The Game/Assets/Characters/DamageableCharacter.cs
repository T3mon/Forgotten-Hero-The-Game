using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageabe
{
    public GameObject healthText;
    public bool targatable;
    private float _health = 3;
    public float health
    {
        set
        {
            if (value < _health)
            {
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }
            health = value;
            if (_health <= 0)
                targatable = false;
        }
        get
        {
            return _health;
        }
    }

    public bool targetable = true;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D physicsCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage, Vector2? knockbackFore = null)
    {
        health -= damage;

        if (damage > 0)
        {
            animator.SetTrigger("hit");

            if (health <= 0)
                animator.SetTrigger("defeated");

            if (knockbackFore is not null && health > 0)
                rb.AddForce((Vector2)knockbackFore);
        }
    }

    public void OnCharacterDead()
    {
        animator.SetTrigger("defeated");
    }

    public void MakeUntargatable()
    {
        throw new System.NotImplementedException();
    }
}