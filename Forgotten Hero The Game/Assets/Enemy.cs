using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Main
{
    public class Enemy : MonoBehaviour
    {
        public float health { get; set; } = 11f;
        // Start is called before the first frame update
        void Start()
        {

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
            Debug.Log("slime dead");
            Destroy(gameObject);
        }
    }
}