using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class SwordAttack : MonoBehaviour
    {
        public enum Direction { up, down, left, right } // *TODO Move it to namespace level 
        public Direction mouseDirection;
        public float damage = 3f;

        private Vector2 rightAttackOffset;
        private Collider2D swordCollider;
        private PlayerController playerController;

        // Start is called before the first frame update
        void Start()
        {
            swordCollider = GetComponent<Collider2D>();
            playerController = GetComponent<PlayerController>();
            swordCollider.enabled = false;
            rightAttackOffset = transform.position; // attack offset is right by default
        }

        public void Stop()
        {
            swordCollider.enabled = false;
        }

        public void AttackRight() // ! delete this
        {
            swordCollider.enabled = true;
            transform.position = rightAttackOffset;

        }

        public void Attack()
        {
            swordCollider.enabled = true;

            switch (mouseDirection) // Vector values are hardcore to be pixel perfect
            {
                case Direction.left:
                    transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
                    break;
                case Direction.right:
                    transform.position = rightAttackOffset;
                    break;
                case Direction.up:
                    transform.position = new Vector3(0, -0.08f);
                    break;
                case Direction.down:
                    transform.position = new Vector3(0.02f, -0.14f);
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
}