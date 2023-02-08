using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SwordAttack : MonoBehaviour
{
    public float damage = 3f;
    public Collider2D swordCollider;
    private Vector3 rightAttackOffset;
    // private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // playerController = GetComponent<PlayerController>();
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
            case Direction.left:
                transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
                break;
            case Direction.right:
                transform.localPosition = rightAttackOffset;
                break;
            case Direction.up:
                transform.localPosition = new Vector3(0, -0.08f);
                break;
            case Direction.down:
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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            //do damage
        }
    }
}
