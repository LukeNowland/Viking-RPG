using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {

        [SerializeField] float health = 100f;

        private bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0 && !isDead)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
