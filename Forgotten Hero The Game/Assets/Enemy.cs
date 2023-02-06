using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Main
{
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
            Debug.Log("slime hp: " + health);
            health -= damage;
        }

        private void Die()
        {
            animator.SetTrigger("defeated");
            Debug.Log("slime dead");
            Destroy(gameObject);
        }
    }
}