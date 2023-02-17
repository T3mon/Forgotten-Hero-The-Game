using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SwordAttack : MonoBehaviour
{
    public float damage = 3f;
    public float knockbackFore = 300f;
    public Collider2D swordCollider;
    private Vector3 rightAttackOffset;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider.enabled = false;
        rightAttackOffset = transform.position; // attack offset is right by default
    }

    public void Stop()
    {
        swordCollider.enabled = false;
    }

    public void Attack(Direction direction)
    {
        swordCollider.enabled = true;
        switch (direction) // Vector values are hardcore to be pixel perfect
        {
            case Direction.Left:
                transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
                break;
            case Direction.Right:
                transform.localPosition = rightAttackOffset;
                break;
            case Direction.Up:
                transform.localPosition = new Vector3(0, -0.08f);
                break;
            case Direction.Down:
                transform.localPosition = new Vector3(0.02f, -0.14f);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            var enemy = other.GetComponent<DamageableCharacter>();
            if (enemy != null)
            {
                Vector3 parentPosition = gameObject.GetComponent<Transform>().position;

                Vector2 direction = (Vector2)(other.gameObject.transform.position - parentPosition).normalized;
                Vector2? knoback = knockbackFore > 0f ? direction * knockbackFore : null;
                enemy.TakeDamage(damage, knoback);
            }
        }
    }
}
